using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.RoleService;
using SaaSPro.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class RolesController : AdminControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            RolesListModel model = _roleService.RolesList(Customer.CustomerId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RolesAddModel model)
        {
            AddRoleRequest request = new AddRoleRequest
            {
                RolesUpdateModel = model,
                Customer = Customer.OriginalCustomer
            };

            _roleService.AddRole(request);

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Success.", "Role added successfully.");
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            GetRoleResponse response = _roleService.Get(new GetRoleRequest() {Id = id});


            if (response.HasError)
            {
                return HttpNotFound();
            }

            if (response.SystemRole)
            {
                return RedirectToAction("index")
                    .AndAlert(AlertType.Danger, "Invalid Operation.", "System roles cannot be modified.");
            }

            return View(response.RolesUpdateModel);
        }

        [HttpPost]
        public ActionResult Update(Guid id, RolesUpdateModel model)
        {
            UpdateRoleRequest request = new UpdateRoleRequest
            {
                RolesUpdateModel = model,
                Id = id
            };
            UpdateRoleResponse response = _roleService.UpdateRole(request);

            if (response.HasError)
            {
                return HttpNotFound();
            }
            return RedirectToAction("update", new {id = id}).AndAlert(AlertType.Success, "Success.",
                                                                      "Role updated successfully.");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            DeleteRoleResponse response = _roleService.DeleteRole(new DeleteRoleRequest() { Id = id });

            if (response.HasError)
            {
                return HttpNotFound();
            }

            if (response.SystemRole)
            {
                return RedirectToAction("index")
                    .AndAlert(AlertType.Danger, "Invalid Operation.", "System roles cannot be deleted.");
            }

            return RedirectToAction("index")
                .AndAlert(AlertType.Warning, "Deleted.", "The role was deleted successfully.");
        }
    }
}