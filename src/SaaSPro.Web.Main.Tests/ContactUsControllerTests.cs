using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Front.Controllers;

namespace SaaSPro.Web.Main.Tests
{
	[TestClass]
	public class ContactUsControllerTests : TestsControlerBase<ContactUsController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new ContactUsController(Container.GetInstance<IEmailTemplatesRepository>());
			TestsDataInitialize.CreatePlans(Container.GetInstance<EFDbContext>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
		}

		[TestMethod]
		public void T01_Index()
		{
			var contactModel = new ContactModel();
			Validate(contactModel);
			Assert.AreEqual(2, Validate(contactModel).Count, "Contact model validation model failed: count of fail validation is not equal 9");

			contactModel.Message = "test message";
			contactModel.Email = "test@mail.com";
			Assert.AreEqual(0, Validate(contactModel).Count, $"Contact model validation model failed:\n { string.Join("\n", Validate(contactModel)) }");

			var result = Controller.Index("Small", contactModel) as ViewResult;
			Assert.IsNull(result);
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
