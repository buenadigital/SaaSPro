using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    public class CustomerPayment : AuditedEntity
    {
        public Guid CustomerId { get; set; }
        public string TransactionId { get;  set; }
        public decimal Amount { get; set; }
        public bool Refunded { get;  set; }
        
        public Customer Customer { get;  set; }

        public CustomerPayment(Guid customerId, string transactionId, bool refunded, decimal amount, DateTime created)
        {
            Update(customerId, transactionId, refunded, amount, created);
        }

        public CustomerPayment() { }

        public void Update(Guid customerId, string transactionId, bool refunded, decimal amount, DateTime created)
        {
            Ensure.Argument.NotNullOrEmpty(transactionId, "transactionId");

            CustomerId = customerId;
            TransactionId = transactionId;
            Refunded = refunded;
            Amount = amount;
            CreatedOn = created;
        }
    }
}