using SaaSPro.Web.Common;
using StructureMap;
using System.Web.Mvc;

namespace SaaSPro.Web.Management
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IContainer container)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}