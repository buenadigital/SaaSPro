using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Management.Controllers;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class DashboardControllerTests : BaseControllerTests<DashboardController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new DashboardController(Container.GetInstance<IDashboardService>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
			Login();
		}

		[TestMethod]
		public void T01_Index()
		{
			var result = Controller.Index() as ViewResult;
			Assert.IsNotNull(result, "Dashboard index page don't work correct");
			Assert.IsNotNull(result.Model, "Dashboard index page model is null");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
