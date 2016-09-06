using AutoMapper;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Common.Scheduling;
using SaaSPro.Web.Management.ViewModels;

namespace SaaSPro.Web.Management
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<Plan, PlansListModel.PlanSummary>();
            Mapper.CreateMap<Plan, PlansUpdateModel>();

            Mapper.CreateMap<Customer, CustomersListModel.CustomerSummary>()
                .ForMember(model => model.AdminEmail, opt => opt.MapFrom(j => j.AdminUser.Email));
            Mapper.CreateMap<Customer, CustomersDetailsModel>();

            Mapper.CreateMap<EmailTemplate, EmailTemplateListModel.EmailTemplateSummary>();

            Mapper.CreateMap<SchedulerJob, SchedulerUpdateModel>()
                .ForMember(model => model.RepeatInterval, opt => opt.MapFrom(j => j.RepeatInterval.TotalMinutes));
            Mapper.CreateMap<SchedulerJobSummary, SchedulerListModel.JobSummary>();

            Mapper.CreateMap<Note, NotesViewModel.Note>();
            Mapper.CreateMap<NotesViewModel.Note, Note>();
        }
    }
}