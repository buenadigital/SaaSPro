using System.Collections.Generic;

namespace SaaSPro.Domain
{
    public class PlanInfo : Entity
    {
        public string Name { get; protected set; }
        public int OrderIndex { get; protected set; }

        public virtual ICollection<PlanInfoValue> PlanInfoValues { get; set; }
	}
}
