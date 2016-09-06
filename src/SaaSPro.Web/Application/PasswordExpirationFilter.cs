using System;
using System.Security.Claims;
using System.Web.Mvc;
using SaaSPro.Common.Web;

namespace SaaSPro.Web
{
    public class PasswordExpirationFilter : IActionFilter
    {
        private const string ChangePasswordUrl = "/account/changepassword";
        
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.RequestContext.HttpContext;

            if (httpContext.Request.IsAuthenticated && IsNotPasswordChangePage(httpContext.Request.Url))
            {
                var user = new UserPrincipal(httpContext.User as ClaimsPrincipal);
                if (user.HasPasswordExpired)
                {
                    filterContext.Result = new RedirectResult(ChangePasswordUrl)
                        .AndAlert(AlertType.Warning, "Password expired.", "Your password has expired. Please enter a new one.");
                }
            }
        }
        
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        private bool IsNotPasswordChangePage(Uri uri)
        {
            return !uri.AbsolutePath.Equals(ChangePasswordUrl);
        }
    }
}