using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSPro.Common;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Services.Implementations
{
    public class Log4NetService : ILog4NetService
    {
        private readonly ILog4NetRepository _log4NetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Log4NetService(IUnitOfWork unitOfWork, ILog4NetRepository log4NetRepository)
        {
            _unitOfWork = unitOfWork;
            _log4NetRepository = log4NetRepository;
        }

        public IPagedList<Log4NetLog> ListErrorLogs(PagingCommand command, string name)
        {
            var pageLogs = _log4NetRepository.FetchPaged(q => q.
                Where(e => string.IsNullOrEmpty(name) || e.Logger == name).OrderByDescending(t => t.Date),
                                                                      command.PageIndex, command.PageSize);

            return pageLogs;
        }

        public LogDashboardModel Dashboard()
        {
            var logs = _log4NetRepository.Query();

            List<ErrorDashboardModel.NameCountPair> logTypes = logs.GroupBy(q => q.Level)
                .Select(q => new ErrorDashboardModel.NameCountPair
                                 {
                                     Name = q.Key,
                                     Count = q.Count()
                                 }).ToList();

            List<ErrorDashboardModel.NameCountPair> applications = logs.GroupBy(q => q.Logger)
                .Select(q => new ErrorDashboardModel.NameCountPair
                                 {
                                     Name = q.Key,
                                     Count = q.Count()
                                 }).ToList();

            StringBuilder json = new StringBuilder();
            json.Append("[");
            foreach (var errorType in logTypes)
            {
                json.Append("['" + errorType.Name + "'," + errorType.Count + "],");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("]");
            string logTypesJson = json.ToString();

            LogDashboardModel logDashboard = new LogDashboardModel
                                                 {
                                                     Applications = applications,
                                                     LogTypes = logTypes,
                                                     LogTypesJson = logTypesJson
                                                 };
            return logDashboard;
        }

        public Log4NetLog Detail(Guid id)
        {
            Log4NetLog model = _log4NetRepository.Get(id);
            return model;
        }

        public void Delete(Guid id)
        {
            Log4NetLog log4NetLog = _log4NetRepository.Get(id);
            _log4NetRepository.Delete(log4NetLog);
            _unitOfWork.Commit();
        }

        public void DeleteAll(string name)
        {
            IQueryable<Log4NetLog> log4NetLogs = _log4NetRepository.Query();
            if (!string.IsNullOrEmpty(name))
            {
                log4NetLogs = log4NetLogs.Where(e => e.Logger == name);
            }
            foreach (var log4NetLog in log4NetLogs.ToList())
            {
                _log4NetRepository.Delete(log4NetLog);
            }
            _unitOfWork.Commit();
        }
    }
}
