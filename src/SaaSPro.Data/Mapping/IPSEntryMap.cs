using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class IPSEntryMap : AuditedClassMap<IPSEntry>
	{
		public IPSEntryMap()
		{
			ToTable("IPS");
			HasKey(x => x.Id);

			Property(x => x.Name);
			Property(x => x.StartBytes).IsRequired();
			Property(x => x.EndBytes).IsRequired();
			Property(x => x.CustomerId);
		}
	}
}
