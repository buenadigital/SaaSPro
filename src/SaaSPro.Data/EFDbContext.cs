using System;
using System.Data.Common;
using System.Data.Entity;
using SaaSPro.Data.Mapping;
using SaaSPro.Domain;

namespace SaaSPro.Data
{
	public class EFDbContext : DbContext
	{
		public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<NotificationMessage> NotificationMessages { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }
		public DbSet<ReferenceListItem> ReferenceListItems { get; set; }
		public DbSet<ReferenceList> ReferenceLists { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<PlanInfo> PlanInfos { get; set; }
		public DbSet<PlanInfoValue> PlanInfoValues { get; set; }
		public DbSet<Plan> Plans { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Log4NetLog> Log4NetLogs { get; set; }
		public DbSet<IPSEntry> IPSEntries { get; set; }
		public DbSet<EmailTemplate> EmailTemplates { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<CustomerPayment> CustomerPayments { get; set; }
		public DbSet<CustomerPaymentRefund> CustomerPaymentRefunds { get; set; }
		public DbSet<AuditEntry> AuditEntries { get; set; }
		public DbSet<ApiToken> ApiTokens { get; set; }
        public DbSet<ApiSessionToken> ApiSessionTokens { get; set; }
        public DbSet<RoleUsers> RoleUsers { get; set; }

		public EFDbContext(string connectionString) : base(connectionString)
		{
			#if DEBUG
			Database.Log = Console.Write;
			#endif
		}

		public EFDbContext(DbConnection connection)
            : base(connection, true)
        {
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new SecurityQuestionMap());
			modelBuilder.Configurations.Add(new UserMap());
			modelBuilder.Configurations.Add(new RoleMap());
			modelBuilder.Configurations.Add(new NotificationMessageMap());
			modelBuilder.Configurations.Add(new UserNotificationMap());
			modelBuilder.Configurations.Add(new ReferenceListItemMap());
			modelBuilder.Configurations.Add(new ReferenceListMap());
			modelBuilder.Configurations.Add(new ProjectMap());
			modelBuilder.Configurations.Add(new PlanInfoMap());
			modelBuilder.Configurations.Add(new PlanInfoValueMap());
			modelBuilder.Configurations.Add(new PlanMap());
			modelBuilder.Configurations.Add(new NoteMap());
			modelBuilder.Configurations.Add(new Log4NetLogMap());
			modelBuilder.Configurations.Add(new IPSEntryMap());
			modelBuilder.Configurations.Add(new EmailTemplateMap());
			modelBuilder.Configurations.Add(new CustomerMap());
			modelBuilder.Configurations.Add(new CustomerPaymentMap());
			modelBuilder.Configurations.Add(new CustomerPaymentRefundMap());
			modelBuilder.Configurations.Add(new AuditEntryMap());
			modelBuilder.Configurations.Add(new ApiTokenMap());
            modelBuilder.Configurations.Add(new ApiSessionTokenMap());
            modelBuilder.Configurations.Add(new RoleUsersMap());

			modelBuilder.Ignore<Notification>();
		}
	}
}