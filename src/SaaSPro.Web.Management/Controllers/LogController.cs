using System;
using System.Web.Mvc;
using SaaSPro.Common.Web;
using SaaSPro.Common;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Web.Management.Controllers
{
    public class LogController : ManagementControllerBase
    {
        private readonly ILog4NetService _log4NetService;

        public LogController(ILog4NetService log4NetService)
        {
            _log4NetService = log4NetService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            LogDashboardModel model = _log4NetService.Dashboard();

            return View(model);

        }
        public ActionResult List(PagingCommand command, string name)
        {
            IPagedList<Log4NetLog> list = _log4NetService.ListErrorLogs(command, name);
            ViewBag.Name = name;
            return View(list);
        }

        public ActionResult Details(Guid id, string name)
        {
            ViewBag.Name = name;
            Log4NetLog model = _log4NetService.Detail(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _log4NetService.Delete(id);

            return RedirectToAction("Index").AndAlert(AlertType.Success, "Log deleted.",
                                                      "The Log was deleted successfully.");
        }

        [HttpPost]
        public ActionResult DeleteAll(string name)
        {
            _log4NetService.DeleteAll(name);

            return RedirectToAction("Index").AndAlert(AlertType.Success, "Logs deleted for " + name,
                                                      "The logs were deleted successfully.");
        }
    }
}
