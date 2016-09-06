using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
	public class Customer : AuditedEntity
	{
		public string Hostname { get; set; }
		public string FullName { get; set; }
		public string Company { get; set; }
		public bool Enabled { get; set; }
		public string EncryptionKey { get; set; }
		public string PaymentCustomerId { get; set; }
		public DateTime? PlanCreatedOn { get; set; }
		public DateTime? PlanUpdatedOn { get; set; }
		public DateTime? PlanCanceledOn { get; set; }

		public virtual ICollection<User> Users { get; set; }

		public virtual ICollection<Role> Roles { get; set; }

		public virtual ICollection<ApiToken> ApiTokens { get; set; }

		public virtual ICollection<ReferenceListItem> ReferenceListItems { get; set; }

		public virtual ICollection<AuditEntry> AuditEntries { get; set; }

		public virtual ICollection<IPSEntry> IPSEntries { get; set; }

		public virtual ICollection<Note> Notes { get; set; }

		public virtual ICollection<Project> Projects { get; set; }

		public virtual Plan Plan { get; protected set; }

		public virtual User AdminUser { get; set; }

		public Customer(string name, string hostname, string company = null, string paymentCustomerId = null,
			bool enabled = true)
		{
			Update(name, hostname, company);
			if (!string.IsNullOrEmpty(paymentCustomerId))
			{
				UpdateStripe(paymentCustomerId);
			}

			if (enabled)
			{
				Enable();
			}

			EncryptionKey = GenerateKey();
		}

		protected Customer()
		{
		}

		public void Update(string name, string hostname, string company)
		{
			FullName = name;
			Hostname = hostname.ToLower();
			Company = company;
		}

		public void UpdateStripe(string paymentCustomerId)
		{
			PaymentCustomerId = paymentCustomerId;
		}

		public void UpdatePlan(Plan plan)
		{
			Ensure.Argument.NotNull(plan, "plan");
			Plan = plan;
			PlanCreatedOn = DateTime.Now;
		}

		public void RemovePlan()
		{
			Plan = null;
			PlanCanceledOn = DateTime.Now;
		}

		public void UpdateAdminUser(User adminUser)
		{
			Ensure.Argument.NotNull(adminUser, "adminUser");
			AdminUser = adminUser;
		}

		public void Enable()
		{
			if (!Enabled)
			{
				Enabled = true;
			}
		}

		public void Disable()
		{
			if (Enabled)
			{
				Enabled = false;
			}
		}

		/// <summary>
		/// Generates a random 256 bit key to be used for encrypting
		/// Customer data.
		/// </summary>
		/// <returns>The base64 encoded key</returns>
		private static string GenerateKey()
		{
			using (var alg = RijndaelManaged.Create())
			{
				return Convert.ToBase64String(alg.Key);
			}
		}

		public string GetFullHostName()
		{
			return $"https://{Hostname}.saaspro.net";
		}
	}
}