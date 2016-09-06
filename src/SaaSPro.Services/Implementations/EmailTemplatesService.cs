using System;
using System.Linq;
using SaaSPro.Common;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Common;
using AutoMapper;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Services.Implementations
{
    public class EmailTemplatesService : IEmailTemplatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;

        public EmailTemplatesService(IUnitOfWork unitOfWork, IEmailTemplatesRepository emailTemplatesRepository)
        {
            _unitOfWork = unitOfWork;
            _emailTemplatesRepository = emailTemplatesRepository;
        }

        public EmailTemplateListModel List(PagingCommand command)
        {
            IPagedList<EmailTemplate> emailTemplates = _emailTemplatesRepository.FetchPaged(q => q.OrderBy(t => t.TemplateName),
                                                                      command.PageIndex, command.PageSize);

            EmailTemplateListModel model = new EmailTemplateListModel
                            {
                                EmailTemplates =
                                    Mapper.Engine.MapPaged<EmailTemplate, EmailTemplateListModel.EmailTemplateSummary>(
                                        emailTemplates)
                            };
            return model;
        }


        public EmailTemplateDetailsModel Details(Guid id)
        {
            EmailTemplate emailTemplate = _emailTemplatesRepository.Get(id);
            if (emailTemplate == null) return null;
            EmailTemplateDetailsModel model = new EmailTemplateDetailsModel
            {
                Id = emailTemplate.Id,
                TemplateName = emailTemplate.TemplateName,
                FromEmail = emailTemplate.FromEmail,
                Subject = emailTemplate.Subject,
                Body = emailTemplate.Body
            };

            return model;
        }

        public Guid Update(EmailTemplateDetailsModel model)
        {
            EmailTemplate emailTemplate = _emailTemplatesRepository.Get(model.Id);
            if (emailTemplate == null)
            {
                return Guid.Empty;
            }
            emailTemplate.Update(model.TemplateName, model.Body, model.FromEmail, model.Subject);
            _emailTemplatesRepository.Update(emailTemplate);
            _unitOfWork.Commit();
            return emailTemplate.Id;
        }

        public void Add(EmailTemplateDetailsModel emailTemplate)
        {

            EmailTemplate model = new EmailTemplate(emailTemplate.TemplateName, emailTemplate.Body, emailTemplate.FromEmail,
                                          emailTemplate.Subject);
            _emailTemplatesRepository.Add(model);
            _unitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            EmailTemplate emailTemplate = _emailTemplatesRepository.Get(id);
            _emailTemplatesRepository.Delete(emailTemplate);
            _unitOfWork.Commit();
        }
    }
}
