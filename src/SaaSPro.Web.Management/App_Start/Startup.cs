using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SaaSPro.Web.Management.App_Start.Startup))]

namespace SaaSPro.Web.Management.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
