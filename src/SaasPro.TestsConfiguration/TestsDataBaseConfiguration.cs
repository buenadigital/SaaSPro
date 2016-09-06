using System;
using System.Data.Common;
using System.IO;
using SaaSPro.Data;

namespace SaasPro.TestsConfiguration
{
	public class TestsDataBaseConfiguration : IDisposable
	{
		private EFDbContext _dbContext;

		private readonly string _currentFile;

		public EFDbContext DbContext => _dbContext;
        public string ConnectionString { get; private set; }

        public TestsDataBaseConfiguration()
		{
			_currentFile = Path.GetTempFileName();
            ConnectionString = $"Data Source={_currentFile};Persist Security Info=False";

			var conn = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0").CreateConnection();
			if (conn != null)
			{
				conn.ConnectionString = ConnectionString;
				conn.Open();
				_dbContext = new EFDbContext(conn);
			}

			_dbContext.Database.CreateIfNotExists();
		}

		public void Dispose()
		{
			_dbContext.Dispose();
			_dbContext = null;

			File.Delete(_currentFile);
		}
	}
}
