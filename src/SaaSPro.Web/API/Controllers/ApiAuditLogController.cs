using SaaSPro.Services.Interfaces;
using SaaSPro.Web.API.Attributes;

namespace SaaSPro.Web.API.Controllers
{
    [ApiAuthorize]
    public class ApiAuditLogController : ApiControllerBase
    {
        private readonly IAuditEntryService _auditEntryService;

        public ApiAuditLogController(IAuditEntryService auditEntryService)
        {
            _auditEntryService = auditEntryService;
        }
    }
}
