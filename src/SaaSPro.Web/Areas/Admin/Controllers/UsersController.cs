using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;
using System;
using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class UsersController : UsersControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
            : base(UserType.SystemUser, userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            UsersAddModel model = _userService.AddUserModel(Customer.CustomerId, UserType.SystemUser);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(UsersAddModel model)
        {
            var request = new AddUserRequest
            {
                Customer = Customer.OriginalCustomer,
                CustomerId = Customer.CustomerId,
                Model = model
            };

            AddUserResponse response = _userService.Add(request);

            return RedirectToAction("update", new {id = response.UserID})
                .AndAlert(AlertType.Success, "Success.", "System user added successfully.");
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            var request = new GetUserProfileRequest
            {
                Id = id,
                CustomerId = Customer.CustomerId,
                UserType = UserType.SystemUser
            };
            GetUserProfileResponse response = _userService.GetUserProfile(request);

            return View(response.UsersUpdateModel);
        }

        [HttpPost]
        public ActionResult Update(Guid id, UsersUpdateModel model)
        {
            var request = new UpdateProfileRequest
            {
                Id = id,
                UsersUpdateModel = model,
                CustomerId = Customer.CustomerId
            };

            UpdateProfileResponse response = _userService.UpdateProfile(request);

            if (response.HasError)
            {
                return HttpNotFound();
            }

            return RedirectToAction("update", new {id})
                .AndAlert(AlertType.Success, "Success.", "System user updated successfully.");
        }
    }
}