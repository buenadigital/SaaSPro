using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class ApiTokenMap : AuditedClassMap<ApiToken>
	{
		public ApiTokenMap()
		{
			ToTable("ApiTokens");
			HasKey(c => c.Id);

			Property(c => c.Name).IsRequired();
			Property(c => c.Token).IsRequired();
			Property(c => c.CustomerId);
		}
	}
}