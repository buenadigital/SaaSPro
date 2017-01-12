using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Data;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Main.Controllers;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Web.Main.Tests
{
	[TestClass]
	public class PlansControllerTests : TestsControlerBase<PlansController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new PlansController(Container.GetInstance<IPlanService>(), Container.GetInstance<IEmailTemplatesRepository>());
			TestsDataInitialize.CreatePlans(Container.GetInstance<EFDbContext>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
		}

		[TestMethod]
		public void T01_Pricing()
		{
			var result = Controller.Pricing() as ViewResult;
			Assert.IsNotNull(result, "Pricing view result return null");
			var pricingModel = (result.Model as PricingModel);
			Assert.IsNotNull(pricingModel, "Pricing model return null");
			Assert.AreEqual(pricingModel.Plans.Count, 3, "Pricing model have incorrect validation");
		}

		[TestMethod]
		public void T02_SignUp()
		{
			var result = Controller.SignUp("Small") as ViewResult;
			Assert.IsNotNull(result, "SignUp view result return null");
			var pricingModel = (result.Model as SignUpModel);
			Assert.IsNotNull(pricingModel, "SignUp model return null");

			Validate(pricingModel);
			Assert.AreEqual(9, Validate(pricingModel).Count, "SignUp validation model failed: count of fail validation is not equal 9");

			pricingModel.Domain = "john-domain";
			pricingModel.FirstName = "John";
			pricingModel.LastName = "Smith";
			pricingModel.Email = "john@mail.com";
			pricingModel.Company = "JohnSmith Company";
			pricingModel.Password = "123456789";
			pricingModel.CardNumber = "4242424242424242";
			pricingModel.SecurityCode = "123";
			pricingModel.Expiration = "2099/12";
			pricingModel.ExpirationMonth = 12;
			pricingModel.ExpirationYear = 2099;

			Validate(pricingModel);
			Assert.AreEqual(0, Validate(pricingModel).Count, $"SignUp validation model failed:\n { string.Join("\n", Validate(pricingModel)) }" );
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
