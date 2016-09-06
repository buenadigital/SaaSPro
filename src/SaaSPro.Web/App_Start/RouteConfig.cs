using System.Web.Mvc;
using System.Web.Routing;

namespace SaaSPro.Web
{
	public class RouteConfig
	{
        public static void RegisterRoutes(RouteCollection routes)
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("SaaSPro.Web.Controllers");
            
            routes.LowercaseUrls = true;
            routes.AppendTrailingSlash = false;
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AuditLog",
                url: "auditlog/{entityType}/{id}",
                defaults: new { controller = "AuditLog", action = "details" }
            );

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
	}
}