using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaaSPro.Data;
using SaaSPro.Infrastructure.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Payment;
using SaaSPro.Web;
using StructureMap;
using StructureMap.Pipeline;
using SaaSPro.Data.Repositories;

namespace SaasPro.TestsConfiguration
{
	[TestClass]
	public abstract class TestsConfiguration
	{
		private static TestsDataBaseConfiguration _dataBaseConfiguration;
		public static Container Container;

		public static void ClassInitialize()
		{
			_dataBaseConfiguration = new TestsDataBaseConfiguration();
			Container = new Container(c =>
			{
				c.For<EFDbContext>().Use(_dataBaseConfiguration.DbContext).LifecycleIs<SingletonLifecycle>();
				c.AddRegistry<TestsRegister>();
				c.For<ICustomerResolver>().Use(new DatabaseCustomerResolver(_dataBaseConfiguration.ConnectionString));
            });

            TestsAutoMapperConfig.Register();

			EmailServiceFactory.InitializeEmailServiceFactory(Container.GetInstance<IMailService>());
			ApplicationSettingsFactory.InitializeApplicationSettingsFactory(Container.GetInstance<IApplicationSettings>());
			StripeFactory.InitializeStripeFactory(Container.GetInstance<IStripeService>());
		}

		protected bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
		{
			var cnt = new Dictionary<T, int>();
			foreach (T s in list1)
			{
				if (cnt.ContainsKey(s))
				{
					cnt[s]++;
				}
				else
				{
					cnt.Add(s, 1);
				}
			}
			foreach (T s in list2)
			{
				if (cnt.ContainsKey(s))
				{
					cnt[s]--;
				}
				else
				{
					return false;
				}
			}
			return cnt.Values.All(c => c == 0);
		}

		protected IList<ValidationResult> Validate(object model)
		{
			var results = new List<ValidationResult>();
			var validationContext = new ValidationContext(model, null, null);
			Validator.TryValidateObject(model, validationContext, results, true);
			(model as IValidatableObject)?.Validate(validationContext);
			return results;
		}

		public static void Cleanup()
		{
			_dataBaseConfiguration.Dispose();
		}

	}
}
