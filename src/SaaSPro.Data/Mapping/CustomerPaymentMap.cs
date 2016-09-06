using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class CustomerPaymentMap : AuditedClassMap<CustomerPayment>
	{
		public CustomerPaymentMap()
		{
			ToTable("CustomerPayments");
			HasKey(x => x.Id);

			Property(x => x.TransactionId);
			Property(x => x.Amount);
			Property(x => x.Refunded);

			HasRequired(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId);
		}
	}
}