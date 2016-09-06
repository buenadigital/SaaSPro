using System.Web.Mvc;
using System.Web.Routing;

namespace SaaSPro.Web.Management
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Scheduler",
                url: "scheduler",
                defaults: new { controller = "Scheduler", action = "Index" }
            );

            routes.MapRoute(
                name: "SchedulerJob",
                url: "scheduler/{action}/{groupName}/{jobName}",
                defaults: new { controller = "Scheduler" }
            );

            routes.MapRoute(
                name: "SetupStripe",
                url: "customers/setup-stripe/{customerId}",
                defaults: new { controller = "Customers", action = "SetupStripe" }
            );

            routes.MapRoute(
                name: "ClosePlan",
                url: "customers/closeplan/{customerId}",
                defaults: new { controller = "Customers", action = "ClosePlan" }
            );

            routes.MapRoute(
               name: "EmailTemplates",
               url: "email-templates/{action}/{id}",
               defaults: new { controller = "EmailTemplates", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "StripeWebHook",
               url: "stripewebhook/{action}",
               defaults: new { controller = "StripeWebHook", action = "Index" }
            );

            //routes.MapRoute(
            //   name: "ForgotPassword",
            //   url: "auth/forgot-password/{action}",
            //   defaults: new { controller = "Auth", action = "ForgotPassword", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}