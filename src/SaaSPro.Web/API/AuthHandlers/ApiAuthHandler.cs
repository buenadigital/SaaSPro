using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;

namespace SaaSPro.Web.API.AuthHandlers
{
    public class ApiAuthHandler : DelegatingHandler
    {
        private readonly IApiSessionTokenService _apiSessionTokenService;
        private readonly IApiTokenService _apiTokenService;

        public ApiAuthHandler(IApiSessionTokenService apiSessionTokenService, IApiTokenService apiTokenService)
        {
            _apiSessionTokenService = apiSessionTokenService;
            _apiTokenService = apiTokenService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {           
            var authHeader = request.Headers.Authorization;
            var customer = HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] as Customer;

            if (authHeader != null)
            {
                IPrincipal principal = null;

                switch (authHeader.Scheme)
                {
                    case Constants.ApiKeySchemeName:
                        principal = ValidateApiKey(authHeader.Parameter, customer);
                        break;
                    case Constants.ApiSessionKeySchemeName:
                        principal = ValidateSessionApiKey(authHeader.Parameter, customer);
                        break;
                }

                if (principal != null)
                {
                    request.GetRequestContext().Principal = principal;
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private IPrincipal ValidateApiKey(string authToken, Customer customer)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return null;
            }

            var apiToken = _apiTokenService.Details(authToken, customer.Id);

            if (apiToken != null)
            {
                return new GenericPrincipal(new GenericIdentity(apiToken.Id.ToString(), Constants.ApiKeySchemeName), null);
            }

            return null;            
        }

        private IPrincipal ValidateSessionApiKey(string authToken, Customer customer)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return null;
            }

            var apiToken = _apiSessionTokenService.Details(authToken, customer.Id);

            if (apiToken != null)
            {
                return new GenericPrincipal(new GenericIdentity(apiToken.Id.ToString(), Constants.ApiSessionKeySchemeName), null);
            }

            return null;
        }
    }
}
