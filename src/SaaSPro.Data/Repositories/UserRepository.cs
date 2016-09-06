using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class UserRepository : EFRespository<User>,IUserRepository
    {
        public UserRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
