using System;

namespace SaaSPro.Domain
{
	public class Project : AuditedEntity
	{
		public string Name { get; set; }
		public Guid CustomerId { get; set; }
	}
}
