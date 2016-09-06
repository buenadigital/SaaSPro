using AutoMapper;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;
using SaaSPro.Web.Areas.Admin.ViewModels;
using SaaSPro.Web.Common.Scheduling;
using System.Linq;
using SecurityQuestion = SaaSPro.Services.ViewModels.SecurityQuestion;

namespace SaaSPro.Web
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<NotificationMessage, NotificationsListModel.Notification>();
            
            // Admin
            
            Mapper.CreateMap<Role, RolesListModel.RoleSummary>();
            Mapper.CreateMap<Role, RolesUpdateModel>();

            Mapper.CreateMap<User, UsersListModel.UserSummary>()
                .ForMember(model => model.Roles, opt => opt.MapFrom(u => u.Roles.Select(r => r.Name).OrderBy(x => x)));

            Mapper.CreateMap<User, UsersUpdateModel>()
                .ForMember(model => model.Roles, opt => opt.Ignore())
                .ForMember(model => model.SelectedRoles, opt => opt.MapFrom(u => u.Roles.Select(r => r.Id).ToList()));
            Mapper.CreateMap<User, UsersUpdateSecurityQuestionsModel>();

            Mapper.CreateMap<Domain.SecurityQuestion, SecurityQuestion>()
                .ForMember(model => model.Answer, opt => opt.Ignore());

            Mapper.CreateMap<SchedulerJob, SchedulerUpdateModel>()
                .ForMember(model => model.RepeatInterval, opt => opt.MapFrom(j => j.RepeatInterval.TotalMinutes));

            Mapper.CreateMap<ReferenceList, ReferenceListsModel.ReferenceList>();
            Mapper.CreateMap<ReferenceList, ReferenceListsDetailsModel>();
            Mapper.CreateMap<ReferenceListItem, ReferenceListsDetailsModel.ReferenceListItem>();

            Mapper.CreateMap<ApiToken, ApiTokensListModel.ApiTokenSummary>();
            Mapper.CreateMap<ApiToken, ApiTokensUpdateModel>();

            Mapper.CreateMap<IPSEntry, IPSListModel.IPSEntrySummary>();

            Mapper.CreateMap<Project, CustomerDashboardModel.ProjectSummary>();
            Mapper.CreateMap<Project, CustomerDashboardModel>();
         
            // API

            Mapper.CreateMap<AuditEntry, API.Model.AuditEntry>();
            Mapper.CreateMap<ApiSessionToken, API.Model.Auth.ApiSessionTokenModel>()
                .ForMember(model => model.SecurityQuestion, opt => opt.MapFrom(t => t.SecurityQuestion.Question));
        }
    }
}