using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Controllers;

namespace SaaSPro.Web.App.Tests
{
	[TestClass]
	public class DashboardControllerTests : TestsControlerBase<DashboardController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new DashboardController(Container.GetInstance<ICustomerDashboardService>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
		}

		[TestMethod]
		public void T01_ChangePassword()
		{
			var result = Controller.Index(new PagingCommand()) as ViewResult;
			Assert.IsNotNull(result, "Change password view result return null");
			Assert.IsNotNull(result.Model, "Change password model is null");

			var model = result.Model as CustomerDashboardModel;
			Assert.IsNotNull(model, "Change password model has incorrect type");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
