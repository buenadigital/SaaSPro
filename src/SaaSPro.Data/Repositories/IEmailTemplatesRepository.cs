using SaaSPro.Common;
using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public interface IEmailTemplatesRepository : IRepository<EmailTemplate>
    {
        EmailTemplate GetTemplateByName(string templateName);

    }
}
