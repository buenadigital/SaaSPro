using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Tests
{
	//TODO: Write test on Notes
	[TestClass]
	public class CustomerServiceTests : TestsConfiguration
	{
		private ICustomerService _customerService => Container.GetInstance<ICustomerService>();

		private static Customer _customer;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();

			var dbContext = Container.GetInstance<EFDbContext>();

			_customer = new Customer("John", "john-domain", "Join-LTD");
			// Create customer in stripe
			var stripeCustomer = StripeFactory.GetStripeService().CreateCustomer("John Smith",
				"john@email.com");
			_customer.PaymentCustomerId = stripeCustomer.Id;
			dbContext.Customers.Add(_customer);
			dbContext.SaveChanges();

			var user = new User(_customer, "user@email.com", "John", "Smith", "Qwerty123");
			dbContext.Users.Add(user);

			_customer.AdminUser = user;
			dbContext.Customers.Attach(_customer);
			dbContext.SaveChanges();
		}

		[TestMethod]
		public void T01_GetAndSaveCustomer()
		{
			var customer = _customerService.GetCustomerDetails(_customer.Id);
			Assert.IsNotNull(customer, "Get customer details method don't work correct");

			customer.Company = "New Company Name";
			_customerService.Save(customer);
			customer = _customerService.GetCustomerDetails(_customer.Id);

			Assert.AreEqual(customer.Company, "New Company Name", "Save customer method don't work correct");
		}

		[TestMethod]
		public void T02_ResetPassword()
		{
			var guid = _customerService.ResetPassword(_customer.Id, new UsersResetPasswordModel { NewPassword = "123Qwerty", ConfirmNewPassword = "123Qwerty", Id = _customer.Id });
			Assert.IsNotNull(guid, "Reset password don't work");
		}

		[TestMethod]
		public void T03_SetupStripe()
		{
			var setupStripService = _customerService.SetupStripe(_customer.Id);
			Assert.IsNotNull(setupStripService, "Setup stripe don't work");
		}

		[TestMethod]
		public void T04_CheckUserName()
		{
			Assert.AreEqual("Not Available", _customerService.CheckUserName("john-domain"), "Check user name don't work correct, for not existing host return 'Available'");
			Assert.AreEqual("Available", _customerService.CheckUserName("john-domain2"), "Check user name don't work correct, for existing host return 'Not Available'");
		}

		[ClassCleanup()]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
