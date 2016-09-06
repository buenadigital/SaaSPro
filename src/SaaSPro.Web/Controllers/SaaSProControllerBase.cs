using System.Security.Claims;
using System.Web.Mvc;

namespace SaaSPro.Web.Controllers
{
    [Authorize]
    public class SaaSProControllerBase : Controller
    {
        public new UserPrincipal User => new UserPrincipal(base.User as ClaimsPrincipal);

        public CustomerInstance Customer => HttpContext.Items[Constants.CurrentCustomerInstanceKey] as CustomerInstance;

        protected ActionResult ViewOrPartial<TModel>(TModel model, string viewName = default(string), string partialViewName = default(string))
        {
            if (Request.IsAjaxRequest() || ControllerContext.IsChildAction)
            {
                return PartialView(partialViewName, model);
            }

            return View(viewName, model);
        }
    }
}