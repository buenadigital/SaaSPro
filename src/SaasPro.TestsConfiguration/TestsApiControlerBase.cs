using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Data;
using SaaSPro.Domain;
using SaaSPro.Web;

namespace SaasPro.TestsConfiguration
{
	[TestClass]
	public abstract class TestsApiControlerBase<T> : TestsConfiguration where T : ApiController
	{
		public static User User { get; set; }
	    public static Customer Customer { get; set; }
	    public static ApiSessionToken ApiSessionToken { get; set; }

	    public static T Controller;

		public new static void ClassInitialize()
		{
			TestsConfiguration.ClassInitialize();
		    var dbContext = Container.GetInstance<EFDbContext>();

			User = TestsDataInitialize.CreateUser(dbContext);
		    Customer = dbContext.Customers.First(c => c.Id == User.CustomerId);
		    ApiSessionToken = TestsDataInitialize.CreateApiSessionToken(dbContext, User);

			var httpContextInitialize = new TestsHttpContextInitialize(new Uri("http://john-domain.saaspro.net/"), User, Container.GetInstance<ICustomerHost>());
			HttpContext.Current = httpContextInitialize.GetTestApiHttpContext(ApiSessionToken);
		    HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] = Customer;
		}

		public virtual void TestInitialize()
		{
			var httpContextInitialize = new TestsHttpContextInitialize(new Uri("http://john-domain.saaspro.net/"), User, Container.GetInstance<ICustomerHost>());

			if (HttpContext.Current == null)
			{
				HttpContext.Current = httpContextInitialize.GetTestApiHttpContext(ApiSessionToken);
                HttpContext.Current.Items[Constants.CurrentCustomerInstanceKey] = Customer;
            }
		}

        public static void ValidateApiModel(object input)
        {
            var provider = new DataAnnotationsModelValidatorProvider();
            var metadata = ModelMetadataProviders.Current.GetMetadataForProperties(input, input.GetType());
            foreach (var modelMetadata in metadata)
            {
                var validators = provider.GetValidators(modelMetadata, new ControllerContext());

                foreach (var validator in validators)
                {
                    var results = validator.Validate(input);
                    foreach (var result in results)
                    {
                        Controller.ModelState.AddModelError(modelMetadata.PropertyName, result.Message);
                    }
                }

                // validation of property value

                var inputType = input.GetType();
                var properties = new List<PropertyInfo>(inputType.GetProperties());
                foreach (var property in properties)
                {
                    var value = property.GetValue(input, null);
                    if (value != null && value.GetType().Assembly == Controller.GetType().Assembly)
                    {
                        ValidateApiModel(value);
                    }
                }
            }
        }
    }
}
