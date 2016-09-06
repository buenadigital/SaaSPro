using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class CustomerRepository : EFRespository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EFDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
