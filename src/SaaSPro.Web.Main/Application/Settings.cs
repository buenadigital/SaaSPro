using System.Configuration;

namespace SaaSPro.Web.Front.Application
{
    public static class Settings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings[Constants.MasterConnectionStringName].ConnectionString;
    }
}