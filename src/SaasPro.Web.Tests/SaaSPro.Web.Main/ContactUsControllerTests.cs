using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Data.Repositories;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Front.Controllers;
using StructureMap;

namespace SaasPro.Web.Tests.SaaSPro.Web.Main.Tests
{
	[TestClass]
	public class ContactUsControllerTests : TestsConfiguration.TestsConfiguration
	{
		private static ContactUsController _controller;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			Initialize();
			InitializePlans();

			_controller = new ContactUsController(ObjectFactory.GetInstance<IEmailTemplatesRepository>());
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

			var result = _controller.Index("Small", contactModel) as ViewResult;
			Assert.IsNull(result);
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
