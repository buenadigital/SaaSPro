using SaaSPro.Common.StructureMap;
using StructureMap;
using StructureMap.Graph;

namespace SaaSPro.Web.Management
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

            System.Web.Mvc.DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
    }
}