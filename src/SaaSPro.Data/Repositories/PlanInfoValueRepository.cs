using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
	public class PlanInfoValueRepository : EFRespository<PlanInfoValue>, IPlanInfoValueRepository
	{
		public PlanInfoValueRepository(EFDbContext dbContext)
			: base(dbContext)
		{
		}
	}
}
