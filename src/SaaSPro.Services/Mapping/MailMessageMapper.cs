using System.Net.Mail;
using SaaSPro.Domain;

namespace SaaSPro.Services.Mapping
{
    public static class MailMessageMapper
    {
        public static MailMessage ConvertToMailMessage(EmailTemplate emailTemplate)
        {
            var message = new MailMessage();
            message.From = new MailAddress(emailTemplate.FromEmail);
            message.To.Add(new MailAddress(emailTemplate.To));
            message.Subject = emailTemplate.Subject;
            message.Body = emailTemplate.Body;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

            return message;
        }
    }
}
