using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SaaSPro.Common;
using SaaSPro.Common.Helpers;
using SaaSPro.Domain;

namespace SaaSPro.Web.API.AuthHandlers
{
    public class IPAddressAuthHandler : DelegatingHandler
    {
        private readonly Func<IRepository<IPSEntry>> _repoFactory;

        public IPAddressAuthHandler(Func<IRepository<IPSEntry>> repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, nameof(repoFactory));
            _repoFactory = repoFactory;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var customer = HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] as Customer;

            if (!ValidateIPAddress(request, customer))
            {
                var forbiddenResponse = request.CreateResponse(HttpStatusCode.Forbidden);
                forbiddenResponse.ReasonPhrase = "IP address rejected.";
                
                return Task.FromResult(forbiddenResponse);
            }

            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateIPAddress(HttpRequestMessage request, Customer customer)
        {
            if (request.IsLocal())
            {
                return true;
            }

            var ipAddress = GetClientIPAddress(request);

            if (ipAddress == null)
            {
                return false;
            }
            
            // Check IPS entries

            var repo = _repoFactory();
            var validRanges = repo.Query().Where(t => t.CustomerId == customer.Id).ToList()
                .Select(entry => entry.GetIPAddressRange());

            return validRanges.Any(range => range.Contains(ipAddress));
        }

        private IPAddress GetClientIPAddress(HttpRequestMessage request)
        {
            // This will only work when hosted in IIS
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = request.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (ctx != null)
                {
                    return IPAddress.Parse(ctx.Request.UserHostAddress);
                }
            }

            return null;
        }
    }
}