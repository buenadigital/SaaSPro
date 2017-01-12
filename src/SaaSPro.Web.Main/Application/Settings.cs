using System.Configuration;

namespace SaaSPro.Web.Main.Application
{
    public static class Settings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings[Constants.MasterConnectionStringName].ConnectionString;
    }
}