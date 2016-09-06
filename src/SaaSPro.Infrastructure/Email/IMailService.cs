using System.Net.Mail;

namespace SaaSPro.Infrastructure.Email
{
    public interface IMailService
    {
        bool SendMail(MailMessage mailMessage);
    }
}
