using System;

namespace SaaSPro.Domain
{
	public class PlanInfoValue : Entity
	{
		public Guid PlanId { get; protected set; }
		public Guid PlanInfoItemId { get; protected set; }

		public string Value { get; protected set; }

		public virtual Plan Plan { get; protected set; }
		public virtual PlanInfo PlanInfo { get; protected set; }
	}
}
