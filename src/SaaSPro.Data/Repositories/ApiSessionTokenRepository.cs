using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class ApiSessionTokenRepository : EFRespository<ApiSessionToken>, IApiSessionTokenRepository
    {
        public ApiSessionTokenRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
