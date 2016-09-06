using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Web;

namespace SaasPro.TestsConfiguration
{
	[TestClass]
	public abstract class TestsControlerBase<T> : TestsConfiguration where T : Controller
	{
		public static User User { get; set; }
		public static T Controller;

		public new static void ClassInitialize()
		{
			TestsConfiguration.ClassInitialize();
			User = TestsDataInitialize.CreateUser(Container.GetInstance<EFDbContext>());
			var httpContextInitialize = new TestsHttpContextInitialize(new Uri("http://john-domain.saaspro.net/"), User,
				Container.GetInstance<ICustomerHost>());
			HttpContext.Current = httpContextInitialize.GetTestHttpContext();
		}

		public virtual void TestInitialize()
		{
			var httpContextInitialize = new TestsHttpContextInitialize(new Uri("http://john-domain.saaspro.net/"), User,
				Container.GetInstance<ICustomerHost>());

			if (HttpContext.Current == null)
			{
				HttpContext.Current = httpContextInitialize.GetTestHttpContext();
			}
			
			Controller.ControllerContext = new ControllerContext(httpContextInitialize.GetTestsHttpContextBase(),
				new RouteData(), Controller);
		}
	}
}
