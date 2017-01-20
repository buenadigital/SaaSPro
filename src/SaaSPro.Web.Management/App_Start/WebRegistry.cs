using Postal;
using SaaSPro.Common;
using SaaSPro.Data.Repositories;
using SaaSPro.Web.Common.Scheduling;
using SaaSPro.Web.Management.Application;
using StructureMap.Configuration.DSL;
using System;
using SaaSPro.Data;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Implementations;
using SaaSPro.Services.Interfaces;
using StructureMap.Web.Pipeline;

namespace SaaSPro.Web.Management
{
	public class WebRegistry : Registry
	{
		public WebRegistry()
		{
			Policies.SetAllProperties(x =>
			{
				x.OfType<Func<IUnitOfWork>>();
			});

			//EF
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
			For<INoteRepository>().Use<NoteRepository>();

			// Services
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
			For<ILog4NetService>().Use<Log4NetService>();
			For<IDashboardService>().Use<DashboardService>();
			For<IStripeWebhookService>().Use<StripeWebhookService>();

			// Infrastructure
			For<IEmailService>().Use<EmailService>().SelectConstructor(() => new EmailService());

			For<IMailService>().Use<SMTPService>();

			// Logger
			For<ILogger>().Use<LogAdapter>();

			// Application Settings
			For<IApplicationSettings>().Use<WebConfigApplicationSettings>();

			//Stripe
			For<IStripeService>().Use<StripeAdapter>();

			For<ISchedulerClient>().Use<QuartzSchedulerClient>().Ctor<string>().Is(Constants.QuartzSchedulerAddressSettingKey);
		}
	}
}