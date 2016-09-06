using System.Net;
using System.Web.Mvc;
using SaaSPro.Common.Web;

namespace SaaSPro.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View("Error");
        }

        [HttpGet]
        public ViewResult NotFound()
        {
            return new HttpStatusCodeViewResult(HttpStatusCode.NotFound, "404 Page Not Found.");
        }
    }
}
