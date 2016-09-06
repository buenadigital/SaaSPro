using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class ProjectMap : AuditedClassMap<Project>
	{
		public ProjectMap()
		{
			ToTable("Projects");
			HasKey(x => x.Id);

			Property(x => x.CustomerId);
			Property(x => x.Name);
		}
	}
}