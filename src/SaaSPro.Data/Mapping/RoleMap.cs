using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;
namespace SaaSPro.Data.Mapping
{
	public class RoleMap : EntityTypeConfiguration<Role>
	{
		public RoleMap()
		{
			ToTable("Roles");
			HasKey(r => r.Id);

			Property(r => r.Name);
			Property(r => r.SystemRole);
			Property(r => r.CustomerId);
			Property(r => r.UserTypeString).HasColumnName("UserType").IsOptional();

			Ignore(t => t.UserType);
			Ignore(t => t.Users);
		}
	}
}