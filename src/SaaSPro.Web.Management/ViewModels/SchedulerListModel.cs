using SaaSPro.Web.Common.Scheduling;
using System;
using System.Collections.Generic;

namespace SaaSPro.Web.Management.ViewModels
{
    public class SchedulerListModel
    {
        public IEnumerable<JobSummary> Jobs { get; set; }

        public class JobSummary : SchedulerJobSummary
        {
            public Guid CustomerId { get; set; }
            public string CustomerName { get; set; }
        }
    }
}