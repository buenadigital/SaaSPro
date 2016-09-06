using System;
using System.Net;
using System.Net.Mail;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Logging;

namespace SaaSPro.Infrastructure.Email
{
    public class SMTPService : IMailService
    {
        public bool SendMail(MailMessage mailMessage)
        {
            bool result = false;

            if (!ApplicationSettingsFactory.GetApplicationSettings().SendEmail)
            {
                //Email is disbaled. Don't send email just return true.
                return true;
            }

            try
            {
                string _server = ApplicationSettingsFactory.GetApplicationSettings().MailServer;
                string _user = ApplicationSettingsFactory.GetApplicationSettings().MailUserName;
                string _password = ApplicationSettingsFactory.GetApplicationSettings().MailPassword;

                string _port = "";
                bool _useSSL = ApplicationSettingsFactory.GetApplicationSettings().MailSSL;

                var smtp = new SmtpClient();
                smtp.Host = _server;
                smtp.Credentials = new NetworkCredential(_user, _password);

                if (!string.IsNullOrEmpty(_port))
                    smtp.Port = Int32.Parse(_port);

                if (_port.Trim() != string.Empty)
                {
                    int altPort = 25;
                    if (int.TryParse(_port, out altPort))
                    {
                        smtp.Port = altPort;
                    }
                }

                if (_useSSL)
                {
                    smtp.EnableSsl = _useSSL;
                }

                if (!string.IsNullOrWhiteSpace(ApplicationSettingsFactory.GetApplicationSettings().SendEmailTo))
                {
                    mailMessage.To.Clear();
                    mailMessage.To.Add(new MailAddress(ApplicationSettingsFactory.GetApplicationSettings().SendEmailTo));
                }

                smtp.Send(mailMessage);
                result = true;
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().LogError(ex);
                result = false;
            }

            return result;
        }
    }
}
