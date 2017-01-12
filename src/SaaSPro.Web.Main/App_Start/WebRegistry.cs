using System;
using Postal;
using SaaSPro.Common;
using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Services.Implementations;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.Main.Application;
using StructureMap.Configuration.DSL;
using StructureMap.Web.Pipeline;


namespace SaaSPro.Web.Main
{
	public class WebRegistry : Registry
	{
		public WebRegistry()
		{
			Policies.SetAllProperties(x =>
			{
				x.OfType<Func<IUnitOfWork>>();
			});

			// Services
			For<IPlanService>().Use<PlanService>();

			For<EFDbContext>().Use(() => new EFDbContext(Settings.ConnectionString)).LifecycleIs<HttpContextLifecycle>();
			For(typeof (IRepository<>)).Use(typeof (EFRespository<>)).LifecycleIs<HttpContextLifecycle>();

			For<IUserRepository>().Use<UserRepository>();
			For<IRoleRepository>().Use<RoleRepository>();
			For<IReferenceListRepository>().Use<ReferenceListRepository>();
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

			// UoW
			For<IUnitOfWork>().Use<EFUnitOfWork>().LifecycleIs<HttpContextLifecycle>();

			// Logger
			For<ILogger>().Use<LogAdapter>();

			// Infrastructure
			For<IEmailService>().Use<EmailService>().SelectConstructor(() => new EmailService());

			For<IMailService>().Use<SendGridSMTPService>();

			// Application Settings
			For<IApplicationSettings>().Use<WebConfigApplicationSettings>();

			//Stripe
			For<IStripeService>().Use<StripeAdapter>();
		}
	}
}