using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using StructureMap;
using log4net.Config;
using StructureMap.Web.Pipeline;

namespace SaaSPro.Web.Front
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            StructureMapConfig.Register();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, ObjectFactory.Container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            AutoMapperConfig.Register();


            EmailServiceFactory.InitializeEmailServiceFactory(ObjectFactory.GetInstance<IMailService>());
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory(ObjectFactory.GetInstance<IApplicationSettings>());
            LoggingFactory.InitializeLogFactory(ObjectFactory.GetInstance<ILogger>());
            StripeFactory.InitializeStripeFactory(ObjectFactory.GetInstance<IStripeService>());
  
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));

            BundleTable.EnableOptimizations = ApplicationSettingsFactory.GetApplicationSettings().EnableOptimizations;

#if DEBUG
            // HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}