using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class EmailTemplateMap : AuditedClassMap<EmailTemplate>
	{
		public EmailTemplateMap()
		{
			ToTable("EmailTemplates");
			HasKey(x => x.Id);

			Property(x => x.TemplateName);
			Property(x => x.FromEmail);
			Property(x => x.Subject);
			Property(x => x.Body).HasColumnType("ntext");
			Property(x => x.Template).HasColumnType("ntext");

			Ignore(t => t.To);
		}
	}
}