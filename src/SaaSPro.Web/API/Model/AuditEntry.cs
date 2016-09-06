using System;

namespace SaaSPro.Web.API.Model
{
    public class AuditEntry
    {
        public DateTime TimeStamp { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}