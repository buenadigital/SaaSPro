using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class DashboardModel
    {
        public List<Subscription> Subscriptions { get; set; }
        public string MRR { get; set; }
        public string CustomerChurn { get; set; }
        public string LTV { get; set; }
        public MonthlyRevenue MonthlyRevenue { get; set; }
        public string SubscriptionChartJson { get; set; }
    }

    public class Subscription
    {
        public string PlanCode { get; set; }
        public string Plan { get; set; }
        public string Price { get; set; }
        public string MRR { get; set; }
        public string Churn { get; set; }
        public string LTV { get; set; }
        public string ARPU { get; set; }
        public string GrossRevenue { get; set; }
        public string TotalCustomers { get; set; }
    }

    public class MonthlyRevenue
    {
        public decimal[] Revenue { get; set; }
        public string[] Month { get; set; }
    }
}
