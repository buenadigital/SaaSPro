using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class Log4NetRepository: EFRespository<Log4NetLog>, ILog4NetRepository
    {
        public Log4NetRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
