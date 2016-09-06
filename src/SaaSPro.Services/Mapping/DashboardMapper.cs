using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Mapping
{
    public class DashboardMapper
    {
        public static DashboardModel ConvertToDashboardView(List<Plan> plans, List<CustomerPayment> customerPayments)
        {
            CultureInfo ci = new CultureInfo("en-us")
            {
                NumberFormat = { CurrencyNegativePattern = 1 }
            };
            List<Subscription> subscriptions = new List<Subscription>();
            StringBuilder json = new StringBuilder();

            var today = DateTime.Now;
            var numberOfDaysInThisMonth = DateTime.DaysInMonth(today.Year, today.Month);
            var startOfThisMonth = new DateTime(today.Year, today.Month, 1);
            var endOfThisMonth = new DateTime(today.Year, today.Month, numberOfDaysInThisMonth);

            var lastMonth = today.Month == 1 ? 12 : today.Month - 1;
            var lastYear = lastMonth == 12 ? today.Year - 1 : today.Year;

            var numberOfDaysInLastMonth = DateTime.DaysInMonth(lastYear, lastMonth);
            var startOfLastMonth = new DateTime(lastYear, lastMonth, 1);
            var endOfLastMonth = new DateTime(lastYear, lastMonth, numberOfDaysInLastMonth);

            json.Append("[");
            foreach (Plan plan in plans)
            {
                decimal churn = plan.Customers != null ? GetChurn(plan.Customers, customerPayments, startOfThisMonth, endOfThisMonth, startOfLastMonth, endOfLastMonth) : 0;
                decimal mrr = plan.Customers != null ? GetMRR(plan.Customers, customerPayments, startOfThisMonth, endOfThisMonth) : 0;
                subscriptions.Add(new Subscription
                                                {
                                                    Plan = plan.Name,
                                                    //ARPU is total payments this month for a plan divided by the total number of customers subscribed to that plan
                                                    ARPU = (plan.Customers != null && plan.Customers.Any() ? mrr / plan.Customers.Count : 0).ToString("C", ci),
                                                    Churn = churn.ToString("0.##") + "%",
                                                    // The total revenue for the plan till date. Calculated by sum of all payments made by customers of this plan
                                                    GrossRevenue = plan.Customers != null ? customerPayments.Where(t => plan.Customers.Select(x => x.Id).Contains(t.CustomerId)).Sum(tp => tp.Amount).ToString("C", ci) : "$0",
                                                    MRR = mrr.ToString("C", ci),
                                                    //LTV = MRR/Churn
                                                    //Source: https://baremetrics.io/academy/saas-calculating-ltv
                                                    LTV = (churn != 0 ? mrr / (churn / 100) : 0).ToString("C", ci),
                                                    Price = plan.Price.ToString("C", ci),
                                                    PlanCode = plan.PlanCode,
                                                    TotalCustomers = (plan.Customers?.Count ?? 0).ToString(ci)
                                                });

                json.Append("{ name:'" + plan.Name + "',y:" + (plan.Customers?.Count ?? 0) + "},");
            }

            json.Remove(json.Length - 1, 1);
            json.Append("]");

            string subscriptionDistributionsJson = json.ToString();
            decimal customerChurn = GetChurn(null, customerPayments, startOfThisMonth, endOfThisMonth, startOfLastMonth, endOfLastMonth);
            decimal netMrr = GetMRR(null, customerPayments, startOfThisMonth, endOfThisMonth);

            var dashboard = new DashboardModel
            {
                CustomerChurn = customerChurn.ToString("0.##") + "%",
                LTV = (customerChurn != 0 ? netMrr / (customerChurn / 100) : 0).ToString("C", ci),
                MRR = netMrr.ToString("C", ci),
                MonthlyRevenue = GetMonthlyRevenues(customerPayments),
                // Subscription values are for the current month
                Subscriptions = subscriptions,
                SubscriptionChartJson = subscriptionDistributionsJson
            };

            return dashboard;
        }

        /// <summary>
        /// Gets Customer Churn rate for current month by evaluating difference between number of customers 
        /// who made payment last month and the number of customers who made payment this month. Difference is the 
        /// number of customers who are no longer paying/subscribed.
        /// Source : http://www.happybootstrapper.com/2013/calculate-saas-churn-rate-from-db/
        /// </summary>
        public static decimal GetChurn(IEnumerable<Customer> customers, IEnumerable<CustomerPayment> customerPayments, DateTime startOfThisMonth, DateTime endOfThisMonth, DateTime startOfLastMonth, DateTime endOfLastMonth)
        {
            IEnumerable<CustomerPayment> currentCustomerPayments = customers == null ? customerPayments.ToList() : customerPayments.Where(t => customers.Select(x => x.Id).Contains(t.CustomerId)).ToList();
            int countThisMonth = currentCustomerPayments.Where(t => t.CreatedOn >= startOfThisMonth && t.CreatedOn <= endOfThisMonth).Select(t => t.CustomerId).Distinct().Count();
            int countLastMonth = currentCustomerPayments.Where(t => t.CreatedOn >= startOfLastMonth && t.CreatedOn <= endOfLastMonth).Select(t => t.CustomerId).Distinct().Count();
            int churnAmount = countLastMonth - countThisMonth;
            decimal churn = countLastMonth > 0 ? (Convert.ToDecimal(churnAmount) / countLastMonth) * 100 : 0;
            return churn;
        }

        /// <summary>
        /// Assuming subscription to be monthly, MRR for a plan is the total payment for that plan in current month.
        /// So the sum of payment amount paid by customers of the given plan in current month gives MRR
        /// Source: http://blog.chargebee.com/mrr-subscription-businesses-saas-metrics-101/
        /// </summary>
        public static decimal GetMRR(IEnumerable<Customer> customers, IEnumerable<CustomerPayment> customerPayments, DateTime startOfThisMonth, DateTime endOfThisMonth)
        {
            IEnumerable<CustomerPayment> currentCustomerPayments = customers == null ? customerPayments : customerPayments.Where(t => customers.Select(x => x.Id).Contains(t.CustomerId));
            decimal paymentThisMonth = currentCustomerPayments.Where(t => t.CreatedOn >= startOfThisMonth && t.CreatedOn <= endOfThisMonth).Sum(t => t.Amount);
            return paymentThisMonth;
        }

        /// <summary>
        /// The monthly revenue is the sum of all payments for respective month by all customers
        /// </summary>
        public static MonthlyRevenue GetMonthlyRevenues(IEnumerable<CustomerPayment> customerPayments)
        {
            var monthlyPayment = customerPayments.OrderBy(tp => tp.CreatedOn).GroupBy(tp => new { tp.CreatedOn.Month, tp.CreatedOn.Year }).Select(tp => new
                {
                    Month = new DateTime(DateTime.Now.Year, tp.Key.Month, 1).ToString("MMM", CultureInfo.InvariantCulture) + ", " + tp.Key.Year,
                    Revenue = tp.Sum(a => a.Amount)
                }).ToList();

            var monthlyRevenue = new MonthlyRevenue
            {
                Month = monthlyPayment.Select(t => t.Month).ToArray(),
                Revenue = monthlyPayment.Select(t => t.Revenue).ToArray()
            };

            return monthlyRevenue;
        }
    }
}
