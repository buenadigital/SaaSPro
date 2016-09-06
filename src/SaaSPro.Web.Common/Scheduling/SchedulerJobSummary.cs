using System;

namespace SaaSPro.Web.Common.Scheduling
{
    public class SchedulerJobSummary
    {
        public string GroupName { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroupName { get; set; }
        public string TriggerType { get; set; }
        public string TriggerState { get; set; }
        public DateTime? NextFireTime { get; set; }
        public DateTime? PreviousFireTime { get; set; }
        public TimeSpan RepeatInterval { get; set; }

        public bool IsPaused => TriggerState == "Paused";
    }
}