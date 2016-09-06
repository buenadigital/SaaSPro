using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Management.Controllers;
using SaaSPro.Web.Management.ViewModels;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class BaseControllerTests<T> : TestsControlerBase<T> where T: Controller
	{
		protected LoginModel LoginModel = new LoginModel { Username = "admin", Password = "Password1" };

		protected ActionResult Login()
		{
			var controller = new AuthController(Container.GetInstance<IUserService>());
			return controller.LogIn(LoginModel);
		}
	}
}
