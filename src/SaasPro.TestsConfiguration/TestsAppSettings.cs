using SaaSPro.Infrastructure.Configuration;

namespace SaasPro.TestsConfiguration
{
	public class TestsAppSettings : IApplicationSettings
	{
		public string MailServer => "smtp.sendgrid.net";
		public string MailUserName => "buenadigital";
		public string MailPassword => "!Password##9";
		public bool MailSSL => true;
		public bool SendEmail => true;
		public string SendEmailTo => "tmueller@buenadigital.com";
		public bool EnableOptimizations => false;
	}
}
