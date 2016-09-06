using SaaSPro.Domain;

namespace SaaSPro.Services.Helpers
{
    public  class EmailTemplateFactory
    {
        public static EmailTemplate ParseTemplate(EmailTemplate emailTemplate, object entity)
        {
            if(entity.GetType()==typeof(Customer))
            {
                // replace all possible options that a template can have
                var customer = (Customer)entity;
                emailTemplate.To = customer.AdminUser.Email;
                emailTemplate.Body = emailTemplate.Body.Replace("##Customer.AdminUser.FistName##", customer.AdminUser.FirstName)
                    .Replace("##Customer.AdminUser.LastName##", customer.AdminUser.LastName)
                    .Replace("##Customer.AdminUser.Email##", customer.AdminUser.Email)
                    .Replace("##Customer.HostName##", customer.GetFullHostName())
                    .Replace("##Customer.Plan.Name##", customer.Plan.Name);
            }
            else if (entity.GetType() == typeof(ViewModels.SignUpModel))
            {
                // replace all possible options that a template can have
                var customer = (ViewModels.SignUpModel)entity;
                emailTemplate.To = customer.Email;
                emailTemplate.Body = emailTemplate.Body.Replace("##Customer.AdminUser.FistName##", customer.FirstName)
                    .Replace("##Customer.AdminUser.LastName##", customer.LastName)
                    .Replace("##Customer.AdminUser.Email##", customer.Email)
                    .Replace("##Customer.HostName##", $"https://{customer.Domain}.saaspro.net")
                    .Replace("##Customer.Plan.Name##", customer.PlanName);
            }
            else if (entity.GetType() == typeof(ViewModels.AuthForgottenPasswordModel))
            {
                // replace all possible options that a template can have
                var customer = (ViewModels.AuthForgottenPasswordModel)entity;
                emailTemplate.To = customer.Email;
                emailTemplate.Body = emailTemplate.Body.Replace("##ResetPasswordUrl##", customer.ResetPasswordUrl);
            }
            else if (entity.GetType() == typeof(ViewModels.ContactModel))
            {
                // replace all possible options that a template can have
                var contactUs = (ViewModels.ContactModel)entity;
                emailTemplate.To = contactUs.MailTo;
                emailTemplate.Body = emailTemplate.Body.Replace("##Email##", contactUs.Email)
                    .Replace("##Message##", contactUs.Message);
            }

            return emailTemplate;
        }
    }
}
