using SaaSPro.Domain;
using System;
using System.Collections.Generic;

namespace SaaSPro.Web.ViewModels
{
    public class AuditLogDetailsModel
    {
        public string EntityType { get; set; }
        public Guid EntityId { get; set; }
        public IEnumerable<AuditEntry> Entries { get; set; }
    }
}