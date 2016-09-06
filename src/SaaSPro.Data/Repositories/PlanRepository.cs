using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class PlanRepository : EFRespository<Plan>,IPlanRepository
    {
        public PlanRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
