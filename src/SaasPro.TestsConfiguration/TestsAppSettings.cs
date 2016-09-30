using SaaSPro.Infrastructure.Configuration;

namespace SaasPro.TestsConfiguration
{
	public class TestsAppSettings : IApplicationSettings
	{
		public string MailServer => "smtp.sendgrid.net";
		public string MailUserName => "XXXX";
		public string MailPassword => "XXXX";
		public bool MailSSL => true;
		public bool SendEmail => true;
		public string SendEmailTo => "XXXX";
		public bool EnableOptimizations => false;
	}
}
