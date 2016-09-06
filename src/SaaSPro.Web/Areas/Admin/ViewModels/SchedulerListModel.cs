using SaaSPro.Web.Common.Scheduling;
using System.Collections.Generic;

namespace SaaSPro.Web.Areas.Admin.ViewModels
{
    public class SchedulerListModel
    {
        public IEnumerable<SchedulerJobSummary> Jobs { get; set; }
    }
}