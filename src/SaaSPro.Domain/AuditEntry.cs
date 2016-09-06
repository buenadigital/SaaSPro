using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
	/// <summary>
	/// Represents an entry into the audit log.
	/// </summary>
	public class AuditEntry : Entity
	{
		/// <summary>
		/// Gets the date/time of the Audit Entry.
		/// </summary>
		public DateTime TimeStamp { get; protected set; }

		/// <summary>
		/// Gets the name of the user performing the action.
		/// </summary>
		public string Username { get; protected set; }

		/// <summary>
		/// Gets the type of entity that was audited.
		/// </summary>
		public string EntityType { get; protected set; }

		/// <summary>
		/// Gets the unique identifier of the entity that was audited.
		/// </summary>
		public Guid EntityId { get; protected set; }

		/// <summary>
		/// Gets the action that was audited.
		/// </summary>
		public string Action { get; protected set; }

		/// <summary>
		/// Gets the description of the event.
		/// </summary>
		public string Description { get; protected set; }

		public Guid CustomerId { get; protected set; }

		/// <summary>
		/// Creates a new <see cref="AuditEntry"/> instance.
		/// </summary>
		public AuditEntry(Customer customer, string entityType, Guid entityId, string username, string action,
			string description)
		{
			Ensure.Argument.NotNull(customer, nameof(customer));
			Ensure.Argument.NotNullOrEmpty(entityType, nameof(entityType));
			Ensure.Argument.NotNullOrEmpty(action, nameof(action));

			CustomerId = customer.Id;
			TimeStamp = DateTime.UtcNow;
			EntityType = entityType;
			EntityId = entityId;
			Username = username;
			Action = action;
			Description = description;
		}

		public AuditEntry() { }
	}
}
