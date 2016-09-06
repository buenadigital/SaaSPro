using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.ViewModels;
using System.Web.Mvc;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Controllers
{
    public class AccountController : SaaSProControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;

        public AccountController(IUserService userService, ICustomerService customerService)
        {
            _userService = userService;
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(AccountChangePasswordModel model)
        {
            var currentUser = _userService.GetLoginSessionUser(User.Id);

            if (!currentUser.ValidatePassword(model.CurrentPassword))
            {
                ModelState.AddModelError("", "Your current password is incorrect.");
                return View();
            }

            if (currentUser.ValidatePassword(model.NewPassword))
            {
                ModelState.AddModelError("", "Your new password cannot be the same as your old password.");
                return View();
            }

            currentUser.SetPassword(model.NewPassword);
            return RedirectToAction("changepassword").AndAlert(AlertType.Success, "Success.", "Your password was updated successfully.");
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            var model = _customerService.GetCustomerDetails(Customer.CustomerId);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProfile(CustomersDetailsModel model)
        {
            _customerService.Save(model);

            return RedirectToAction("UpdateProfile")
                .AndAlert(AlertType.Success, "Profile updated.", "The details were updated successfully.");
        }

        [HttpGet]
        public ActionResult ChangeSecurityQuestions()
        {
            var request = new GetSecurityQuestionsRequest
            {
                Id = User.Id,
                CustomerId = Customer.CustomerId
            };
            GetSecurityQuestionsResponse response = _userService.GetSecurityQuestions(request);

            return View(response.UsersUpdateSecurityQuestionsModel);
        }

        [HttpPost]
        public ActionResult ChangeSecurityQuestions(UsersUpdateSecurityQuestionsModel model)
        {
            var request = new UpdateSecurityQuestionsRequest
            {
                Id = User.Id,
                UsersUpdateSecurityQuestionsModel = model,
                CustomerId = Customer.CustomerId
            };

            UpdateSecurityQuestionsResponse response = _userService.UpdateSecurityQuestions(request);

            if (response.HasError)
            {
                return HttpNotFound();
            }

            return RedirectToAction("ChangeSecurityQuestions").AndAlert(AlertType.Success, "Success.", "Your security questions were updated successfully.");
        }
    }
}