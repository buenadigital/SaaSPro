using SaaSPro.Web.Common;
using StructureMap;
using System.Web.Mvc;

namespace SaaSPro.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters, IContainer container)
		{
			filters.Add(new HandleErrorAttribute());
            filters.Add(container.GetInstance<ClaimsTransformerFilter>());
            filters.Add(new PasswordExpirationFilter());
		}
	}
}