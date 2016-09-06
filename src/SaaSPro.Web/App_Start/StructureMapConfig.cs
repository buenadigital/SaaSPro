using Microsoft.AspNet.SignalR.Hubs;
using SaaSPro.Common.StructureMap;
using SaaSPro.Web.Hubs;
using StructureMap;
using StructureMap.Graph;

namespace SaaSPro.Web
{
    public static class StructureMapConfig
    {
        public static void Register()
        {
            ObjectFactory.Initialize(cfg =>
            {
                cfg.Scan(scan =>
                {
                    scan.LookForRegistries();
                    scan.TheCallingAssembly();
                });
            });
            
            // Set up Microsoft's "1 asp.net" service locators
            System.Web.Mvc.DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new StructureMapApiDependencyResolver(ObjectFactory.Container);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(ObjectFactory.Container));
        }
    }
}