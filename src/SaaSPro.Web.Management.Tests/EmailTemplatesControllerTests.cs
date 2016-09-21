using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Common.Web;
using SaaSPro.Data;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Management.Controllers;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class EmailTemplatesControllerTests : BaseControllerTests<EmailTemplatesController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
            TestsDataInitialize.CreateEmailTemplates(Container.GetInstance<EFDbContext>());
            Controller = new EmailTemplatesController(Container.GetInstance<IEmailTemplatesService>());
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
			Login();
		}

		[TestMethod]
		public void T01_Create()
		{
			var model = new EmailTemplateDetailsModel();
			Validate(model);
			Assert.AreEqual(4, Validate(model).Count, "Create email templates validation model not work correct");

			model.TemplateName = "TestEmail";
			model.Subject = "Test";
			model.FromEmail = "Test email";
			model.Body = "<html><body></body></html>";

			var result = Controller.Create(model) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Create email templates method don't work correct");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Create email templates method return not success result");
		}

		[TestMethod]
		public void T02_Index()
		{
			var result = Controller.Index(new PagingCommand()) as ViewResult;
			Assert.IsNotNull(result, "Email templates index page don't work correct");
			Assert.IsNotNull(result.Model, "Email templates index page model is null");
		}

		[TestMethod]
		public void T03_Details()
		{
			var dbContext = Container.GetInstance<EFDbContext>();
			var emailTemplate = dbContext.EmailTemplates.FirstOrDefault();
			Assert.IsNotNull(emailTemplate, "Email templates data is null");

			var result = Controller.Details(emailTemplate.Id) as ViewResult;
			Assert.IsNotNull(result, "Email templates details page view result is null");
			var model = result.Model as EmailTemplateDetailsModel;
			Assert.IsNotNull(model, "Email templates details model has incorrect type");

			model.Subject = null;
			Validate(model);
			Assert.AreEqual(1, Validate(model).Count, "Email templates details model is incorrect");

			model.Subject = "new Subject";
			var saveResult = Controller.Details(model) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(saveResult, "Email templates details save method works incorrect");
			Assert.AreEqual(saveResult.Message.AlertType, AlertType.Success, "Email templates details save method return not success result");
		}

		[TestMethod]
		public void T04_Delete()
		{
			var dbContext = Container.GetInstance<EFDbContext>();
			var emailTemplate = dbContext.EmailTemplates.FirstOrDefault();
			Assert.IsNotNull(emailTemplate, "Email templates delete method works incorrect");

			var result = Controller.Delete(emailTemplate.Id) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result);
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Email templates delete method return not success result");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
