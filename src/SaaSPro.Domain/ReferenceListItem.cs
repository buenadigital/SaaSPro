using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    /// <summary>
    /// Represents an item in a <see cref="ReferenceList"/>.
    /// </summary>
    public class ReferenceListItem : Entity
    {
        public virtual string Value { get; protected set; }
        public virtual Guid CustomerId { get; set; }
        
        /// <summary>
        /// Creates a new <see cref="ReferenceListItem"/> instance.
        /// </summary>
        public ReferenceListItem(string value)
        {
            Ensure.Argument.NotNullOrEmpty(value, "value");
            Value = value;
        }

        public ReferenceListItem() { }
    }
}
