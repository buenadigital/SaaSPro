using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
    public class CustomerMap : AuditedClassMap<Customer>
    {
        public CustomerMap()
        {
			ToTable("Customers");
			HasKey(x => x.Id);

			Property(x => x.FullName);
			Property(x => x.Company);
			Property(x => x.Hostname);
			Property(x => x.Enabled);
			Property(x => x.EncryptionKey);
			Property(x => x.PaymentCustomerId);
			Property(x => x.PlanCreatedOn);
			Property(x => x.PlanCanceledOn);
			Property(x => x.PlanUpdatedOn);

			HasMany(r => r.Roles)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.Users)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.ReferenceListItems)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.ApiTokens)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.AuditEntries)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.IPSEntries)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.Notes)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);

			HasMany(r => r.Projects)
				.WithRequired()
				.HasForeignKey(t => t.CustomerId)
				.WillCascadeOnDelete(true);


			HasOptional(p => p.Plan).WithMany(t=>t.Customers).Map(t => t.MapKey("PlanId"));

			HasOptional(t => t.AdminUser).WithMany(t=>t.Customers).Map(t=>t.MapKey("AdminUserId")).WillCascadeOnDelete(false);
		}
    }
}