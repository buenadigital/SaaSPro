using System.Web.Mvc;
using SaaSPro.Infrastructure.Logging;

namespace SaaSPro.Web.Management.Controllers
{
    public class ManagementControllerBase : Controller
    {       
        protected override void OnException(ExceptionContext ex)
        {
            LoggingFactory.GetLogger().Log(ex.Exception.Message, EventLogSeverity.Error);
        }
    
    }
}