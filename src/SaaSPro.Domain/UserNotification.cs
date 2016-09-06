using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    /// <summary>
    /// Represents a User copy of a notification. Holds a reference to the original <see cref="NotificationMessage"/>.
    /// </summary>
    public class UserNotification : IEquatable<UserNotification>
    {
		public Guid UserId { get; set; }
		/// <summary>
		/// The User to which the otification belongs.
		/// </summary>
		public User User { get; protected set; }

		public Guid MessageId { get; set; }
		/// <summary>
		/// The notification contents/details.
		/// </summary>
		public NotificationMessage Message { get; protected set; }

        /// <summary>
        /// Whether the notification has been read/viewed by the user.
        /// </summary>
        public bool HasRead { get; protected set; }
        
        public DateTime CreatedOn { get; set; }
        public User CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public User UpdatedBy { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserNotification"/> instance.
        /// </summary>
        public UserNotification(User user, NotificationMessage message)
        {
            Ensure.Argument.NotNull(user, "user");
            Ensure.Argument.NotNull(message, "message");

            User = user;
            Message = message;
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

        protected UserNotification()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserNotification);
        }
        
        public bool Equals(UserNotification other)
        {
            if (other == null)
            {
                return false;
            }

            return other.User == User && other.Message == Message;
        }

        public override int GetHashCode()
        {
            return User.Id.GetHashCode() ^ Message.Id.GetHashCode();
        }
    }
}
