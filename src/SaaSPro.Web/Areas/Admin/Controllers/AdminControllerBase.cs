using SaaSPro.Domain;
using SaaSPro.Web.Controllers;
using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = DefaultRoles.Admin)] 
    public class AdminControllerBase : SaaSProControllerBase
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            ViewBag.CurrentPage = "admin";
        }
    }
}
