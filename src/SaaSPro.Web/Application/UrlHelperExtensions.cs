using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Web
{
    public static class UrlHelperExtensions
    {
        public static string CurrentUrl(this UrlHelper url, object queryOverrides = null)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;

            var query = HttpUtility.ParseQueryString(requestUrl.Query);

            if (queryOverrides != null)
            {
                var queryDictionary = new RouteValueDictionary(queryOverrides);
                foreach (var param in queryDictionary)
                {
                    query.Set(param.Key, queryDictionary[param.Key].ToString());
                }
            }

            return "{0}?{1}".FormatWith(requestUrl.AbsolutePath, query.ToString());
        }        
    }
}