using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using StructureMap;
using StructureMap.Web.Pipeline;
using System;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SaaSPro.Web
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
            AutoMapperConfig.Register();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.PrimarySid;
            MvcHandler.DisableMvcResponseHeader = true;

            EmailServiceFactory.InitializeEmailServiceFactory(ObjectFactory.GetInstance<IMailService>());
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory(ObjectFactory.GetInstance<IApplicationSettings>()); 
            LoggingFactory.InitializeLogFactory(ObjectFactory.GetInstance<ILogger>());
            StripeFactory.InitializeStripeFactory(ObjectFactory.GetInstance<IStripeService>());
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}