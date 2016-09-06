namespace SaaSPro.Domain
{
    /// <summary>
    /// Represents the type of <see cref="Notification"/>.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Any system/application notification.
        /// </summary>
        System,

        /// <summary>
        /// The Referral Actionee has changed.
        /// </summary>
        ActioneeChanged,

        /// <summary>
        /// A new Note has been added to a Referral.
        /// </summary>
        NoteAdded
    }
}
