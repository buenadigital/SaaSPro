using System;

namespace SaaSPro.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin(string transactionDescription = null);
        void End(string transactionDescription = null);
        void Commit();
    }
}