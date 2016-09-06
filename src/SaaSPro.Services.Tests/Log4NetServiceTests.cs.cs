using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;

namespace SaaSPro.Services.Tests
{
	[TestClass]
	public class Log4NetServiceTests : TestsConfiguration
	{
		private ILog4NetService _log4NetService => Container.GetInstance<ILog4NetService>();

		private static Log4NetLog _log;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();

			var dbContext = Container.GetInstance<EFDbContext>();
			_log = dbContext.Log4NetLogs.Add(new Log4NetLog
			{
				Date = DateTime.Now,
				Exception = "Custom Dashboard exception",
				Level = "Low",
				Logger = "Test",
				Message = "Custom message"
			});
			dbContext.SaveChanges();

		}

		[TestMethod]
		public void T01_Dashboard()
		{
			var dashboardLog = _log4NetService.Dashboard();
			Assert.IsNotNull(dashboardLog);
			Assert.AreEqual(dashboardLog.Applications.Count, 1);
		}

		[TestMethod]
		public void T02_Detail()
		{
			var detail = _log4NetService.Detail(_log.Id);
			Assert.IsNotNull(detail);
		}

		[TestMethod]
		public void T02_DeleteAll()
		{
			_log4NetService.DeleteAll("Test");
			var dbContext = Container.GetInstance<EFDbContext>();
			Assert.IsFalse(dbContext.Log4NetLogs.Any());
		}

		[ClassCleanup()]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
