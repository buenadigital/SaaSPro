using System.Net.Mail;
using System.Threading.Tasks;
using Postal;

namespace SaasPro.TestsConfiguration.Helper
{
	public class CustomEmailService : IEmailService
	{
		public void Send(Email email)
		{
		}

		public Task SendAsync(Email email)
		{
			return null;
		}

		public MailMessage CreateMailMessage(Email email)
		{
			return new MailMessage();
		}
	}
}
