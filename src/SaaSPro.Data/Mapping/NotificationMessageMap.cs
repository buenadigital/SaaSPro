using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class NotificationMessageMap : AuditedClassMap<NotificationMessage>
	{
		public NotificationMessageMap()
		{
			ToTable("NotificationMessages");
			HasKey(p => p.Id);

			Property(p => p.NotificationTypeString)
				.HasColumnName("NotificationType")
				.HasColumnType("ntext");
			Property(p => p.Subject);
			Property(p => p.Body);
			Property(p => p.ReferenceId).IsOptional();

			HasOptional(p => p.Sender).WithMany().Map(t => t.MapKey("SenderId"));

			Ignore(p => p.NotificationType);
		}
	}
}