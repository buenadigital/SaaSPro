using SaaSPro.Common.Web;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using System;
using System.Data;
using System.Web.Mvc;

namespace SaaSPro.Web.Management.Controllers
{
    public class PlansController : ManagementControllerBase
    {

        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        //
        // GET: /Plans/

        [HttpGet]
        public ActionResult Index(PagingCommand command)
        {
            PlansListModel model = _planService.List(command);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PlanAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _planService.Add(model);

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Success.", "Plan added successfully.");
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            //Plan plan = _planService.Get(id);

            //if (plan == null)
            //{
            //    return HttpNotFound();
            //}

            //PlansUpdateModel model = AutoMapper.Mapper.Engine.Map<PlansUpdateModel>(plan);

            //return View(model);

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Success.", "Plan updated successfully.");
        }

        [HttpPost]
        public ActionResult Update(Guid id, PlansUpdateModel model)
        {
            //Plan plan = _planService.Get(id);

            //if (plan == null)
            //{
            //    return HttpNotFound();
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //plan.UpdatePlan(model.Name, model.Price, model.Period, model.OrderIndex, model.PlanCode, model.Enabled);
            //_planService.Update(plan);

            //return RedirectToAction("index")
            //    .AndAlert(AlertType.Success, "Success.", "Plan updated successfully.");

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Success.", "Plan updated successfully.");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            //Plan plan = _planService.Get(id);

            //try
            //{
            //    _planService.Delete(plan);

            //    return RedirectToAction("index")
            //        .AndAlert(AlertType.Success, "Plan deleted.", "The plan was deleted successfully.");
            //}
            //catch (DataException)
            //{
            //    // the plan is in use so de-activate it
            //    plan.UpdatePlan(plan.Name, plan.Price, plan.Period, plan.OrderIndex, plan.PlanCode, false);
            //    _planService.Update(plan);

            //    return RedirectToAction("index")
            //        .AndAlert(AlertType.Success, "Plan de-activated.", "The plan was de-activated successfully.");
            //}

            return RedirectToAction("index")
                    .AndAlert(AlertType.Success, "Plan deleted.", "The plan was deleted successfully.");
        }
    }
}
