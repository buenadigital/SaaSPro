using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class UserNotificationMap : EntityTypeConfiguration<UserNotification>
	{
		public UserNotificationMap()
		{
			ToTable("UserNotifications");
			HasKey(t => new {t.UserId, t.MessageId});

			Property(x => x.HasRead);
			Property(x => x.CreatedOn);
			Property(x => x.UpdatedOn);

			HasRequired(t => t.User).WithMany().HasForeignKey(t=>t.UserId).WillCascadeOnDelete(false);
			HasRequired(t => t.Message).WithMany().HasForeignKey(t => t.MessageId).WillCascadeOnDelete(false);

			HasOptional(t => t.UpdatedBy).WithMany().Map(t => t.MapKey("UpdatedBy"));
			HasOptional(t => t.CreatedBy).WithMany().Map(t => t.MapKey("CreatedBy"));
		}
	}
}