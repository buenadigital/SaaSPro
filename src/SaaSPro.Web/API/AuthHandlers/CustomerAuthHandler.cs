using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Web.API.AuthHandlers
{
    public class CustomerAuthHandler : DelegatingHandler
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerAuthHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var hostname = request.RequestUri.Host.ToLower().Replace(ConfigurationManager.AppSettings["HostnameBase"], string.Empty);
            var customer = _customerRepository.Query().FirstOrDefault(x => x.Hostname.Equals(hostname, StringComparison.InvariantCultureIgnoreCase));
            
            if (customer == null)
            {
                throw new Exception("Customer is not found");
            }

            HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] = customer;

            return base.SendAsync(request, cancellationToken);
        }
    }
}