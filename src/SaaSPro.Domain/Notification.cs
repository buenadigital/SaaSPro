using System;
using System.Collections.Generic;
using System.Linq;

namespace SaaSPro.Domain
{
	/// <summary>
	/// Represents a Notification to a user(s) of the application.
	/// </summary>
	public class Notification : NotificationMessage
	{
		/// <summary>
		/// The user ids of the recipients of the Notification.
		/// </summary>
		public IEnumerable<Guid> Recipients { get; private set; }

		/// <summary>
		/// Whether the Notification should be sent to all users of the system.
		/// </summary>
		public bool SendToAllUsers { get; private set; }

		/// <summary>
		/// Creates a new <see cref="Notification"/> instance.
		/// </summary>
		internal Notification(
			NotificationType notificationType,
			string subject,
			IEnumerable<Guid> recipients,
			string body = null,
			Guid? referenceId = null,
			User sender = null,
			bool sendToAllUsers = false)
			: base(notificationType,
				subject,
				body,
				referenceId,
				sender)
		{
			if (!sendToAllUsers && !recipients.Any())
			{
				throw new ArgumentException("You must specify at least one recipient.", nameof(recipients));
			}

			Recipients = recipients;
			SendToAllUsers = sendToAllUsers;
		}

		public Notification()
		{
		}
	}
}
