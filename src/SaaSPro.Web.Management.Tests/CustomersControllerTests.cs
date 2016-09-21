using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Management.Controllers;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class CustomersControllerTests : BaseControllerTests<CustomersController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new CustomersController(Container.GetInstance<ICustomerService>());
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
			var result = Controller.GetCustomersData(0, 10, null, null, null);
			Assert.IsNotNull(result, "GetCustomersData method don't work correct");
			var customerJsonResult = result.Data as CustomerJsonResult;
			Assert.IsNotNull(customerJsonResult, "GetCustomersData method return incorrect json result");
			Assert.AreEqual(1, customerJsonResult.total, "GetCustomersData method return incorrect value for field 'total'");
			Assert.AreEqual(1, customerJsonResult.rows.Count(), "GetCustomersData method return incorrect value for field 'rows'");

			var customer = customerJsonResult.rows.FirstOrDefault();
			Assert.IsNotNull(customer, "GetCustomersData method field rows return null");
			Assert.AreEqual(User.CustomerId, customer.Id, "GetCustomersData method field rows return incorrect rows");
		}

		[TestMethod]
		public void T02_Provision()
		{
			var model = new CustomersProvisionModel();
			Validate(model);
			Assert.AreEqual(7, Validate(model).Count, "Customers provision model validation is incorrect");

			model.Domain = "alex-domain";
			model.FirstName = "Alex";
			model.LastName = "Sanches";
			model.Email = "foo@bar.com";
			model.Company = "Sanches LTD";
			model.Password = "Password1";
			model.ConfirmPassword = "Password1";

			var result = Controller.Provision(model) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Customers provision method don't work correct");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Customers provision method return not success result");
		}

		[TestMethod]
		public void T03_Details()
		{
			var result = Controller.Details(User.CustomerId) as ViewResult;
			Assert.IsNotNull(result, "Customers details page view result is null");
			var model = result.Model as CustomersDetailsModel;
			Assert.IsNotNull(model, "Customers details page model is null");
			Assert.AreEqual(User.CustomerId, model.Id, "Customers details page model return incorrect data");

			model.Company = null;
			Validate(model);
			Assert.AreEqual(3, Validate(model).Count, "Customers details model validation is incorrect");

			model.Company = "New Sanches LTD";
			model.Password = "Password2";
			model.ConfirmPassword = "Password2";
			var saveResult = Controller.Details(model) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(saveResult, "Customers details save method don't work correct");
			Assert.AreEqual(saveResult.Message.AlertType, AlertType.Success, "Customers details save method return not success result");
		}

		[TestMethod]
		public void T04_Delete()
		{
			var result = Controller.Delete(User.CustomerId) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Customers delete method don't work correct");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Customers delete save method return not success result");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
