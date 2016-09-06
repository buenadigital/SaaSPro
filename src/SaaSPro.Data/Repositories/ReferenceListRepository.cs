using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class ReferenceListRepository : EFRespository<ReferenceList>, IReferenceListRepository
    {
        public ReferenceListRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
