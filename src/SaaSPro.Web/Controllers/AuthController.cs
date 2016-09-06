using System.Configuration;
using SaaSPro.Common.Helpers;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Domain;
using SaaSPro.Web.ViewModels;
using Postal;
using System;
using System.Web.Mvc;
using SaaSPro.Common;
using SaaSPro.Services.Mapping;
using SaaSPro.Services.Helpers;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Data.Repositories;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : SaaSProControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginManager _loginManager;
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;

        public AuthController(IUserService userService, ILoginManager loginManager, IEmailTemplatesRepository emailTemplatesRepository)
        {
            Ensure.Argument.NotNull(loginManager, nameof(loginManager));
            Ensure.Argument.NotNull(emailTemplatesRepository, nameof(emailTemplatesRepository));

            _userService = userService;
            _loginManager = loginManager;
            _emailTemplatesRepository = emailTemplatesRepository;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            return View(new AuthLogInModel { ReturnUrl = returnUrl });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(AuthLogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = FindUser(model.Email);

            if (user == null || !user.ValidatePassword(model.Password))
            {
                ModelState.AddModelError("", "The e-mail address and password you entered do not match any accounts on record.");
                return View();
            }

            _loginManager.SetLoginSessionUserId(user.Id);
            return RedirectToAction("securitycheck", new { returnUrl = model.ReturnUrl });
        }

        [HttpGet]
        public ActionResult SecurityCheck(string returnUrl = null)
        {
            var user = GetLoginSessionUser();

            if (user == null)
            {
                return RedirectToAction("login", new { returnUrl = returnUrl });
            }

            var securityQuestion = user.GetRandomSecurityQuestion();

            var model = new AuthSecurityCheckModel { Id = securityQuestion.Id, Question = securityQuestion.Question };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SecurityCheck(AuthSecurityCheckModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = GetLoginSessionUser();

            if (user == null)
            {
                return RedirectToAction("login", new { returnUrl = model.ReturnUrl });
            }

            var question = user.GetSecurityQuestion(model.Id);

            if (!question.ValidateAnswer(model.Answer))
            {
                ModelState.AddModelError("", "The provided answer was incorrect.");
                return View(model);
            }

            user.AuditLogin(Request.UserHostAddress);
            return AuthorizeAndRedirect(user.Email, model.ReturnUrl);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            _loginManager.LogOut();
            return RedirectToAction("login");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(AuthForgottenPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = FindUser(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "The e-mail address you entered does not match any accounts on record.");
                return View();
            }

            var resetToken = user.GenerateResetToken(TimeSpan.FromHours(24));
            var emailTemplate = _emailTemplatesRepository.GetTemplateByName(Utilities.ToDescriptionString(EmailTemplateCode.ForgotPassword));

            model.ResetPasswordUrl = $"{Request.Url.Authority}/auth/resetpassword?email={model.Email}&resettoken={resetToken}";

            emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, model);
            var mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);

            var sent = EmailServiceFactory.GetEmailService().SendMail(mailMessage);

            return RedirectToAction("forgotpasswordemail");
        }

        [HttpGet]
        public ActionResult ForgotPasswordEmail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string email, string resetToken)
        {
            return View(new AuthResetPasswordModel { Email = email, ResetToken = resetToken });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(AuthResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = FindUser(model.Email);
            if (user != null && user.ResetPassword(model.ResetToken, model.NewPassword))
            {
                return AuthorizeAndRedirect(model.Email, "~/").AndAlert(AlertType.Success, "Password Changed.", "Your password was changed successfully.");
            }

            ModelState.AddModelError("", "The password reset attempt was unsuccessful. Please try again.");
            return View();
        }

        private ActionResult AuthorizeAndRedirect(string email, string returnUrl, bool createPersistentCookie = false)
        {
            _loginManager.LogIn(email, createPersistentCookie);

            if (returnUrl.IsNotNullOrEmpty() && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // otherwise redirect to dashboard home
            return RedirectToAction("index", "dashboard");
        }

        private User FindUser(string email)
        {
            GetUserRequest request = new GetUserRequest
            {
                Email = email,
                CustomerId = Customer.CustomerId
            };

            return _userService.GetUser(request);
        }

        private User GetLoginSessionUser()
        {
            Guid? userId = _loginManager.GetLoginSessionUserId();
            if (userId.HasValue)
            {
                return _userService.GetLoginSessionUser(userId.Value);
            }

            return null;
        }
    }
}
