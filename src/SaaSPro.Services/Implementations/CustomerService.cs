using System;
using System.Linq;
using SaaSPro.Common.Helpers;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.CustomerService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Common;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using AutoMapper;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Web.Common;
using Stripe;
using System.Collections.Generic;

namespace SaaSPro.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerPaymentRepository _customerPaymentRepository;
        private readonly ICustomerPaymentRefundRepository _customerPaymentRefundRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReferenceListRepository _referenceListRepository;
        private readonly IReferenceListItemRepository _referenceListItemRepository;
        private readonly IPlanRepository _planRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IApiTokenRepository _apitokensRepository;
        private readonly IIPSRepository _ipsEntryRepository;
        


        public CustomerService(IUnitOfWork unitOfWork,ICustomerRepository customerRepository,
            ICustomerPaymentRepository customerPaymentRepository, ICustomerPaymentRefundRepository customerPaymentRefundRepository,
            IRoleRepository roleRepository,IUserRepository userRepository, IReferenceListRepository referenceListRepository,
            IReferenceListItemRepository referenceListItemRepository, IPlanRepository planRepository, 
            INoteRepository noteRepository, IApiTokenRepository apitokensRepository, IIPSRepository ipsEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _customerPaymentRepository = customerPaymentRepository;
            _customerPaymentRefundRepository = customerPaymentRefundRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _referenceListRepository = referenceListRepository;
            _referenceListItemRepository = referenceListItemRepository;
            _planRepository = planRepository;
            _noteRepository = noteRepository;
            _apitokensRepository = apitokensRepository;
            _ipsEntryRepository = ipsEntryRepository;
        }



        public CustomersListModel List(PagingCommand command, Func<IQueryable<Customer>, IQueryable<Customer>> query)
        {
            var customers = _customerRepository.FetchPaged(query, command.PageIndex, command.PageSize);

            var model = new CustomersListModel
            {
                Customers = Mapper.Engine.MapPaged<Customer, CustomersListModel.CustomerSummary>(customers)
            };

            return model;
        }

        public CustomersDetailsModel GetCustomerDetails(Guid id)
        {
             Customer customer= _customerRepository.Get(id);

             var model = new CustomersDetailsModel
             {
                 Id = customer.Id,
                 Hostname = customer.Hostname,
                 FullName = customer.FullName,
                 Company = customer.Company,
                 Enabled = customer.Enabled,
                 Email = customer.AdminUser.Email,
                 PaymentCustomerId = customer.PaymentCustomerId,
                 HasPlan = (customer.Plan!=null) 
             };

            return model;
        }

        public void Save(CustomersDetailsModel model)
        {
            Customer customer = _customerRepository.Get(model.Id);

            customer.Update(model.FullName, model.Hostname, model.Company);
            customer.AdminUser.UpdateProfile(model.Email, customer.AdminUser.FirstName, customer.AdminUser.LastName);

            if (model.Enabled)
            {
                customer.Enable();
            }
            else
            {
                customer.Disable();
            }

            _customerRepository.Update(customer);

            _unitOfWork.Commit();
        }

        public Guid ResetPassword(Guid id, UsersResetPasswordModel model)
        {
            Customer customer = _customerRepository.Get(id);

            customer.AdminUser.SetPassword(model.NewPassword, expireImmediately: true);

            return customer.Id;
        }

        public  void Delete(Guid id)
        {
            Customer customer = _customerRepository.Get(id);

            // Remove Customer from stripe service
            if (!string.IsNullOrEmpty(customer.PaymentCustomerId))
            {
                try
                {
                    if (StripeFactory.GetStripeService().GetCustomer(customer.PaymentCustomerId)!=null)
                    {
                        StripeFactory.GetStripeService().DeleteCustomer(customer.PaymentCustomerId);
                    }
                    LoggingFactory.GetLogger().Log("Customer not found",EventLogSeverity.Fatal);
                }catch(Exception ex)
                {
                    LoggingFactory.GetLogger().LogError(ex);
                }
            }

			_unitOfWork.Begin();

			// Delete relations between users and roles
			foreach (var role in customer.Roles)
	        {
				role.RoleUsers.Clear();
				_roleRepository.Update(role);
	        }

            // Delete Notes
            foreach (var note in customer.Notes.ToList())
            {
                _noteRepository.Delete(note);
            }

            // Delete API records
            foreach (var apiToken in customer.ApiTokens.ToList())
            {
                _apitokensRepository.Delete(apiToken);
            }

            // Delete IPS records
            foreach (var ipsEntry in customer.IPSEntries.ToList())
            {
                _ipsEntryRepository.Delete(ipsEntry);
            }

            //delete all reference list items
            foreach (var item in customer.ReferenceListItems.ToList())
            {
                _referenceListItemRepository.Delete(item);
            }

            // delete all users
            foreach (var user in customer.Users.ToList())
            {
                _userRepository.Delete(user);
            }

            //// delete all payments
            //foreach (var payment in _customerPaymentRepository.Query().Where(x => x.CustomerId == id).ToList())
            //{
            //    _customerPaymentRepository.Delete(payment);
            //}

            //// delete all payment refunds
            //foreach (var paymentRefund in _customerPaymentRefundRepository.Query().Where(x => x.CustomerId == id).ToList())
            //{
            //    _customerPaymentRefundRepository.Delete(paymentRefund);
            //}

            _customerRepository.Delete(customer);

            _unitOfWork.End();
		}

        public Guid Provision(CustomersProvisionModel model)
        {
            var fullName = model.FirstName + " " + model.LastName;

            //create customer in stripe
            StripeCustomer stripeCustomer = StripeFactory.GetStripeService().CreateCustomer(fullName, model.Email);

            // Customer
            Customer customer = new Customer(fullName, model.Domain, model.Company, stripeCustomer.Id)
            {
                PlanCreatedOn = stripeCustomer.Created
            };

            _customerRepository.Add(customer);

            // roles
            var role = new Role(customer, "Administrator", true, UserType.SystemUser);
            _roleRepository.Add(role);

            // users
            var user = new User(customer, model.Email, "Admin", "User", "admin", true);
            _userRepository.Add(user);
            role.AddUser(user);
            customer.UpdateAdminUser(user);

            // security questions
            ReferenceList referenceList = _referenceListRepository.Query().SingleOrDefault(l => l.SystemName == "Security Questions");
            foreach (var securityQuestion in SecurityQuestions.Questions)
            {
                referenceList.AddItem(customer, securityQuestion);
            }

            // user security questions
            user.AddSecurityQuestion("a", "a");
            user.AddSecurityQuestion("b", "b");
            user.AddSecurityQuestion("c", "c");

            _unitOfWork.Commit();

            return customer.Id;
        }

        public CustomerPaymentsModel Payments(Guid id)
        {
            Customer customer = _customerRepository.Get(id);
			var model = new CustomerPaymentsModel
                            {
                                CustomerId = customer.Id,
                                CustomerName = customer.FullName,
                                CustomerEmail = customer.AdminUser.Email,
                                PaymentCustomerId = customer.PaymentCustomerId,
                                CustomerPlan =
                                    (!string.IsNullOrEmpty(customer.PaymentCustomerId))
                                        ? StripeFactory.GetStripeService().GetCustomerPlan(customer.PaymentCustomerId)
                                        : null,
                                StripeChargesList =
                                    (!string.IsNullOrEmpty(customer.PaymentCustomerId))
                                        ? StripeFactory.GetStripeService().GetCustomerPayments(10, customer.PaymentCustomerId)
                                        : null
                            };
            return model;
        }

        public SetupStripeResponse SetupStripe(Guid customerId)
        {
            SetupStripeResponse  response=new SetupStripeResponse();

            Customer customerObj = _customerRepository.Get(customerId);

            try
            {
                var customer = StripeFactory.GetStripeService().CreateCustomer(customerObj.FullName, customerObj.AdminUser.Email);
                customerObj.UpdateStripe(customer.Id);

                _customerRepository.Update(customerObj);

                _unitOfWork.Commit();
            }
            catch
            {
                response.HasError = true;
                response.ErrorCode=ErrorCode.StripeSetupError;
            }

            return response;
        }

        public string ClosePlan(Guid customerId)
        {
            Customer customer = _customerRepository.Get(customerId);

            var status = StripeFactory.GetStripeService().CloseCustomerPlan(customer.PaymentCustomerId);

            customer.RemovePlan();
            _customerRepository.Update(customer);
            _unitOfWork.Commit();

            return status;
        }

        public RefundModel GetRefundPayment(Guid customerId, string paymentId)
        {
            //Customer customer = _customerRepository.Get(customerId);

            var payment = StripeFactory.GetStripeService().GetCustomerPayment(paymentId);

            var viewModel = new RefundModel
            {
                CustomerId = customerId,
                PaymentId = paymentId,
                Amount = GetRemainingAmount(
                    payment.Amount,
                    payment.AmountRefunded),
            };

            return viewModel;
        }

        public string CheckUserName(string input)
        {
            var result = _customerRepository.Query().Any(t => t.Hostname.ToLower() == input.ToLower());

            if (result == false)
                return "Available";

            return "Not Available";
        }

        public SetupPaymentPlanReponse SetupPaymentPlan(PlanSetupModel planModel)
        {
            var response = new SetupPaymentPlanReponse();
            try
            {
                Customer customer = _customerRepository.Get(Guid.Parse(planModel.CustomerId));
                if (customer == null)
                {
                    throw new Exception("Incorrect Customer identifier");
                }

                if (customer.Plan != null)
                {
                    response.HasPlan = true;
                    var status = StripeFactory.GetStripeService().CloseCustomerPlan(customer.PaymentCustomerId);
                    response.Status = status;

                    //return response;
                }

                Plan dbPlan =
                    _planRepository.Query().FirstOrDefault(p => p.PlanCode.ToLower() == planModel.StripePlanId.ToLower());
                if (dbPlan == null)
                {
                    throw new Exception("Incorrect plan identifier");
                }

                var result = StripeFactory.GetStripeService().AssignCustomerPlan(
                    planModel.PaymentCustomerId, planModel.StripePlanId, planModel.CreditCardNumber,
                    planModel.Cvc, planModel.CardExpirationMonth, planModel.CardExpirationYear);

                if (result != "active")
                {
                    response.IsStatusActive = false;
                    return response;
                }
                else
                {
                    //dbPlan.Customers.
                    response.IsStatusActive = true;
                }

                customer.UpdatePlan(dbPlan);
                _unitOfWork.Commit();

                return response;
            }
            catch (StripeException ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                response.HasError = true;
                response.ErrorCode = ErrorCode.StripeSetupError;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                response.HasError = true;
                response.ErrorCode = ErrorCode.StripeSetupError;
                response.Message = ErrorCode.StripeSetupError.ToString();
            }
            return response;
        }

        public NotesViewModel Notes(Guid customerId, PagingCommand command)
        {
            var notes =
                _noteRepository.FetchPaged(
                    q => q.Where(n => n.CustomerId == customerId).OrderByDescending(t => t.CreatedOn), command.PageIndex,
                    command.PageSize);

            var model = new NotesViewModel
            {
                Notes = Mapper.Engine.MapPaged<Note, NotesViewModel.Note>(notes)
            };

            return model;
        }

        public void AddNote(NotesViewModel.Note note)
        {
            Ensure.NotNull(note, "note");

            Mapper.CreateMap<NotesViewModel.Note, Note>();
            var newNote = Mapper.Map<NotesViewModel.Note, Note>(note);

            _noteRepository.Add(newNote);
            _unitOfWork.Commit();
        }

        public void DeleteNote(Guid id)
        {
            var note = _noteRepository.Get(id);

            _noteRepository.Delete(note);

            _unitOfWork.Commit();
        }

        private static decimal GetRemainingAmount(int amount, int amountRefunded)
        {
            var remaining = amount - amountRefunded;
            return remaining > 0 ? (decimal)remaining / 100 : 0;
        }


    }
}
