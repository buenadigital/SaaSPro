using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Front.Controllers;
using StructureMap;

namespace SaasPro.Web.Tests.SaaSPro.Web.Main.Tests
{
	[TestClass]
	public class PlansControllerTests : TestsConfiguration.TestsConfiguration
	{
		private static PlansController _controller;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			Initialize();
			InitializePlans();

			_controller = new PlansController(ObjectFactory.GetInstance<IPlanService>());
		}

		[TestMethod]
		public void T01_Pricing()
		{
			var result = _controller.Pricing() as ViewResult;
			Assert.IsNotNull(result);
			var pricingModel = (result.Model as PricingModel);
			Assert.IsNotNull(pricingModel);
			Assert.AreEqual(pricingModel.Plans.Count, 3);
		}

		[TestMethod]
		public void T02_SignUp()
		{
			var result = _controller.SignUp("Small") as ViewResult;
			Assert.IsNotNull(result);
			var pricingModel = (result.Model as SignUpModel);
			Assert.IsNotNull(pricingModel);

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
