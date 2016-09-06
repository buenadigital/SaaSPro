using SaaSPro.Services.Interfaces;
using SaaSPro.Web.ViewModels;
using System;
using System.Web.Mvc;

namespace SaaSPro.Web.Controllers
{
    public class AuditLogController : SaaSProControllerBase
    {
        private readonly IAuditEntryService _auditEntryService;

        public AuditLogController(IAuditEntryService auditEntryService)
        {
            _auditEntryService = auditEntryService;
        }
        
        [HttpGet]
        public ActionResult Details(string entityType, Guid id)
        {
            var entries = _auditEntryService.GetEntries(entityType, id, Customer.OriginalCustomer);
            var model = new AuditLogDetailsModel
            {
                EntityType = entityType,
                EntityId = id,
                Entries = entries
            };
            
            return View(model);
        }
    }
}