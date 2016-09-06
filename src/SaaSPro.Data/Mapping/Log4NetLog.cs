using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class Log4NetLogMap : EntityTypeConfiguration<Log4NetLog>
	{
		public Log4NetLogMap()
		{
			ToTable("Log");
			HasKey(r => r.Id)
				.Property(r => r.Id)
				.HasColumnName("LogId");

			Property(r => r.Date);
			Property(r => r.Thread);
			Property(r => r.Level);
			Property(r => r.Logger);
			Property(r => r.Message);
			Property(r => r.Exception);
		}
	}
}