using System;
using SaaSPro.Common;

namespace SaaSPro.Services.ViewModels
{
    public class CustomerDashboardModel
    {
        public IPagedList<ProjectSummary> Projects { get; set; }

        public class ProjectSummary
        {
            public string Name { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
