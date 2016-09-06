using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class RoleRepository : EFRespository<Role>,IRoleRepository
    {
        public RoleRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
