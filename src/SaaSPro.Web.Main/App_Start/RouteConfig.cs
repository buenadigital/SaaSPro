using System.Web.Mvc;
using System.Web.Routing;
using LowercaseRoutesMVC;

namespace SaaSPro.Web.Main
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase(
                name: "SignUp",
                url: "plan/signup/{plan}",
                defaults: new { controller = "Plan", action = "SignUp" });

            routes.MapRouteLowercase(
              name: "Tour",
              url: "tour",
              defaults: new { controller = "Tour", action = "Index" });

            routes.MapRouteLowercase(
               name: "Pricing",
               url: "pricing",
               defaults: new { controller = "Plan", action = "Pricing" });

            routes.MapRouteLowercase(
               name: "About",
               url: "about",
               defaults: new { controller = "Home", action = "About" });

            routes.MapRouteLowercase(
              name: "Contact",
              url: "contact",
              defaults: new { controller = "ContactUs", action = "Index" });

            routes.MapRouteLowercase(
              name: "ThankYou",
              url: "thank-you",
              defaults: new { controller = "ContactUs", action = "ThankYou" });

            routes.MapRouteLowercase(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}