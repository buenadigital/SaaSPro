using System;
using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class IPSListModel
    {
        public IEnumerable<IPSEntrySummary> Entries { get; set; } 

        public class IPSEntrySummary
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string StartIPAddress { get; set; }
            public string EndIPAddress { get; set; }
        }
    }
}