using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Postal;
using SaasPro.Data.Repositories;
using SaaSPro.Common;
using SaaSPro.Common.StructureMap;
using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Implementations;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Application;
using SaaSPro.Web.Common.Scheduling;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Web.Pipeline;

namespace SaaSPro.Web
{
	public class WebRegistry : Registry
	{
		public WebRegistry()
		{
			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.ConnectImplementationsToTypesClosing(typeof(IHandleEvent<>));
			});

			// For action filters
			Policies.SetAllProperties(x =>
			{
				x.OfType<Func<IUnitOfWork>>();
				x.OfType<Func<IRepository<User>>>();
            });

			// Mvc
			For<IFilterProvider>().Use<StructureMapFilterProvider>();

			// Customers
			For<ICustomerStartupTask>().Add<CustomerDatabaseStartupTask>();
			ForSingletonOf<ICustomerHost>().Use<WebAppHost>();
            For<ICustomerResolver>()
                .Use<DatabaseCustomerResolver>()
                .Ctor<string>()
                .Is(ConfigurationManager.ConnectionStrings[Constants.MasterConnectionStringName].ConnectionString);
			For<CustomerInstance>()
				.Use(() => HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] as CustomerInstance)
				.LifecycleIs<HttpContextLifecycle>();

			// Data
			For<EFDbContext>().Use(() => new EFDbContext(Settings.ConnectionString)).LifecycleIs<HttpContextLifecycle>();
			For(typeof (IRepository<>)).Use(typeof (EFRespository<>)).LifecycleIs<HttpContextLifecycle>();

			// UoW
			For<IUnitOfWork>().Use<EFUnitOfWork>().LifecycleIs<HttpContextLifecycle>();

			For<IUserRepository>().Use<UserRepository>();
			For<IRoleRepository>().Use<RoleRepository>();
			For<IReferenceListRepository>().Use<ReferenceListRepository>();
            For<IReferenceListItemRepository>().Use<ReferenceListItemRepository>();
            For<IApiTokenRepository>().Use<ApiTokenRepository>();
            For<IApiSessionTokenRepository>().Use<ApiSessionTokenRepository>();
			For<IPlanRepository>().Use<PlanRepository>();
			For<ICustomerRepository>().Use<CustomerRepository>();
			For<IEmailTemplatesRepository>().Use<EmailTemplatesRepository>();
			For<IPlanInfoRepository>().Use<PlanInfoRepository>();
			For<IPlanInfoValueRepository>().Use<PlanInfoValueRepository>();
			For<IAuditEntryRepository>().Use<AuditEntryRepository>();
			For<ILog4NetRepository>().Use<Log4NetRepository>();
			For<ICustomerPaymentRepository>().Use<CustomerPaymentRepository>();
			For<ICustomerPaymentRefundRepository>().Use<CustomerPaymentRefundRepository>();
			For<IProjectRepository>().Use<ProjectRepository>();
			For<INoteRepository>().Use<NoteRepository>();

            // Security
            For<ILoginManager>().Use<FormsLoginManager>();

			// Infrastructure
			

			// Eventing
			For<IEventBus>().Use<EventBus>();
			For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
			Events.RegisterEventBus(() => ObjectFactory.GetInstance<IEventBus>());
			DomainEvents.RegisterEventBus(() => ObjectFactory.GetInstance<IEventBus>());

			// Domain
			For<INotificationTransport>().Add<EFNotificationTransport>();
			For<INotificationTransport>().Add<SignalRNotificationTransport>();
			For<INotificationCenter>().Use<NotificationCenter>();

			//Services
			For<IUserService>().Use<UserService>();
			For<IRoleService>().Use<RoleService>();
			For<IReferenceListService>().Use<ReferenceListService>();
			For<IApiTokenService>().Use<ApiTokenService>();
            For<IApiSessionTokenService>().Use<ApiSessionTokenService>();
			For<IPlanService>().Use<PlanService>();
			For<ISubscriptionsService>().Use<SubscriptionsService>();
			For<IUserNotificationService>().Use<UserNotificationService>();
			For<ICustomerService>().Use<CustomerService>();
			For<IEmailTemplatesService>().Use<EmailTemplatesService>();
			For<IAuditEntryService>().Use<AuditEntryService>();
			For<ICustomerDashboardService>().Use<ProjectService>();

            // Infrastructure
            For<IEmailService>().Use<EmailService>().SelectConstructor(() => new EmailService());
			For<IMailService>().Use<SendGridSMTPService>();
            For<ISchedulerClient>().Use<QuartzSchedulerClient>().Ctor<string>().Is(Constants.QuartzSchedulerAddressSettingKey);

            // Logger
            For<ILogger>().Use<LogAdapter>();

			// Application Settings
			For<IApplicationSettings>().Use<WebConfigApplicationSettings>();

			//Stripe
			For<IStripeService>().Use<StripeAdapter>();
		}
	}
}