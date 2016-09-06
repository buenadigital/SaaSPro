using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class CustomerPaymentRefundRepository : EFRespository<CustomerPaymentRefund>,
        ICustomerPaymentRefundRepository
    {
        public CustomerPaymentRefundRepository(EFDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
