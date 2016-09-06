using System;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Interfaces
{
    public interface ICustomerDashboardService
   {
        CustomerDashboardModel Dashboard(PagingCommand command,Guid customerId);
   }
}
