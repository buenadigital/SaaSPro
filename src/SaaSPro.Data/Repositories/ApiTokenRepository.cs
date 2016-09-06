using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class ApiTokenRepository : EFRespository<ApiToken>, IApiTokenRepository
    {
        public ApiTokenRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
