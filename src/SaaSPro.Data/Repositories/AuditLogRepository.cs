using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class AuditEntryRepository :EFRespository<AuditEntry>, IAuditEntryRepository
    {
        public AuditEntryRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
