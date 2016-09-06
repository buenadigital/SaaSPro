using SaaSPro.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(RegisterHttpModules), "Start")]

namespace SaaSPro.Web
{
    public class RegisterHttpModules
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(CustomerHttpModule));
        }
    }
}