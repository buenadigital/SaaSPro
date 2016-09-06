using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Web.Application;
using System.Linq;
using System;

namespace SaaSPro.Web
{
    public abstract class CustomerResolver : ICustomerResolver
    {
        public virtual string CreateInstanceKey(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return requestUri.Host.ToLowerInvariant();
        }

        public abstract Customer ResolveCustomer(string instanceKey);

        public static Customer GetByHostname(string hostname, string connectionString)
        {
            using (var dbContext = new EFDbContext(connectionString))
            {
                return dbContext.Customers.FirstOrDefault(x => x.Hostname.Equals(hostname, StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}