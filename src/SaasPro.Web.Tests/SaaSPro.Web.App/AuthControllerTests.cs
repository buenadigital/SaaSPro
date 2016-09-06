using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postal;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web;
using SaaSPro.Web.Controllers;
using SaaSPro.Web.ViewModels;
using StructureMap;

namespace SaasPro.Web.Tests.SaaSPro.Web.App
{
	[TestClass]
	public class AuthControllerTests : TestsConfiguration.TestsConfiguration
	{
		private static AuthController _controller;
		private static User _user;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			Initialize();
			_user = InitializeUser();
			InitHttpContext();
			
			_controller = new AuthController(ObjectFactory.GetInstance<IUserService>(), 
				ObjectFactory.GetInstance<ILoginManager>(), ObjectFactory.GetInstance<IEmailService>());
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
		public void T01_Login()
		{
			var loginModel = new AuthLogInModel();

			Validate(loginModel);
			Assert.AreEqual(2, Validate(loginModel).Count, "Login model failed: count of fail validation is not equal 9");

			loginModel.Email = _user.Email;
			loginModel.Password = "Wrong password";

			Validate(loginModel);
			Assert.AreEqual(0, Validate(loginModel).Count, "Login model failed");

			// Wrong password
			var wrongResult = _controller.Login(loginModel) as ViewResult; 
			Assert.IsNotNull(wrongResult);
			Assert.IsFalse(wrongResult.ViewData.ModelState.IsValid);

			// Correct password
			loginModel.Password = "Qwerty123";
			_controller.ModelState.Clear();
			var result = _controller.Login(loginModel) as RedirectToRouteResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void T02_SecurityCheck()
		{
			var result = _controller.SecurityCheck() as ViewResult;
			Assert.IsNotNull(result);
			var model = result.Model as AuthSecurityCheckModel;
			Assert.IsNotNull(model);
			Assert.AreEqual(model.Question, "b");

			// Check validation
			Validate(model);
			Assert.AreNotEqual(0, Validate(model).Count, "Authentication security check model failed");
			model.Answer = "a";
			Assert.AreEqual(0, Validate(model).Count, "Authentication security check model failed");

			// Wrong answer
			var wrongPostResult = _controller.SecurityCheck(model) as ViewResult;
			Assert.IsNotNull(wrongPostResult);
			Assert.IsFalse(wrongPostResult.ViewData.ModelState.IsValid);

			// Correct answer
			model.Answer = "b";
			_controller.ModelState.Clear();

			var postResult = _controller.SecurityCheck(model);
			Assert.IsNotNull(postResult);
		}

		[TestMethod]
		public void T03_LogOut()
		{
			var result = _controller.LogOut() as RedirectToRouteResult;
			Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "login");
		}

		[TestMethod]
		public void T04_ForgotPassword()
		{
			var result = _controller.ForgotPassword() as ViewResult;
			Assert.IsNotNull(result);
			var model = result.Model as AuthForgottenPasswordModel;
			Assert.IsNull(model);

			model = new AuthForgottenPasswordModel {Email = "wrong@email.com"};
			// Wrong email
			var wrongPostResult = _controller.ForgotPassword(model) as ViewResult;
			Assert.IsNotNull(wrongPostResult);
			Assert.IsFalse(wrongPostResult.ViewData.ModelState.IsValid);

			// Correct email
			model.Email = _user.Email;
			_controller.ModelState.Clear();

			var correctResult = _controller.ForgotPassword(model) as RedirectToRouteResult;
			Assert.IsNotNull(correctResult);
			Assert.AreEqual(correctResult.RouteValues["action"], "forgotpasswordemail");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
