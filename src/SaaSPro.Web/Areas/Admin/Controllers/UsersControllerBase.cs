using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;
using System;
using System.Web.Mvc;
using UsersResetPasswordModel = SaaSPro.Web.Areas.Admin.ViewModels.UsersResetPasswordModel;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public abstract class UsersControllerBase : AdminControllerBase
    {
        private readonly UserType _userType;
        private readonly IUserService _userService;

        public UsersControllerBase(UserType userType, IUserService userService)
        {
            _userType = userType;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index(PagingCommand command)
        {
            command.CustomerId = Customer.CustomerId;
            command.UserType = _userType;
            UsersListModel model = _userService.List(command);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            DeleteUserResponse response = _userService.Delete(new DeleteUserRequest { Id = id });

            if (response.HasError)
            {
                return HttpNotFound();
            }

            return RedirectToAction("index").AndAlert(AlertType.Warning, "Deleted.", "The user was deleted successfully.");
        }

        [HttpPost]
        public ActionResult ResetPassword(Guid id, UsersResetPasswordModel model)
        {
            _userService.SetPassword(id, model.NewPassword,expireImmediately: true);

            return RedirectToAction("update", new {id = id})
                .AndAlert(AlertType.Success, "Success.",
                          "The users password has been reset. They will prompted to change it when they next log in.");
        }

    }
}