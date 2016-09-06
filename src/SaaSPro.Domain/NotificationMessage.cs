using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
	/// <summary>
	/// Represents the details of a Notification that has been sent to any number of recipients.
	/// </summary>
	public class NotificationMessage : AuditedEntity
	{
		public string NotificationTypeString
		{
			get { return NotificationType.ToString(); }
			private set { NotificationType = EntityExtensions.ParseEnum<NotificationType>(value); }
		}

		/// <summary>
		/// The type of Notification.
		/// </summary>
		public NotificationType NotificationType { get; protected set; }

		/// <summary>
		/// The subject of the Notification.
		/// </summary>
		public string Subject { get; protected set; }

		/// <summary>
		/// The body of the Notification.
		/// </summary>
		public string Body { get; protected set; }

		/// <summary>
		/// The identifier of the related entity (e.g. Referral).
		/// The entity type will be determined from the <see cref="NotificationType"/>.
		/// </summary>
		public Guid? ReferenceId { get; protected set; }

		/// <summary>
		/// The User who sent the Notification. Null for system generated Notifications.
		/// </summary>
		public virtual User Sender { get; protected set; }

		public NotificationMessage(
			NotificationType notificationType,
			string subject,
			string body = null,
			Guid? referenceId = null,
			User sender = null)
		{
			Ensure.Argument.NotNullOrEmpty(subject, "subject");
			NotificationType = notificationType;
			Subject = subject;
			Body = body;
			ReferenceId = referenceId;

			CreatedOn = DateTime.UtcNow;
		}

		public NotificationMessage()
		{
		}

		/// <summary>
		/// Gets the Name of the Sender.
		/// </summary>
		public string SenderName => Sender != null ? Sender.FullName : "System";
	}
}
