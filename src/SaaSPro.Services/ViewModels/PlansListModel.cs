using System;
using SaaSPro.Common;

namespace SaaSPro.Services.ViewModels
{
    public class PlansListModel
    {
        public IPagedList<PlanSummary> Plans { get; set; }

        public class PlanSummary
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string PlanCode { get; set; }
        }
    }
}