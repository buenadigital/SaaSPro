using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Logging;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SaaSPro.Infrastructure.Email
{
    public class SendGridSMTPService : IMailService
    {
        public bool SendMail(MailMessage mailMessage)
        {
            try
            {
                SendMailAsync(mailMessage).Wait();
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                return false;
            }

            return true;
        }

        private static async Task SendMailAsync(MailMessage message)
        {
            string apiKey = ApplicationSettingsFactory.GetApplicationSettings().APIKey;
            dynamic sg = new SendGridAPIClient(apiKey);

            SendGrid.Helpers.Mail.Email from = new SendGrid.Helpers.Mail.Email(message.From.ToString());
            SendGrid.Helpers.Mail.Email to = new SendGrid.Helpers.Mail.Email(message.To.ToString());

            Content content;

            if(message.IsBodyHtml)
                content = new Content("text/html", message.Body);
            else
                content = new Content("text/plain", message.Body);
            
            Mail mail = new Mail(from, message.Subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}
