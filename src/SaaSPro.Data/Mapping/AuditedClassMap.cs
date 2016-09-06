using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public abstract class AuditedClassMap<T> : EntityTypeConfiguration<T> where T : AuditedEntity
	{
		protected AuditedClassMap()
		{
			Property(x => x.CreatedOn).IsOptional();
			Property(x => x.UpdatedOn).IsOptional();

			HasOptional(t => t.UpdatedBy).WithMany().Map(t => t.MapKey("UpdatedBy"));
			HasOptional(t => t.CreatedBy).WithMany().Map(t => t.MapKey("CreatedBy"));
		}
	}
}