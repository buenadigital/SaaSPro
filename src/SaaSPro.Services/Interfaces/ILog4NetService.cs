using System;
using SaaSPro.Common;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Interfaces
{
    public interface ILog4NetService
    {
        IPagedList<Log4NetLog> ListErrorLogs(PagingCommand command, string name);
        LogDashboardModel Dashboard();
        Log4NetLog Detail(Guid id);
        void Delete(Guid id);
        void DeleteAll(string name);
    }
}
