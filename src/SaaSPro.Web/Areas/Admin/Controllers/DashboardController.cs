using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class DashboardController : AdminControllerBase
    {   
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
