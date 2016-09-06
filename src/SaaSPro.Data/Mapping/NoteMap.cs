using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class NoteMap : AuditedClassMap<Note>
	{
		public NoteMap()
		{
			ToTable("Notes");
			HasKey(x => x.Id);

			Property(x => x.NoteContent).HasColumnName("Note");
			Property(x => x.CustomerId);

			HasRequired(t => t.Customer)
				.WithMany(t => t.Notes)
				.HasForeignKey(t => t.CustomerId);
		}
	}
}
