namespace SaaSPro.Infrastructure.Email
{
    public class EmailServiceFactory
    {
        private static IMailService _emailService;

        public static void InitializeEmailServiceFactory(IMailService emailService)
        {
            _emailService = emailService;
        }

        public static IMailService GetEmailService()
        {
            return _emailService;
        }
    }
}
