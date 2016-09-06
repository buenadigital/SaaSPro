using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using StructureMap;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using SaaSPro.Web.Common;
using StructureMap.Web.Pipeline;

namespace SaaSPro.Web.Management
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            StructureMapConfig.Register();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, ObjectFactory.Container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));

            AutoMapperConfig.Register();

            EmailServiceFactory.InitializeEmailServiceFactory(ObjectFactory.GetInstance<IMailService>());
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory(ObjectFactory.GetInstance<IApplicationSettings>());
            LoggingFactory.InitializeLogFactory(ObjectFactory.GetInstance<ILogger>());
            StripeFactory.InitializeStripeFactory(ObjectFactory.GetInstance<IStripeService>());

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}