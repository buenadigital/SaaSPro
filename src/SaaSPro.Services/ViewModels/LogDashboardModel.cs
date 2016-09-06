using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class LogDashboardModel
    {
        public List<ErrorDashboardModel.NameCountPair> LogTypes { get; set; }
        public List<ErrorDashboardModel.NameCountPair> Applications { get; set; }
        public string LogTypesJson { get; set; }
    }
}
