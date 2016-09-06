using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;
namespace SaaSPro.Data.Mapping
{
	public class RoleUsersMap : EntityTypeConfiguration<RoleUsers> 
	{
		public RoleUsersMap()
		{
			ToTable("RoleUsers");
			HasKey(x => new { x.RoleId, x.UserId });

			HasRequired(x => x.User)
				.WithMany(x => x.RoleUsers)
				.HasForeignKey(x => x.UserId)
				.WillCascadeOnDelete(false);

			HasRequired(x => x.Role)
				.WithMany(x => x.RoleUsers)
				.HasForeignKey(x => x.RoleId)
				.WillCascadeOnDelete(false);
		}
	}
}