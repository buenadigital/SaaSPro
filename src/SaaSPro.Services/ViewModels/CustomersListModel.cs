using System;
using SaaSPro.Common;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class CustomersListModel
    {
        public IPagedList<CustomerSummary> Customers { get; set; }

        public class CustomerSummary
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string Company { get; set; }
            public string Hostname { get; set; }
            public string AdminEmail { get; set; }
            public bool Enabled { get; set; }
            [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
            public DateTime CreatedOn { get; set; }
        }
    }
}