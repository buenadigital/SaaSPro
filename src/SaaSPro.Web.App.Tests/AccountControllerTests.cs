using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postal;
using SaasPro.TestsConfiguration;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Controllers;
using SaaSPro.Web.ViewModels;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Web.App.Tests
{
	[TestClass]
	public class AccountControllerTests : TestsControlerBase<AccountController>
	{

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new AccountController(Container.GetInstance<IUserService>(),
				Container.GetInstance<ICustomerService>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
		}

		[TestMethod]
		public void T01_ChangePassword()
		{
			var authController = new AuthController(Container.GetInstance<IUserService>(),
				Container.GetInstance<ILoginManager>(), Container.GetInstance<IEmailTemplatesRepository>());
			authController.ControllerContext = Controller.ControllerContext;
			authController.Login(new AuthLogInModel {Email = User.Email, Password = "Qwerty123"});

			var result = Controller.ChangePassword() as ViewResult;
			Assert.IsNotNull(result, "Change password view result is null");

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
			var wrongResult = Controller.ChangePassword(model) as ViewResult;
			Assert.IsNotNull(wrongResult, "Change password view result with wrong password return null");
			Assert.IsFalse(wrongResult.ViewData.ModelState.IsValid, "Change password model with wrong password has incorrect validation");

			//Correct password
			model.CurrentPassword = "Qwerty123";
			Controller.ModelState.Clear();

			var postResult = Controller.ChangePassword(model);
			Assert.IsNotNull(postResult, "Change password model with correct password has incorrect validation");
		}

		[TestMethod]
		public void T02_UpdateProfile()
		{
			var result = Controller.UpdateProfile() as ViewResult;
			Assert.IsNotNull(result, "Update profile method view result is null");

			var model = result.Model as CustomersDetailsModel;
			Assert.IsNotNull(model, "Update profile model is null");
			// Check validation
			model.Company = null;
			Validate(model);
			Assert.AreNotEqual(0, Validate(model).Count, "Update profile model validation failed");

			model.Company = "New Company name";
			var updateProfileResult = Controller.UpdateProfile(model);
			Assert.IsNotNull(updateProfileResult, "Update profile method result is null");
		}

		[TestMethod]
		public void T03_ChangeSecurityQuestions()
		{
			var result = Controller.ChangeSecurityQuestions() as ViewResult;
			Assert.IsNotNull(result, "Change security questions view result is null");

			var model = result.Model as UsersUpdateSecurityQuestionsModel;
			Assert.IsNotNull(model, "Change security questions model is null");

			// Check validation
			Validate(model);
			Assert.AreEqual(0, Validate(model).Count, "Change security questions model validation is incorrect");

			var changeSecurityQuestionsResult = Controller.ChangeSecurityQuestions(model) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(changeSecurityQuestionsResult, "Change security questions method is incorrect");
			Assert.AreEqual(changeSecurityQuestionsResult.Message.AlertType, AlertType.Success, "Change security questions model has incorrect message type");
		}


		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
