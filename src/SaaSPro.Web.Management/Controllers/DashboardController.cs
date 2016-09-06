using System.Web.Mvc;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Management.Controllers
{
    public class DashboardController : ManagementControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public ActionResult Index()
        {
            DashboardModel model = _dashboardService.Dashboard();
            return View(model);
        }
    }
}
