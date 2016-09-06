using System;

namespace SaaSPro.Domain
{
    /// <summary>
    /// Base class for entities whose changes should be audited.
    /// </summary>
    public class AuditedEntity : Entity
    {
        public AuditedEntity()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }
		public virtual User CreatedBy { get; set; }
		public DateTime UpdatedOn { get; set; }
		public virtual User UpdatedBy { get; set; }
    }
}
