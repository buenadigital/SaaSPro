using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class PlanInfoMap : EntityTypeConfiguration<PlanInfo>
	{
		public PlanInfoMap()
		{
			ToTable("PlanInfo");
			HasKey(x => x.Id);

			Property(x => x.Name);
			Property(x => x.OrderIndex);

			HasMany(x => x.PlanInfoValues)
					.WithRequired(t => t.PlanInfo)
					.HasForeignKey(t => t.PlanInfoItemId);
		}
	}
}