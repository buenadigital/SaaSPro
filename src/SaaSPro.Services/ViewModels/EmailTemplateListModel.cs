using System;
using SaaSPro.Common;

namespace SaaSPro.Services.ViewModels
{
    public class EmailTemplateListModel
    {
        public IPagedList<EmailTemplateSummary> EmailTemplates { get; set; }

        public class EmailTemplateSummary
        {
            public Guid Id { get; set; }
            public string TemplateName { get; set; }
            public string Body { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime UpdatedOn { get; set; }
        }
    }
}