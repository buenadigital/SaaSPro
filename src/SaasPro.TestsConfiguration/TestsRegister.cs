using Postal;
using SaasPro.Data.Repositories;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Common;
using SaaSPro.Data.Repositories;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Implementations;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace SaasPro.TestsConfiguration
{
	class TestsRegister : Registry
	{
		public TestsRegister()
		{
			// Services
			For<IPlanService>().Use<PlanService>();
			For<IRoleService>().Use<RoleService>();
			For<IUserService>().Use<UserService>();
			For<ICustomerService>().Use<CustomerService>();
			For<ILog4NetService>().Use<Log4NetService>();
			For<IDashboardService>().Use<DashboardService>();
			For<IEmailTemplatesService>().Use<EmailTemplatesService>();
			For<ICustomerDashboardService>().Use<ProjectService>();
			For<IProjectRepository>().Use<ProjectRepository>();
			For<IEmailService>().Use<CustomEmailService>().SelectConstructor(() => new CustomEmailService());
			For<IMailService>().Use<SendGridSMTPService>();
		    For<IApiSessionTokenService>().Use<ApiSessionTokenService>();

			// Infrastructure
			For<ICustomerStartupTask>().Add<CustomerDatabaseStartupTask>();
			For<ICustomerHost>().Use<WebAppHost>();

			For(typeof (IRepository<>)).Use(typeof (EFRespository<>)).LifecycleIs<SingletonLifecycle>();

			For<IUserRepository>().Use<UserRepository>();
			For<IRoleRepository>().Use<RoleRepository>();
			For<IReferenceListRepository>().Use<ReferenceListRepository>();
            For<IReferenceListItemRepository>().Use<ReferenceListItemRepository>();
            For<IApiTokenRepository>().Use<ApiTokenRepository>();
            For<IApiSessionTokenRepository>().Use<ApiSessionTokenRepository>();
			For<IIPSRepository>().Use<IPSRepository>();
			For<IPlanRepository>().Use<PlanRepository>();
			For<ICustomerRepository>().Use<CustomerRepository>();
			For<IEmailTemplatesRepository>().Use<EmailTemplatesRepository>();
			For<IPlanInfoRepository>().Use<PlanInfoRepository>();
			For<IPlanInfoValueRepository>().Use<PlanInfoValueRepository>();
			For<IAuditEntryRepository>().Use<AuditEntryRepository>();
			For<ILog4NetRepository>().Use<Log4NetRepository>();
			For<ICustomerPaymentRepository>().Use<CustomerPaymentRepository>();
			For<ICustomerPaymentRefundRepository>().Use<CustomerPaymentRefundRepository>();
			For<INoteRepository>().Use<NoteRepository>();
			For<ILoginManager>().Use<FormsLoginManager>();

			// UoW
			For<IUnitOfWork>().Use<EFUnitOfWork>().AlwaysUnique();

			// Logger
			For<ILogger>().Use<LogAdapter>();

			// Application Settings
			For<IApplicationSettings>().Use<TestsAppSettings>();

			//Stripe
			For<IStripeService>().Use<StripeAdapter>();
		}
	}
}
