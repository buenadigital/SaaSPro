using System.Security.Claims;
using System.Web.Mvc;

namespace SaaSPro.Web
{
    public abstract class SaaSProViewPage<TModel> : WebViewPage<TModel>
    {
        public new UserPrincipal User => new UserPrincipal(base.User as ClaimsPrincipal);
    }

    public abstract class SaaSProViewPage : SaaSProViewPage<dynamic>
    {
    }
}