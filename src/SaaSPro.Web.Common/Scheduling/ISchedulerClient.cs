using System;
using System.Collections.Generic;

namespace SaaSPro.Web.Common.Scheduling
{
    public interface ISchedulerClient
    {
        IEnumerable<SchedulerJobSummary> GetJobs(string groupName = null);
        void PauseJob(string jobName, string groupName);
        void ResumeJob(string jobName, string groupName);
        SchedulerJob GetJob(string jobName, string groupName);
        void UpdateJobProperties(string jobName, string groupName, TimeSpan repeatInterval, IDictionary<string, string> properties);
    }
}