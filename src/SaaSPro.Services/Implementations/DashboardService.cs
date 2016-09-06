using System.Collections.Generic;
using System.Linq;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Mapping;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IPlanRepository _planRepository;
        private readonly ICustomerPaymentRepository _customerPaymentRepository;

        public DashboardService(IPlanRepository planRepository, ICustomerPaymentRepository customerPaymentRepository)
        {
            _planRepository = planRepository;
            _customerPaymentRepository = customerPaymentRepository;
        }

        public DashboardModel Dashboard()
        {
            List<Plan> list = _planRepository.Query().ToList();
            
			DashboardModel dashboard = DashboardMapper.ConvertToDashboardView(list, _customerPaymentRepository.Query().ToList());
            return dashboard;
        }

    }
}
