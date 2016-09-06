using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.PlanService;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Tests
{
	[TestClass]
	public class PlanServiceTests : TestsConfiguration
	{
		private IPlanService _planService => Container.GetInstance<IPlanService>();

		private static List<Plan> _plans = new List<Plan>();

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			_plans = TestsDataInitialize.CreatePlans(Container.GetInstance<EFDbContext>());
		}

		[TestMethod]
		public void T01_PlanSignUp()
		{
			var respounce = _planService.PlanSignUp(new PlanSignUpRequest
			{
				PlanName = "Medium",
				SignUpModel = new SignUpModel
				{
					FirstName = "John",
					LastName = "Smith",
					Password = "Password1",
					Company = "Global John Smith Company",
					Email = "john@smith.com",
					Domain = "john-smith-domain",
					CardNumber = "4242424242424242",
					SecurityCode = "123",
					ExpirationMonth = 01,
					ExpirationYear = 2099,
					Expiration = "01/2099"
				}
			});

			if (respounce.HasError)
			{
				Assert.Fail("Response returns with errors");
			}

			var customerRepository = Container.GetInstance<ICustomerRepository>();
			var customers = customerRepository.Query();
			if (customers.Count() != 1)
			{
				Assert.Fail("Customer was not created or created more then one");
			}

			var cusomer = customers.FirstOrDefault();
			Assert.IsNotNull(cusomer);

			if (cusomer.AdminUser == null)
			{
				Assert.Fail("User was not created");
			}

			if (!cusomer.AdminUser.Roles.Any())
			{
				Assert.Fail("Roles was not created");
			}

			if (!cusomer.AdminUser.SecurityQuestions.Any())
			{
				Assert.Fail("Security questions was not created");
			}

			Assert.IsTrue(true);
		}

		[TestMethod]
		public void T02_IsHostNameAvailable()
		{
			Assert.IsTrue(!_planService.IsHostNameAvailable("john-smith-domain"), "For existing host return true");
			Assert.IsTrue(_planService.IsHostNameAvailable("new-john-smith-domain"), "For not existing host return false");
		}

		[TestMethod]
		public void T03_PlanCodeExist()
		{
			Assert.IsTrue(_planService.PlanCodeExist("Super"), "For existing plan return false");
			Assert.IsTrue(!_planService.PlanCodeExist("ExtraSuper"), "For not existing plan return true");
		}

		[TestMethod]
		public void T04_PricingModel()
		{
			var price = _planService.GetPricing();
			Assert.AreEqual(_plans.Count, price.Plans.Count, "Expected plan count is not much with real");
			Assert.IsTrue(ScrambledEquals(_plans, price.Plans), "Expected plan id's is not much with real");
		}

		[TestMethod]
		public void T05_PlanModification()
		{
			_planService.Add(new PlanAddModel
			{
				Enabled = true,
				Name = "Extra",
				OrderIndex = 10,
				Period = "",
				PlanCode = "extra",
				Price = 1466
			});

			var plan = _planService.List().FirstOrDefault(t=>t.PlanCode == "extra");
			Assert.IsNotNull(plan, "Add plan method don't work correct");

			plan.Price = 1699;
			_planService.Update(plan);
			plan = _planService.Get(plan.Id);
			Assert.AreEqual(1699, plan.Price, "Update plan method don't work correct");

			_planService.Delete(plan);
			Assert.IsFalse(_planService.PlanCodeExist("extra"), "Delete plan method don't work correct");
		}

		[ClassCleanup()]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
