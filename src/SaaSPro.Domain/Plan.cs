using System.Collections.Generic;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
	public class Plan : AuditedEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Period { get; set; }
		public int OrderIndex { get; set; }
		public string PlanCode { get; set; }
		public bool Enabled { get; set; }

		public virtual ICollection<Customer> Customers { get; set; }
		public virtual ICollection<PlanInfoValue> PlanInfoValues { get; set; }

		public Plan(string name, decimal price, string period, int orderIndex, string planCode, bool enabled)
		{
			Name = name;
			Price = price;
			Period = period;
			OrderIndex = orderIndex;
			PlanCode = planCode;
			Enabled = enabled;
		}

		public Plan()
		{
		}

		public void UpdatePlan(string name, decimal price, string period, int orderIndex, string planCode,
			bool enabled)
		{
			Ensure.Argument.NotNullOrEmpty(name, "name");
			Ensure.Argument.NotNull(price, "price");
			Ensure.Argument.NotNullOrEmpty(period, "period");
			Ensure.Argument.NotNull(orderIndex, "orderIndex");
			Ensure.Argument.NotNullOrEmpty(planCode, "planCode");
			Ensure.Argument.NotNull(enabled, "enabled");

			Name = name;
			Price = price;
			Period = period;
			OrderIndex = orderIndex;
			PlanCode = planCode;
			Enabled = enabled;
		}
	}
}
