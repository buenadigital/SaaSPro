using System.Configuration;

namespace SaaSPro.Web.Management.Application
{
    public static class Settings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings[Constants.MasterConnectionStringName].ConnectionString;
    }
}