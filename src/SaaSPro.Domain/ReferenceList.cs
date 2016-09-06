using System;
using System.Collections.Generic;
using System.Linq;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
	/// <summary>
	/// Represents a reference list.
	/// </summary>
	public class ReferenceList : Entity
	{
		/// <summary>
		/// Gets the system name of the ReferenceList.
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// Gets items in the ReferenceList.
		/// </summary>
		public virtual ICollection<ReferenceListItem> Items { get; set; } = new List<ReferenceListItem>();

		/// <summary>
		/// Creates a new <see cref="ReferenceList"/> instance.
		/// </summary>
		public ReferenceList(string systemName)
		{
			Ensure.Argument.NotNullOrEmpty(systemName, "systemName");
			SystemName = systemName;
		}

		/// <summary>
		/// Adds a new item to the ReferenceList.
		/// </summary>
		/// <param name="customer">Customer</param>
		/// <param name="value">The value of the item to add.</param>
		public void AddItem(Customer customer, string value)
		{
			var item = new ReferenceListItem(value);
			item.CustomerId = customer.Id;
			Items.Add(item);
		}

		/// <summary>
		/// Removes an item from the ReferenceList.
		/// </summary>
		/// <param name="itemId">The id of the item to remove.</param>
		public bool RemoveItem(Guid itemId)
		{
			var item = Items.SingleOrDefault(i => i.Id == itemId);

			if (item != null)
			{
				Items.Remove(item);
				return true;
			}

			return false;
		}

		protected ReferenceList()
		{

		}
	}
}
