using System;
using System.Data.Entity;
using System.Diagnostics;

namespace SaaSPro.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext _dbContext;
	    private DbContextTransaction _transaction;

		public EFUnitOfWork(EFDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            
            _dbContext = dbContext;
        }
        
        public void Begin(string transactionDescription)
        {
            TraceStatus("Beginning Transaction", transactionDescription);
			_transaction = _dbContext.Database.BeginTransaction();
        }

        public void End(string transactionDescription)
        {
            if (_transaction != null)
            {
	            try
	            {
		            TraceStatus("Committing Transaction", transactionDescription);
		            _dbContext.SaveChanges();
                    _transaction.Commit();
	            }
	            catch
	            {
		            TraceStatus("Rolling back Transaction", transactionDescription);
		            _transaction.Rollback();
		            throw;
	            }
	            finally
	            {
					_transaction.Dispose();
                }
            }
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                TraceStatus("Disposing");
                _dbContext.Dispose();
            }
        }

        public void Commit()
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
	            try
	            {
		            _dbContext.SaveChanges();
                    transaction.Commit();
	            }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void TraceStatus(string status, string transactionDescription = null)
        {
			//TODO: Get 
            Trace.WriteLine($"Entity Framework transaction  {transactionDescription}: {status}.");
        }
    }
}