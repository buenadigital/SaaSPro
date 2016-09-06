using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class UserMap : AuditedClassMap<User>
	{
		public UserMap()
		{
			ToTable("Users");
			HasKey(u => u.Id);

			Property(u => u.Email).IsRequired();
			Property(u => u.FirstName).IsRequired();
			Property(u => u.LastName).IsRequired();
			Property(u => u.Password).IsRequired();
			Property(u => u.PasswordExpiryDate).IsRequired();
			Property(u => u.RegisteredDate).IsRequired();
			Property(u => u.Enabled).IsRequired();
			Property(u => u.LastLoginIP);
			Property(u => u.LastLoginDate);
			Property(u => u.PasswordResetToken);
			Property(u => u.PasswordResetTokenExpiry);
			Property(u => u.CustomerId);

			Property(r => r.UserTypeString).HasColumnName("UserType").IsRequired();

			HasMany(t => t.SecurityQuestions)
				.WithRequired(t => t.User)
				.HasForeignKey(t => t.UserId)
				.WillCascadeOnDelete(true);

            HasMany(t => t.ApiSessionTokens)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(true);

            Ignore(t => t.UserType);
			Ignore(t => t.Roles);
		}
	}
}

