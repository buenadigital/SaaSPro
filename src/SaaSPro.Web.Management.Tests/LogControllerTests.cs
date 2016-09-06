using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Common.Web;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Management.Controllers;

namespace SaaSPro.Web.Management.Tests
{
	[TestClass]
	public class LogControllerTests : BaseControllerTests<LogController>
	{
		private static Log4NetLog _log;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			var log4NetRepository = Container.GetInstance<ILog4NetRepository>();
			_log = new Log4NetLog
			{
				Date = DateTime.Now,
				Exception = "Custom exception",
				Level = "Low",
				Logger = "Web.Main",
				Message = "Custom message"
			};
			log4NetRepository.Add(_log);
			Controller = new LogController(Container.GetInstance<ILog4NetService>());
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
			Assert.IsNotNull(result, "Log index page view result is null");
			Assert.IsNotNull(result.Model, "Log index page model is null");
			var model = result.Model as LogDashboardModel;
			Assert.IsNotNull(model, "Log index page model have incorrect type");
			Assert.AreEqual(1, model.LogTypes.Count, "Log index page model return incorrect data");
		}

		[TestMethod]
		public void T02_Details()
		{
			var result = Controller.Details(_log.Id, "Test log") as ViewResult;
			Assert.IsNotNull(result, "Log details page view result is null");
			Assert.IsNotNull(result.Model, "Log details page model is null");

			var model = result.Model as Log4NetLog;
			Assert.IsNotNull(model, "Log details page model have incorrect type");
			Assert.AreEqual(_log.Id, model.Id, "Log details page model return incorrect data");
		}

		[TestMethod]
		public void T03_Delete()
		{
			var result = Controller.Delete(_log.Id) as AlertResult<RedirectToRouteResult>;
			Assert.IsNotNull(result, "Log delete method work incorrect");
			Assert.AreEqual(result.Message.AlertType, AlertType.Success, "Log delete method result return not success result");
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
