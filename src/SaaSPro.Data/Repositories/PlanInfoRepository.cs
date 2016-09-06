using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
	public class PlanInfoRepository : EFRespository<PlanInfo>, IPlanInfoRepository
	{
		public PlanInfoRepository(EFDbContext dbContext)
			: base(dbContext)
		{
		}
	}
}
