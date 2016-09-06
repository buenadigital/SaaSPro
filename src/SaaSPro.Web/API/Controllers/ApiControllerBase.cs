using System.Web;
using System.Web.Http;
using SaaSPro.Domain;
using SaaSPro.Web.API.Attributes;

namespace SaaSPro.Web.API.Controllers
{
    [UnhandledExceptionFilter]
    [ApiResponse]
    public class ApiControllerBase : ApiController
    {
        protected Customer Customer => HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] as Customer;
    }
}