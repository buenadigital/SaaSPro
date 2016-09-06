using System.Linq;
using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class EmailTemplatesRepository : EFRespository<EmailTemplate>, IEmailTemplatesRepository
    {
        public EmailTemplatesRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public EmailTemplate GetTemplateByName(string emailTemplateName)
        {
            EmailTemplate template = this.Query().SingleOrDefault(x => x.TemplateName.Equals(emailTemplateName));

            return template;
        }
    }
}
