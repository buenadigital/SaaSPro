using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.RoleService;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Tests
{
	[TestClass]
	public class RoleServiceTests : TestsConfiguration
	{
		private IRoleService _roleService => Container.GetInstance<IRoleService>();

		private static Customer _customer;

		private static List<Role> _roles;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();

			var dbContext = Container.GetInstance<EFDbContext>();
			var customer = new Customer("John", "john-domain", "Join-LTD");
			dbContext.Customers.Add(customer);

			_roles = new List<Role>();
			_roles.AddRange(new[]
			{
				new Role
				{
					CustomerId = customer.Id,
					Name = "Admin"
				},
					new Role
				{
					CustomerId = customer.Id,
					Name = "User"
				}
			});

			dbContext.Roles.AddRange(_roles);
			dbContext.SaveChanges();
			_customer = customer;
		}

		[TestMethod]
		public void T01_RolesList()
		{
			var roleList = _roleService.RolesList(_customer.Id);
			Assert.AreEqual(2, roleList.Roles.Count(), "Expected roles count is not much with real");
		}

		[TestMethod]
		public void T02_RolesModifications()
		{
			var rolesUpdateModel = new RolesUpdateModel
			{
				Name = "CustomRole",
				UserType = UserType.SystemUser
			};

			var role = new AddRoleRequest
			{
				Customer = _customer,
				RolesUpdateModel = rolesUpdateModel
			};
			_roleService.AddRole(role);

			var roleSummury = _roleService.RolesList(_customer.Id).Roles.FirstOrDefault(t => t.Name == "CustomRole");
			Assert.IsNotNull(roleSummury, "Can not add role");

			var roleResponse = _roleService.Get(new GetRoleRequest { Id = roleSummury.Id } );

			rolesUpdateModel.Name = "CustomRoleReName";
			_roleService.UpdateRole(new UpdateRoleRequest
			{
				Id = roleResponse.RolesUpdateModel.Id,
				RolesUpdateModel = rolesUpdateModel
			});

			roleSummury = _roleService.RolesList(_customer.Id).Roles.FirstOrDefault(t => t.Name == "CustomRoleReName");
			Assert.IsNotNull(roleSummury, "Can not edit role");

			_roleService.DeleteRole(new DeleteRoleRequest {Id = roleSummury.Id});
			roleSummury = _roleService.RolesList(_customer.Id).Roles.FirstOrDefault(t => t.Name == "CustomRoleReName");
			Assert.IsNull(roleSummury, "Can not delete role");
		}

		[ClassCleanup()]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
