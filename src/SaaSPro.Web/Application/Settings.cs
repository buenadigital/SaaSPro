using System.Configuration;

namespace SaaSPro.Web.Application
{
    public static class Settings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings[Constants.MasterConnectionStringName].ConnectionString;
    }
}