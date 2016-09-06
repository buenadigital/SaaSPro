using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class CustomerPaymentRefundMap : EntityTypeConfiguration<CustomerPaymentRefund>
	{
		public CustomerPaymentRefundMap()
		{
			ToTable("CustomerPaymentRefunds");
			HasKey(x => x.Id);

			Property(x => x.TransactionId);
			Property(x => x.Amount);
			Property(x => x.ChargeId);
			Property(x => x.Created);

			HasRequired(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId);
		}
	}
}