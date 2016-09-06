using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class PlanMap : AuditedClassMap<Plan>
	{
		public PlanMap()
		{
			ToTable("Plans");
			HasKey(x => x.Id);

			Property(x => x.Name);
			Property(x => x.Price);
			Property(x => x.Period);
			Property(x => x.OrderIndex);
			Property(x => x.PlanCode);
			Property(x => x.Enabled);

			HasMany(x => x.PlanInfoValues)
				.WithRequired(t => t.Plan)
				.HasForeignKey(t => t.PlanId).WillCascadeOnDelete(true);
		}
	}
}
