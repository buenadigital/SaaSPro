using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Common.Web;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Management.Controllers;
using SaaSPro.Web.Management.ViewModels;
using StructureMap;

namespace SaasPro.Web.Tests.SaaSPro.Web.Management
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
			_controller = new AuthController(ObjectFactory.GetInstance<IUserService>());
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
			var loginModel = new LoginModel();

			Validate(loginModel);
			Assert.AreEqual(2, Validate(loginModel).Count, "Login model failed: count of fail validation is not equal 9");

			loginModel.Username = _user.Email;
			loginModel.Password = "Wrong password";

			Validate(loginModel);
			Assert.AreEqual(0, Validate(loginModel).Count, "Login model failed");

			// Wrong password
			var wrongResult = _controller.LogIn(loginModel) as AlertResult<RedirectToRouteResult>; 
			Assert.IsNotNull(wrongResult);
			Assert.AreEqual(wrongResult.Message.AlertType, AlertType.Danger);
			Assert.AreEqual(wrongResult.Result.RouteValues["action"], "login");

			var t = ObjectFactory.GetInstance<EFDbContext>();
			var tt = t.Customers;

			// Correct password
			//TODO: before production change user: admin password: Password1, uncoment buenadigital
			loginModel.Username = "admin";
            loginModel.Password = "Password1";
			_controller.ModelState.Clear();
			var result = _controller.LogIn(loginModel) as RedirectToRouteResult;
			Assert.IsNotNull(result);
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
