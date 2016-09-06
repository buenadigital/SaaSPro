using System.Collections.Generic;
using SaaSPro.Common.Web;
using SaaSPro.Common;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.CustomerService;
using SaaSPro.Services.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using SaaSPro.Web.Management.ViewModels.CustomerPayments;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Services.Helpers;
using SaaSPro.Web.Common;
using SaaSPro.Domain;

namespace SaaSPro.Web.Management.Controllers
{
    public class CustomersController : ManagementControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        public ActionResult Provision()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Provision(CustomersProvisionModel model)
        {
            if (ModelState.IsValid)
            {
                var customerId = _customerService.Provision(model);
                return RedirectToAction("index", new {id = customerId})
                    .AndAlert(AlertType.Success, "Customer Provisioned.", "The Customer was provisioned successfully.");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            CustomersDetailsModel model = _customerService.GetCustomerDetails(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Details(CustomersDetailsModel model)
        {
            _customerService.Save(model);

            return RedirectToAction("index", new {id = model.Id})
                .AndAlert(AlertType.Success, "Customer updated.", "The Customer details were updated successfully.");
        }

        [HttpPost]
        public ActionResult ResetPassword(Guid id, UsersResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                _customerService.ResetPassword(id, model);
            }

            return RedirectToAction("details", new {id = model.Id})
                .AndAlert(AlertType.Success, "Customer updated.", "The Customer details were updated successfully.");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _customerService.Delete(id);

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Customer deleted.", "The Customer was deleted successfully.");
        }

        [HttpGet]
        [ActionName("get-regions")]
        public ActionResult GetRegions(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            }

            var country = Country.FindByIsoCode(countryCode);
            if (country != null)
            {
                return
                    Json(
                        new
                        {
                            success = true,
                            regions = country.Regions.Select(r => new {name = r.Name, code = r.Abbreviation})
                        },
                        JsonRequestBehavior.AllowGet);
            }

            return Json(new {success = false}, JsonRequestBehavior.AllowGet);
        }

        #region Payments processing

        public ActionResult Payments(Guid id)
        {
            var model = _customerService.Payments(id);
            return View(model);
        }

        private SelectList GetPlans()
        {
            var availablePlans =
                StripeFactory.GetStripeService().GetAvailablePlans().Select(p => new CompactPlan(p)).ToList();
            return new SelectList(availablePlans, "PlanId", "PlanName", 1);
        }

        private static SelectList GetYears()
        {
            var years = new List<int>();
            var startYear = DateTime.Now;
            while (startYear.Year <= DateTime.Now.AddYears(10).Year)
            {
                years.Add(startYear.Year);
                startYear = startYear.AddYears(1);
            }
            return new SelectList(years);
        }

        private static SelectList GetMonths()
        {
            var months = new List<int>(12);
            for (var month = 1; month <= 12; month++)
            {
                months.Add(month);
            }
            return new SelectList(months);
        }

        public ActionResult SetupStripe(Guid customerId)
        {
            SetupStripeResponse response = _customerService.SetupStripe(customerId);

            if (response.HasError && response.ErrorCode == ErrorCode.StripeSetupError)
            {
                return RedirectToAction("payments", "customers", new {id = customerId})
                    .AndAlert(AlertType.Danger, "Stripe error.", "Error is thrown during creating stripe customer.");
            }

            return RedirectToAction("payments", "customers", new {id = customerId})
                .AndAlert(AlertType.Success, "Stripe customer created.", "Stripe customer was created successfully.");

        }

        [HttpGet]
        public ActionResult SetupPaymentPlan(string paymentCustomerId, string customerId)
        {
            var viewModel = new PlanSetupModel
            {
                StripePlanList = GetPlans(),
                YearsList = GetYears(),
                MonthsList = GetMonths(),

                PaymentCustomerId = paymentCustomerId,
                CustomerId = customerId
            };
            return PartialView("Partials/_SetupPlan", viewModel);
        }

        [HttpPost]
        public ActionResult SetupPaymentPlan(PlanSetupModel planModel)
        {
            planModel.StripePlanList = GetPlans();
            planModel.YearsList = GetYears();
            planModel.MonthsList = GetMonths();

            if (ModelState.IsValid)
            {
                SetupPaymentPlanReponse reponse = _customerService.SetupPaymentPlan(planModel);

                if (reponse.HasError && reponse.ErrorCode == ErrorCode.StripeSetupError)
                {
                    LoggingFactory.GetLogger()
                        .Log("Stripe Error - Error is thrown during setting plan and payment.", EventLogSeverity.Error);

                    return RedirectToAction("payments", "customers", new { id = planModel.CustomerId })
                        .AndAlert(AlertType.Danger, "Stripe error.", "Error is thrown during setting plan and payment.");
                }
                else if (!reponse.IsStatusActive)
                {
                    LoggingFactory.GetLogger()
                        .Log("Stripe Error - Setting plan and payment result is not correct.", EventLogSeverity.Error);

                    return RedirectToAction("payments", "customers", new { id = planModel.CustomerId })
                        .AndAlert(AlertType.Danger, "Stripe error.", "Setting plan and payment result is not correct.");
                }
                else if (reponse.HasPlan && reponse.Status.ToLower() == "active")
                {
                        LoggingFactory.GetLogger()
                            .Log("Stripe Error - Previous plan wasn't closed.", EventLogSeverity.Error);

                        return RedirectToAction("payments", "customers", new {id = Guid.Parse(planModel.CustomerId)})
                            .AndAlert(AlertType.Danger, "Stripe error.", "Previous plan wasn't closed.");
                }

                return RedirectToAction("payments", "customers", new {id = planModel.CustomerId})
                    .AndAlert(AlertType.Success, "Stripe payment.", "Stripe plan and payment were created sucessfully.");
            }

            return RedirectToAction("payments", "customers", new {id = planModel.CustomerId})
                .AndAlert(AlertType.Danger, "Error.", "Provided data is incorrect.");
        }

        [AllowAnonymous]
        public string CheckUserName(string input)
        {
            string result = _customerService.CheckUserName(input);

            return result;
        }

        [HttpPost]
        public ActionResult ClosePlan(Guid customerId)
        {
            try
            {
                string status = _customerService.ClosePlan(customerId);

                if (status.ToLower() == "active")
                {
                    return RedirectToAction("payments", "customers", new {id = customerId})
                        .AndAlert(AlertType.Danger, "Plan.", "There was a problem closing the plan.");
                }
            }
            catch
            {
                return RedirectToAction("payments", "customers", new {id = customerId})
                    .AndAlert(AlertType.Danger, "Plan.", "Error was thrown closing the plan.");
            }

            return RedirectToAction("payments", "customers", new {id = customerId})
                .AndAlert(AlertType.Success, "Plan.", "Plan was successfully closed.");
        }

        [HttpGet]
        public ActionResult RefundPayment(Guid customerId, string paymentId)
        {
            RefundModel model = _customerService.GetRefundPayment(customerId, paymentId);
            return PartialView("Partials/_RefundPayment", model);
        }

        [HttpPost]
        public ActionResult RefundPayment(RefundModel refundModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CustomersDetailsModel customer = _customerService.GetCustomerDetails(refundModel.CustomerId);
                    if (customer == null)
                    {
                        return HttpNotFound();
                    }

                    var payment = StripeFactory.GetStripeService().GetCustomerPayment(refundModel.PaymentId);
                    var remainingAmount = GetRemainingAmount(
                        payment.Amount,
                        payment.AmountRefunded);
                    if (refundModel.Amount > remainingAmount)
                    {
                        return RedirectToAction("payments", "customers", new {id = refundModel.CustomerId})
                            .AndAlert(AlertType.Danger, "Error.", "Provided data is incorrect.");
                    }

                    StripeFactory.GetStripeService().Refund(refundModel.PaymentId, refundModel.AmountCents);
                }
                catch
                {
                    return RedirectToAction("payments", "customers", new {id = refundModel.CustomerId})
                        .AndAlert(AlertType.Danger, "Refund error.", "Error was thrown during refunding payment.");
                }

                return RedirectToAction("payments", "customers", new {id = refundModel.CustomerId})
                    .AndAlert(AlertType.Success, "Refund.", "Payment was refunded sucessfully.");
            }

            return RedirectToAction("payments", "customers", new {id = refundModel.CustomerId})
                .AndAlert(AlertType.Danger, "Error.", "Provided data is incorrect.");
        }

        private static decimal GetRemainingAmount(int amount, int amountRefunded)
        {
            var remaining = amount - amountRefunded;
            return remaining > 0 ? (decimal) remaining/100 : 0;
        }

        #endregion Payments processing

        #region Notes


        [HttpGet]
        public ActionResult Notes(Guid id, PagingCommand command)
        {
            NotesViewModel model = _customerService.Notes(id, command);
            model.CustomerNote = new NotesViewModel.Note {CustomerId = id};
            return View(model);
        }

        [HttpPost]
        [ActionName("delete-note")]
        public ActionResult DeleteNote(Guid id, Guid customerId)
        {
            _customerService.DeleteNote(id);

            return RedirectToAction("Notes", new {id = customerId})
                .AndAlert(AlertType.Success, "Note deleted.", "The note was deleted successfully.");
        }


        [ActionName("add-note")]
        [ValidateInput(false)]
        public ActionResult AddNote(NotesViewModel noteView)
        {
            if (ModelState.IsValid)
            {
                var note = noteView.CustomerNote;

                note.Id = Guid.NewGuid();
                note.CreatedOn = DateTime.Now;

                _customerService.AddNote(note);

                return RedirectToAction("Notes", new { id = note.CustomerId })
                    .AndAlert(AlertType.Success, "Note added.", "The new note was added successfully.");
            }

            return RedirectToAction("Notes", new { id = noteView.CustomerNote.CustomerId})
                .AndAlert(AlertType.Danger, "Error.", "Please add a note."); 
        }

        #endregion

        #region Ajax requests

        public JsonResult GetCustomersData(int offset, int limit, string search, string sort, string order)
        {
            Func<IQueryable<Customer>, IQueryable<Customer>> sortOrderQuery = null;

            var querySorting = (sort ?? "") + (order ?? "");

            switch (querySorting.ToLower())
            {
                case "idasc":
                    sortOrderQuery = q => q.OrderBy(t => t.Id);
                    break;
                case "iddesc":
                    sortOrderQuery = q => q.OrderByDescending(t => t.Id);
                    break;
                case "companyasc":
                    sortOrderQuery = q => q.OrderBy(t => t.Company);
                    break;
                case "companydesc":
                    sortOrderQuery = q => q.OrderByDescending(t => t.Company);
                    break;
                case "fullnameasc":
                    sortOrderQuery = q => q.OrderBy(t => t.Company);
                    break;
                case "fullnamedesc":
                    sortOrderQuery = q => q.OrderByDescending(t => t.Company);
                    break;
                case "hostnameasc":
                    sortOrderQuery = q => q.OrderBy(t => t.Hostname);
                    break;
                case "hostnamedesc":
                    sortOrderQuery = q => q.OrderByDescending(t => t.Hostname);
                    break;
                case "createdonasc":
                    sortOrderQuery = q => q.OrderBy(t => t.CreatedOn);
                    break;
                case "createdondesc":
                    sortOrderQuery = q => q.OrderByDescending(t => t.CreatedOn);
                    break;
                default:
                    sortOrderQuery = q => q.OrderByDescending(t => t.CreatedOn);
                    break;
            }
         
            var customersModel = _customerService.List(new PagingCommand() { Page = (offset / limit) + 1, PageSize = limit }, sortOrderQuery);
            var customers = QueryExtensions.WhereIf(customersModel.Customers.AsQueryable(),
                !string.IsNullOrEmpty(search),
                t => t.FullName.Contains(search, StringComparison.InvariantCultureIgnoreCase) || t.Hostname.Contains(search, StringComparison.InvariantCultureIgnoreCase) || t.AdminEmail.Contains(search, StringComparison.InvariantCultureIgnoreCase));

            customers = customers.AsQueryable().OrderBy(sort ?? "CreatedOn", order ?? "desc");

            var model = new CustomerJsonResult
			{
                total = customersModel.Customers.TotalCount,
                rows = customers,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CustomerFullNameAutocomplete(string term)
        {
            var filteredItems = _customerService.List(new PagingCommand(), q => q.OrderByDescending(x => x.CreatedOn)).Customers.Select(t=>t.FullName).Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );

            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

	public class CustomerJsonResult
	{
		public int total { get; set; }
		public IQueryable<CustomersListModel.CustomerSummary> rows { get; set; }
	}
}
