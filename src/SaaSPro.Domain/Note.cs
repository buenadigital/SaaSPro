using System;

namespace SaaSPro.Domain
{
    public class Note : AuditedEntity
    {
        public Guid CustomerId { get; set; }
        public string NoteContent { get;  set; }

        public virtual Customer Customer { get; set; }
    }
}