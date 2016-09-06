using System.Web.Mvc;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Controllers
{
    public class DashboardController : SaaSProControllerBase
    {
        private readonly ICustomerDashboardService _customerDashboardService;

        public DashboardController(ICustomerDashboardService customerDashboardService)
        {
            _customerDashboardService = customerDashboardService;
        }

        [HttpGet]
        public ActionResult Index(PagingCommand command)
        {
            CustomerDashboardModel model = _customerDashboardService.Dashboard(command, Customer.CustomerId);
            return View(model);
        }
    }
}
