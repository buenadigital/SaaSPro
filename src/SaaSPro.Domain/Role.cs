using System;
using System.Collections.Generic;
using System.Linq;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
	/// <summary>
	/// Represents a User Role.
	/// </summary>
	public class Role : Entity
	{
		public Guid CustomerId { get; set; }

		/// <summary>
		/// Gets the name of the Role.
		/// </summary>
		public string Name { get; set; }

		public string UserTypeString
		{
			get { return UserType.ToString(); }
			set { UserType = EntityExtensions.ParseEnum<UserType>(value); }
		}

		/// <summary>
		/// Gets the type of user that be assigned to this role.
		/// </summary>
		public UserType? UserType { get; set; }

		/// <summary>
		/// Indicates whether the Role is a System Role and therefore should not be removed.
		/// </summary>
		public bool SystemRole { get; protected set; }
        
		/// <summary>
		/// Gets the users that are assigned to the Role.
		/// </summary>
		public virtual IEnumerable<User> Users {
			get { return RoleUsers.Select(t => t.User).ToList(); }
		} 
		public virtual ICollection<RoleUsers> RoleUsers { get; set; } = new HashSet<RoleUsers>();

		/// <summary>
		/// Creates a new <see cref="Role"/> instance.
		/// </summary>
		public Role(Customer customer, string name, bool systemRole = false, UserType? userType = null): this()
		{
			Ensure.Argument.NotNull(customer, "Customer");
			Ensure.Argument.NotNullOrEmpty(name, "name");
			CustomerId = customer.Id;
			Name = name;
			SystemRole = systemRole;
			UserType = userType;
		}

		public Role()
		{
		}

		/// <summary>
		/// Adds a <see cref="User"/> to the Role.
		/// </summary>
		/// <param name="user">The user to add.</param>
		public void AddUser(User user)
		{
			Ensure.Argument.NotNull(user, "user");
			if (!Users.Contains(user))
			{

				RoleUsers.Add(new RoleUsers
				{
					Role = this,
					RoleId = Id,
					User = user,
					UserId = user.Id
				});
			}
		}

		/// <summary>
		/// Removes a <see cref="User"/> user from the Role.
		/// </summary>
		/// <param name="user">The user to remove.</param>
		public void RemoveUser(User user)
		{
			Ensure.Argument.NotNull(user, "user");
			if (Users.Contains(user))
			{
				RoleUsers.Remove(RoleUsers.FirstOrDefault(t => t.UserId == user.Id));
			}
		}
	}
}
