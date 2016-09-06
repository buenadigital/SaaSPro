using System;
using System.Collections.Generic;
using System.Linq;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.RoleService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using AutoMapper;

namespace SaaSPro.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IUnitOfWork unitOfWork,IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        public RolesListModel RolesList(Guid customerId)
        {
            var roles = _roleRepository.Query().Where(r => r.CustomerId == customerId).OrderBy(r => r.Name).ToList();

            var model = new RolesListModel
            {
                Roles = Mapper.Map<IEnumerable<RolesListModel.RoleSummary>>(roles)
            };

            return model;
        }


        public void AddRole(AddRoleRequest request)
        {
            var role = new Role(request.Customer, request.RolesUpdateModel.Name, userType: request.RolesUpdateModel.UserType);
            _roleRepository.Add(role);
            _unitOfWork.Commit();
        }

        public GetRoleResponse Get(GetRoleRequest request)
        {
            GetRoleResponse response=new GetRoleResponse();

            Role role = _roleRepository.Get(request.Id);

            response.SystemRole = role.SystemRole;

            response.RolesUpdateModel=Mapper.Map<RolesUpdateModel>(role);

            return response;
        }

        public DeleteRoleResponse DeleteRole(DeleteRoleRequest request)
        {
            DeleteRoleResponse  response=new DeleteRoleResponse();

            Role role = _roleRepository.Get(request.Id);

            if(role==null)
            {
                response.HasError = true;
                return response;
            }

            if(role.SystemRole)
            {
                response.SystemRole = true;
                return response;
            }
            _roleRepository.Delete(role);
            _unitOfWork.Commit();

            return response;
        }

        public UpdateRoleResponse UpdateRole(UpdateRoleRequest request)
        {
            UpdateRoleResponse response = new UpdateRoleResponse();

            Role role = _roleRepository.Get(request.Id);

            role.Name = request.RolesUpdateModel.Name;
            role.UserType = request.RolesUpdateModel.UserType;

            _roleRepository.Update(role);
            _unitOfWork.Commit();

            return response;
        }
    }
}
