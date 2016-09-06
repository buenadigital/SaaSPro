using System.Configuration;

namespace SaaSPro.Infrastructure.Configuration
{
    public class WebConfigApplicationSettings : IApplicationSettings 
    {
        public string MailServer => ConfigurationManager.AppSettings["MailServer"];
        public string MailUserName => ConfigurationManager.AppSettings["MailUserName"];
        public string MailPassword => ConfigurationManager.AppSettings["MailPassword"];
        public bool MailSSL => bool.Parse(ConfigurationManager.AppSettings["MailSSL"]);
        public bool SendEmail => bool.Parse(ConfigurationManager.AppSettings["SendEmail"]);
        public string SendEmailTo => ConfigurationManager.AppSettings["SendEmailTo"];
        public bool EnableOptimizations => bool.Parse(ConfigurationManager.AppSettings["EnableOptimizations"]);
    }
}
