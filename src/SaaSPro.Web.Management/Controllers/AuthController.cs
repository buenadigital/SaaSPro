using System.Web.Mvc;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Web.Management.ViewModels;
using System.Web.Security;

namespace SaaSPro.Web.Management.Controllers
{
    [AllowAnonymous]
    public class AuthController : ManagementControllerBase
    {
        private readonly IUserService _userService;
      

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
            if (FormsAuthentication.Authenticate(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("index", "dashboard");
            }

            return RedirectToAction("login").AndAlert(AlertType.Danger,
                "Invalid credentials");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        [HttpGet]
        [ActionName("Forgot-Password")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Forgot-Password")]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                GetUserRequest request = new GetUserRequest()
                                             {
                                                 Email = model.Username,
                                                 GetBy = "EMAIL"
                                             };
                var user = _userService.GetUser(request);
                if (user == null)
                {
                    ModelState.AddModelError("", "The e-mail address you entered does not match any accounts on record.");
                    return View();
                }

                //dynamic message = new Email("_PasswordResetEmail");
                //message.To = model.Email;
                //message.From = "support@saaspro.net";
                //message.ResetToken = user.GenerateResetToken(TimeSpan.FromHours(24));
                //emailService.Send(message);

                return RedirectToAction("forgot-password").AndAlert(AlertType.Success,
                "Password Reset Information MailedSuccessfully!");
            }
            catch (System.Exception)
            {
                return RedirectToAction("forgot-password").AndAlert(AlertType.Danger,
                    "Invalid credentials");
            }
        }
       
    }
}
