using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class ReferenceListMap : EntityTypeConfiguration<ReferenceList>
	{
		public ReferenceListMap()
		{
			ToTable("ReferenceLists");
			HasKey(x => x.Id);

			Property(x => x.SystemName)
				.IsRequired();

			HasMany(x => x.Items)
				.WithRequired()
				.Map(t => t.ToTable("ReferenceListItems").MapKey("ReferenceListId"))
				.WillCascadeOnDelete(true);
		}
	}
}
