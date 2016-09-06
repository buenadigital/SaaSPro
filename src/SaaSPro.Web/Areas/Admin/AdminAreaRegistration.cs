using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;

            context.MapRoute(
                "AdminDefault",
                "admin/{controller}/{action}/{id}",
                new { controller = "dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
