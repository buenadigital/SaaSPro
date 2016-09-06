using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Management.Controllers;
using SaaSPro.Web.Management.ViewModels;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class AuthControllerTests : BaseControllerTests<AuthController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new AuthController(Container.GetInstance<IUserService>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
			Login();
		}

		[TestMethod]
		public void T01_Login()
		{
			var loginModel = new LoginModel();

			Validate(loginModel);
			Assert.AreEqual(2, Validate(loginModel).Count, "Login model failed: count of fail validation is not equal 9");

			loginModel.Username = User.Email;
			loginModel.Password = "Wrong password";

			Validate(loginModel);
			Assert.AreEqual(0, Validate(loginModel).Count, "Login model failed");

			// Wrong password
			var wrongResult = Controller.LogIn(loginModel) as AlertResult<RedirectToRouteResult>; 
			Assert.IsNotNull(wrongResult, "Login with wrong password model return null");
			Assert.AreEqual(wrongResult.Message.AlertType, AlertType.Danger, "Login with wrong password model return incorrect message type");
			Assert.AreEqual(wrongResult.Result.RouteValues["action"], "login", "Login with wrong password method has incorrect redirect action");

			// Correct password
			//TODO: before production change user: admin password: Password1, uncoment buenadigital
			var result = Login() as RedirectToRouteResult;
			Assert.IsNotNull(result, "Login with correct password method return null value");
		}

		[TestMethod]
		public void T02_ForgotPassword()
		{
			var forgotPasswordModel = new ForgotPasswordModel();
			Validate(forgotPasswordModel);
			Assert.AreEqual(1, Validate(forgotPasswordModel).Count, "Forgot password model failed: count of fail validation is not equal 1");

			// Wrong user name
			forgotPasswordModel.Username = "admin2";
			var wrongResult = Controller.ForgotPassword(forgotPasswordModel) as ViewResult;
			Assert.IsNotNull(wrongResult, "Forgot password with wrong name view result is null");
			Assert.IsFalse(wrongResult.ViewData.ModelState.IsValid, "Forgot password with wrong name model has incorrect validation");

			// Correct user name
			Controller.ModelState.Clear();
			forgotPasswordModel.Username = User.Email;
			var result = Controller.ForgotPassword(forgotPasswordModel) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Forgot password with correct name view result is null");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Forgot password method with correct name return not success result");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
