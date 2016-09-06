using System.Configuration;
using SaaSPro.Domain;
using System;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Web
{
    public class DatabaseCustomerResolver : CustomerResolver
	{
        private readonly string _connectionString;

        public DatabaseCustomerResolver(string connectionString)
		{
			if (connectionString == null)
				throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
		}

		public override Customer ResolveCustomer(string instanceKey)
		{
			if (string.IsNullOrEmpty(instanceKey))
			{
				throw new ArgumentNullException(nameof(instanceKey));
			}

			var hostname = instanceKey.Replace(ConfigurationManager.AppSettings["HostnameBase"], string.Empty);
            return CustomerResolver.GetByHostname(hostname, _connectionString);
		}
	}
}