using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class AuditEntryMap : EntityTypeConfiguration<AuditEntry>
	{
		public AuditEntryMap()
		{
			ToTable("AuditLog");
			HasKey(x => x.Id);

			Property(x => x.TimeStamp).IsRequired();
			Property(x => x.Username);
			Property(x => x.EntityType).IsRequired();
			Property(x => x.EntityId).IsRequired();
			Property(x => x.Action).IsRequired();
			Property(x => x.Description).IsMaxLength();
			Property(x => x.CustomerId);
		}
	}
}