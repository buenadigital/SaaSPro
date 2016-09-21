using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Common.Web;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Management.Controllers;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class PlansControllerTests : BaseControllerTests<PlansController>
	{
		private static List<Plan> _plans;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			_plans = TestsDataInitialize.CreatePlans(Container.GetInstance<EFDbContext>());
			Controller = new PlansController(Container.GetInstance<IPlanService>());
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
			var result = Controller.Index(new PagingCommand()) as ViewResult;
			Assert.IsNotNull(result, "Plans index page view result is null");
			Assert.IsNotNull(result.Model, "Plans index page model is null");
			var model = result.Model as PlansListModel;
			Assert.IsNotNull(model, "Plans index page model has incorrect type");
			Assert.AreEqual(3, model.Plans.Count, "Plans index page return incorrect data");
		}

		[TestMethod]
		public void T02_Add()
		{
			var planModel = new PlanAddModel();
			Validate(planModel);
			Assert.AreEqual(3, Validate(planModel).Count, "Plans add validation is incorrect");

			planModel.Name = "Small";
			planModel.Price = 1699;
			planModel.Period = "monthly";
			planModel.OrderIndex = 1;
			planModel.PlanCode = "Small";
			planModel.Enabled = true;

			var result = Controller.Add(planModel) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Plans add method work incorrect");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Plans add method return not success result");
		}

		[TestMethod]
		public void T03_Update()
		{
			var plan = _plans.FirstOrDefault();
			Assert.IsNotNull(plan, "Plans list is null");

			//plan.Name += "NEW";
			//var result = Controller.Update(plan.Id) as ViewResult;
			//Assert.IsNotNull(result, "Update plan method don't work correct");
		}

		[TestMethod]
		public void T04_Delete()
		{
			var plan = _plans.FirstOrDefault();
			Assert.IsNotNull(plan, "Plans list is null");
			var result = Controller.Delete(plan.Id) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Delete method don't work correct");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Delete method return not success result");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
