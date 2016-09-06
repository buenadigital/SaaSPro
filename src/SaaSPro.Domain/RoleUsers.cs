using System;

namespace SaaSPro.Domain
{
	public class RoleUsers
	{
		public Guid RoleId { get; set; }
		public Guid UserId { get; set; }

		public virtual Role Role { get; set; }
		public virtual User User { get; set; }
	}
}
