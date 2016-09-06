using System.Linq;
using AutoMapper;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.API.Model.Auth;

namespace SaasPro.TestsConfiguration
{
	public class TestsAutoMapperConfig
	{
		public static void Register()
		{
			Mapper.CreateMap<NotificationMessage, NotificationsListModel.Notification>();
			Mapper.CreateMap<Role, RolesListModel.RoleSummary>();
			Mapper.CreateMap<Role, RolesUpdateModel>();
			Mapper.CreateMap<User, UsersListModel.UserSummary>()
				.ForMember(model => model.Roles, opt => opt.MapFrom(u => u.Roles.Select(r => r.Name).OrderBy(x => x)));
			Mapper.CreateMap<User, UsersUpdateModel>()
				.ForMember(model => model.Roles, opt => opt.Ignore())
				.ForMember(model => model.SelectedRoles, opt => opt.MapFrom(u => u.Roles.Select(r => r.Id).ToList()));
			Mapper.CreateMap<User, UsersUpdateSecurityQuestionsModel>();
			Mapper.CreateMap<SaaSPro.Domain.SecurityQuestion, SaaSPro.Services.ViewModels.SecurityQuestion>()
				.ForMember(model => model.Answer, opt => opt.Ignore());
			Mapper.CreateMap<ReferenceList, ReferenceListsModel.ReferenceList>();
			Mapper.CreateMap<ReferenceList, ReferenceListsDetailsModel>();
			Mapper.CreateMap<ReferenceListItem, ReferenceListsDetailsModel.ReferenceListItem>();
			Mapper.CreateMap<ApiToken, ApiTokensListModel.ApiTokenSummary>();
			Mapper.CreateMap<ApiToken, ApiTokensUpdateModel>();
			Mapper.CreateMap<IPSEntry, IPSListModel.IPSEntrySummary>();
			Mapper.CreateMap<Project, CustomerDashboardModel.ProjectSummary>();
			Mapper.CreateMap<Project, CustomerDashboardModel>();
			Mapper.CreateMap<Note, NotesViewModel.Note>().ReverseMap();
			Mapper.CreateMap<Customer, CustomersListModel.CustomerSummary>()
				.ForMember(model => model.AdminEmail, opt => opt.MapFrom(j => j.AdminUser.Email));
			Mapper.CreateMap<Customer, CustomersDetailsModel>();
			Mapper.CreateMap<EmailTemplate, EmailTemplateListModel.EmailTemplateSummary>();
			Mapper.CreateMap<Plan, PlansListModel.PlanSummary>();
			Mapper.CreateMap<Plan, PlansUpdateModel>();

            // API
		    Mapper.CreateMap<ApiSessionToken, ApiSessionTokenModel>();
		}
	}
}
