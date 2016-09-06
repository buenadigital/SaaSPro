using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class PlanInfoValueMap : EntityTypeConfiguration<PlanInfoValue>
	{
		public PlanInfoValueMap()
		{
			ToTable("PlanInfoValues");
			HasKey(a => new { a.PlanId, a.PlanInfoItemId });

			Property(x => x.Value);

			Ignore(x => x.Id);
		}
	}
}