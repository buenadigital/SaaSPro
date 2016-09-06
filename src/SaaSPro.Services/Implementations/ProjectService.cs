using System;
using System.Linq;
using SaaSPro.Common;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Web.Common;
using SaasPro.Data.Repositories;
using AutoMapper;

namespace SaaSPro.Services.Implementations
{
    public class ProjectService : ICustomerDashboardService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public CustomerDashboardModel Dashboard(PagingCommand command, Guid customerId)
        {
            IPagedList<Project> projects = _projectRepository.FetchPaged(q => q.Where(p => p.CustomerId == customerId), command.PageIndex,
                                                                command.PageSize);

            var model = new CustomerDashboardModel
            {
                Projects = Mapper.Engine.MapPaged<Project, CustomerDashboardModel.ProjectSummary>(projects)
            };
            return model;
        }
    }
}