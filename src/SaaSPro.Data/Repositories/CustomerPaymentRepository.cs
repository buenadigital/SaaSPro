using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class CustomerPaymentRepository : EFRespository<CustomerPayment>, ICustomerPaymentRepository
    {
        public CustomerPaymentRepository(EFDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
