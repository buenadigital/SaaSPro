using System;
using System.Net;
using System.Net.Mail;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Logging;
using SendGrid;

namespace SaaSPro.Infrastructure.Email
{
    public class SendGridSMTPService : IMailService
    {
        public bool SendMail(MailMessage mailMessage)
        {
            try
            {
                SendMailAsync(CreateSendGridMessage(mailMessage));
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                return false;
            }

            return true;
        }

        private void SendMailAsync(SendGridMessage message)
	    {
            string server = ApplicationSettingsFactory.GetApplicationSettings().MailServer;
            string user = ApplicationSettingsFactory.GetApplicationSettings().MailUserName;
            string password = ApplicationSettingsFactory.GetApplicationSettings().MailPassword;

            // Create credentials, specifying your user name and password.
            var credentials = new NetworkCredential(user, password, server);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.DeliverAsync(message).GetAwaiter();
	    }

        private SendGridMessage CreateSendGridMessage(MailMessage mail)
        {
            var sendGridMessage = new SendGridMessage()
            {
                From = mail.From,
                Subject = mail.Subject
            };

            foreach (var recipient in mail.To)
            {
                sendGridMessage.AddTo(recipient.ToString());
            }

            if (mail.IsBodyHtml)
                sendGridMessage.Html = mail.Body;
            else
                sendGridMessage.Text = mail.Body;
            
            return sendGridMessage;
        }
    }
}
