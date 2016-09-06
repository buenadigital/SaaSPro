using System;
using System.Collections.Generic;

namespace SaaSPro.Web.Common.Scheduling
{
    public class SchedulerJob
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string JobDescription { get; set; }
        public TimeSpan RepeatInterval { get; set; }
        public IDictionary<string, object> Properties { get; set; }
    }
}