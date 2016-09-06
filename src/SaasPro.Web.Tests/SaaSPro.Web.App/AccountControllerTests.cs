using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postal;
using SaaSPro.Common.Web;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web;
using SaaSPro.Web.Controllers;
using SaaSPro.Web.ViewModels;
using StructureMap;

namespace SaasPro.Web.Tests.SaaSPro.Web.App
{
	[TestClass]
	public class AccountControllerTests : TestsConfiguration.TestsConfiguration
	{
		private static AccountController _controller;
		private static User _user;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			Initialize();
			_user = InitializeUser();
			InitHttpContext();
			
			_controller = new AccountController(ObjectFactory.GetInstance<IUserService>(), 
				ObjectFactory.GetInstance<ICustomerService>());
		}

		[TestInitialize]
		public void TestInitialize()
		{
			if (HttpContext.Current == null)
			{
				InitHttpContext();
			}
			InitControllerContext(_controller, _user);

		}

		[TestMethod]
		public void T01_ChangePassword()
		{
			var authController = new AuthController(ObjectFactory.GetInstance<IUserService>(),
				ObjectFactory.GetInstance<ILoginManager>(), ObjectFactory.GetInstance<IEmailService>());
			InitControllerContext(authController, _user);
			authController.Login(new AuthLogInModel { Email = _user.Email, Password = "Qwerty123" });

			var result = _controller.ChangePassword() as ViewResult;
			Assert.IsNotNull(result);

			var model = new AccountChangePasswordModel
			{
				CurrentPassword = "",
				NewPassword = "Qwerty123New",
				ConfirmNewPassword = "Qwerty123New"
			};

			// Check validation
			Validate(model);
			Assert.AreNotEqual(0, Validate(model).Count, "Change password model validation failed");

			// Wrong current password
			model.CurrentPassword = "wrong password";
			var wrongResult = _controller.ChangePassword(model) as ViewResult;
			Assert.IsNotNull(wrongResult);
			Assert.IsFalse(wrongResult.ViewData.ModelState.IsValid);

			//Correct password
			model.CurrentPassword = "Qwerty123";
			_controller.ModelState.Clear();

			var postResult = _controller.ChangePassword(model);
			Assert.IsNotNull(postResult);
		}

		[TestMethod]
		public void T02_UpdateProfile()
		{
			var result = _controller.UpdateProfile() as ViewResult;
			Assert.IsNotNull(result);

			var model = result.Model as CustomersDetailsModel;
			Assert.IsNotNull(model);
			// Check validation
			model.Company = null;
			Validate(model);
			Assert.AreNotEqual(0, Validate(model).Count, "Update profile model validation failed");

			model.Company = "New Company name";
			var updateProfileResult = _controller.UpdateProfile(model);
			Assert.IsNotNull(updateProfileResult);
		}

		[TestMethod]
		public void T03_ChangeSecurityQuestions()
		{
			var result = _controller.ChangeSecurityQuestions() as ViewResult;
			Assert.IsNotNull(result);

			var model = result.Model as UsersUpdateSecurityQuestionsModel;
			Assert.IsNotNull(model);

			// Check validation
			Validate(model);
			Assert.AreEqual(0, Validate(model).Count);

			var changeSecurityQuestionsResult = _controller.ChangeSecurityQuestions(model) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(changeSecurityQuestionsResult);
            Assert.AreEqual(changeSecurityQuestionsResult.Message.AlertType, AlertType.Success);
		}


		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
