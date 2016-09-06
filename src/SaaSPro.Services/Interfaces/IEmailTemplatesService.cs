using System;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Interfaces
{
    public interface IEmailTemplatesService
    {
        EmailTemplateListModel List(PagingCommand command);
        EmailTemplateDetailsModel Details(Guid id);
        Guid Update(EmailTemplateDetailsModel model);
        void Delete(Guid id);
        void Add(EmailTemplateDetailsModel emailTemplate);
    }
}
