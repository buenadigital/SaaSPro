using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class ReferenceListItemMap : EntityTypeConfiguration<ReferenceListItem>
	{
		public ReferenceListItemMap()
		{
			ToTable("ReferenceListItems");
			HasKey(x => x.Id);

			Property(x => x.Value).IsRequired();
			Property(x => x.CustomerId);
		}
	}
}