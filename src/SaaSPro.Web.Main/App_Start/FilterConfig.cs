using System.Web.Mvc;
using StructureMap;

namespace SaaSPro.Web.Main
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IContainer container)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}