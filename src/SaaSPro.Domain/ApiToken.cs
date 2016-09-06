using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    /// <summary>
    /// Represents an Api Token
    /// </summary>
    public class ApiToken : AuditedEntity
    {
        /// <summary>
        /// Gets the name/reference for the token.
        /// </summary>
        public virtual string Name { get;  set; }
        
        /// <summary>
        /// Gets the token value.
        /// </summary>
        public virtual string Token { get; protected set; }

        public virtual Guid CustomerId { get; protected set; }

        /// <summary>
        /// Creates a new <see cref="ApiToken"/> instance.
        /// </summary>
        /// <param name="name">The name of the Api Token.</param>
        /// <param name="customer">Customer</param>
        public ApiToken(string name, Customer customer)
        {
            Ensure.Argument.NotNull(customer, nameof(customer));

            Update(name);
            Token = GenerateToken();
            CustomerId = customer.Id;
        }

        /// <summary>
        /// Updates the ApiToken details.
        /// </summary>
        /// <param name="name">The name of the api token.</param>
        public virtual void Update(string name)
        {
            Ensure.Argument.NotNullOrEmpty(name, nameof(name));
            Name = name;
        }

        private static string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        protected ApiToken() { }
    }
}