using System.Collections.Generic;
using System.Linq;
using SaaSPro.Data;
using SaaSPro.Domain;

namespace SaasPro.TestsConfiguration.Helper
{
	public static class TestsDataInitialize
	{
		public static List<Plan> CreatePlans(EFDbContext dbContext)
		{
			var planSuper = new Plan
			{
				Name = "Small",
				Price = 699,
				Period = "monthly",
				OrderIndex = 1,
				PlanCode = "Small",
				Enabled = true
			};

			var planMedium = new Plan
			{
				Name = "Medium",
				Price = 399,
				Period = "money",
				OrderIndex = 2,
				PlanCode = "Medium",
				Enabled = true
			};

			var planSmall = new Plan
			{
				Name = "Small",
				Price = 199,
				Period = "money",
				OrderIndex = 3,
				PlanCode = "Super",
				Enabled = true
			};

			var referenceList = new ReferenceList("Security Questions");

			CreateEmailTemplates(dbContext);

			dbContext.Plans.Add(planSuper);
			dbContext.Plans.Add(planMedium);
			dbContext.Plans.Add(planSmall);
            
			dbContext.ReferenceLists.Add(referenceList);

			dbContext.SaveChanges();

			return new List<Plan> { planSuper, planMedium, planSmall };
		}

        public static void CreateEmailTemplates(EFDbContext dbContext)
        {
            var emailTemplates = new List<EmailTemplate>
            {
                new EmailTemplate("Forgot Password", "Body Text", "test@saaspro.com", "Payment"),
                new EmailTemplate("Other", "Body Text", null, null),
                new EmailTemplate("Payment", "Body Text", "test@saaspro.com", "Payment"),
                new EmailTemplate("Contact Request", "Body Text", "test@saaspro.com", "Contact Request"),
                new EmailTemplate("Sign Up Greeting", "Body Text", "test@saaspro.com", "Welcome to SaaSPro!!")
            };

            dbContext.EmailTemplates.AddRange(emailTemplates);

            dbContext.SaveChanges();
        }

        public static User CreateUser(EFDbContext dbContext)
		{
			var customer = new Customer("John", "john-domain", "Join-LTD");
			dbContext.Customers.Add(customer);

			var role = new Role
			{
				CustomerId = customer.Id,
				Name = "User",
				UserType = UserType.SystemUser,
			};
			dbContext.Roles.Add(role);
			dbContext.SaveChanges();

			var referenceList = new ReferenceList("Security Questions");
			dbContext.ReferenceLists.Add(referenceList);

			var user = new User(customer, "user@email.com", "John", "Smith", "Qwerty123");
			dbContext.Users.Add(user);
			user.RoleUsers.Add(new RoleUsers
			{
				Role = role,
				User = user,
				RoleId = role.Id,
				UserId = user.Id
			});
			dbContext.SaveChanges();

			customer.AdminUser = user;
			dbContext.SaveChanges();

			dbContext.SecurityQuestions.Add(
				new SecurityQuestion
				{
					User = user,
					UserId = user.Id,
					Question = "b",
					Answer = "AFY/TzXQKOSYGVlRGN1wqoMMkFafUlap2myEs+6wsX3Zr8NheaRhgwJCy/dVqFXiAw=="
				});
		    dbContext.SaveChanges();

			return user;
		}

	    public static ApiSessionToken CreateApiSessionToken(EFDbContext dbContext, User user)
	    {
            var apiSessionToken = new ApiSessionToken(user, 20);
            dbContext.ApiSessionTokens.Add(apiSessionToken);
	        dbContext.SaveChanges();
	        return apiSessionToken;
	    }
	}
}
