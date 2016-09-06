using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Controllers;
using SaaSPro.Web.ViewModels;
using SaaSPro.Data.Repositories;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.App.Tests
{
    [TestClass]
	public class AuthControllerTests : TestsControlerBase<AuthController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new AuthController(Container.GetInstance<IUserService>(),
				Container.GetInstance<ILoginManager>(), Container.GetInstance<IEmailTemplatesRepository>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
		}

		[TestMethod]
		public void T01_Login()
		{
			var loginModel = new AuthLogInModel();

			Validate(loginModel);
			Assert.AreEqual(2, Validate(loginModel).Count, "Login model failed: count of fail validation is not equal 9");

			loginModel.Email = User.Email;
			loginModel.Password = "Wrong password";

			Validate(loginModel);
			Assert.AreEqual(0, Validate(loginModel).Count, "Login model failed");

			// Wrong password
			var wrongResult = Controller.Login(loginModel) as ViewResult; 
			Assert.IsNotNull(wrongResult, "Login with wrong password return null view result");
			Assert.IsFalse(wrongResult.ViewData.ModelState.IsValid, "Login with wrong password model has no errors");

			// Correct password
			loginModel.Password = "Qwerty123";
			Controller.ModelState.Clear();
			var result = Controller.Login(loginModel) as RedirectToRouteResult;
			Assert.IsNotNull(result, "Login with correct password return null view result");
		}

		[TestMethod]
		public void T02_SecurityCheck()
		{
			var result = Controller.SecurityCheck() as ViewResult;
			Assert.IsNotNull(result, "Security check view result is null");
			var model = result.Model as AuthSecurityCheckModel;
			Assert.IsNotNull(model, "Security check vmodel is null");
			Assert.AreEqual(model.Question, "b", "Security check model return incorrect data");

			// Check validation
			Validate(model);
			Assert.AreNotEqual(0, Validate(model).Count, "Authentication security check model failed");
			model.Answer = "a";
			Assert.AreEqual(0, Validate(model).Count, "Authentication security check model failed");

			// Wrong answer
			var wrongPostResult = Controller.SecurityCheck(model) as ViewResult;
			Assert.IsNotNull(wrongPostResult, "Security check with wrong answer return view result is null");
			Assert.IsFalse(wrongPostResult.ViewData.ModelState.IsValid, "Security check with wrong answer model has incorrect model state");

			// Correct answer
			model.Answer = "b";
			Controller.ModelState.Clear();

			var postResult = Controller.SecurityCheck(model);
			Assert.IsNotNull(postResult, "Security check with correct answer return view result is null");
		}

		[TestMethod]
		public void T03_LogOut()
		{
			var result = Controller.LogOut() as RedirectToRouteResult;
			Assert.IsNotNull(result, "Logout method return view result equals null");
			Assert.AreEqual(result.RouteValues["action"], "login", "Logout method have incorrect redirect");
		}

		[TestMethod]
		public void T04_ForgotPassword()
		{
			var result = Controller.ForgotPassword() as ViewResult;
			Assert.IsNotNull(result, "Forgot password view result is null");
			var model = result.Model as AuthForgottenPasswordModel;
			Assert.IsNull(model, "Forgot password model is null");

			model = new AuthForgottenPasswordModel {Email = "wrong@email.com"};
			// Wrong email
			var wrongPostResult = Controller.ForgotPassword(model) as ViewResult;
			Assert.IsNotNull(wrongPostResult, "Forgot password with wrong email view result is null");
			Assert.IsFalse(wrongPostResult.ViewData.ModelState.IsValid, "Forgot password  model with wrong email have incorrect validation");

			// Correct email
			model.Email = User.Email;
			Controller.ModelState.Clear();

			var correctResult = Controller.ForgotPassword(model) as RedirectToRouteResult;
			Assert.IsNotNull(correctResult, "Forgot password with correct email view result is null");
			Assert.AreEqual(correctResult.RouteValues["action"], "forgotpasswordemail", "Forgot password with correct email have incorrect redirect");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
