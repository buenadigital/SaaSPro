using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaaSPro.Domain
{
    public class User : AuditedEntity
    {
		private Random random = new Random();
        private const int PasswordExpiryDays = 90;

		public string UserTypeString
		{
			get { return UserType.ToString(); }
			set { UserType = EntityExtensions.ParseEnum<UserType>(value); }
		}
		public UserType UserType { get; protected set; } = UserType.RegularUser;

        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public DateTime PasswordExpiryDate { get;  set; }
        public DateTime RegisteredDate { get;  set; }
        public bool Enabled { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        public Guid CustomerId { get; set; }

		public virtual ICollection<Customer> Customers { get; set; }
		public virtual ICollection<RoleUsers> RoleUsers { get; set; } = new HashSet<RoleUsers>();
		public virtual ICollection<SecurityQuestion> SecurityQuestions { get; protected set; } = new HashSet<SecurityQuestion>();
        public virtual ICollection<ApiSessionToken> ApiSessionTokens { get; protected set; } = new HashSet<ApiSessionToken>();

        public virtual IEnumerable<Role> Roles
	    {
		    get { return RoleUsers.Select(t => t.Role).ToList(); }
	    }

	    public User(Customer customer, string email, string firstName, string lastName, string password, bool enabled = true) : this()
        {
            Ensure.Argument.NotNull(customer, "Customer");
            Ensure.Argument.NotNullOrEmpty(email, "email");
            Ensure.Argument.NotNullOrEmpty(firstName, "firstName");
            Ensure.Argument.NotNullOrEmpty(lastName, "lastName");
            Ensure.Argument.NotNullOrEmpty(password, "password");

            //Customer = customer;
			CustomerId = customer.Id;
			UserType = UserType.SystemUser;
            UpdateProfile(email, firstName, lastName);
            Enabled = enabled;
            RegisteredDate = DateTime.UtcNow;
            SetPassword(password); //TODO Need to remove
        }

	    public User()
	    {
	    }

		public string FullName => string.Concat(FirstName, " ", LastName);

        public bool HasExpiredPassword => PasswordExpiryDate <= DateTime.UtcNow;

        public void UpdateProfile(string email, string firstName, string lastName)
        {
            Ensure.Argument.NotNullOrEmpty(email, "email");
            Ensure.Argument.NotNullOrEmpty(firstName, "firstName");
            Ensure.Argument.NotNullOrEmpty(lastName, "lastName");

            Email = email.ToLower();
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetPassword(string newPassword, bool expireImmediately = false)
        {
            Ensure.Argument.NotNullOrEmpty(newPassword, "newPassword");
            Password = CryptoHelper.HashPassword(newPassword);
            
            PasswordExpiryDate = expireImmediately ? DateTime.UtcNow : DateTime.UtcNow.AddDays(PasswordExpiryDays);
        }

        public void AuditLogin(string ipAddress)
        {
            Ensure.Argument.NotNullOrEmpty(ipAddress, "ipAddress");
            LastLoginIP = ipAddress;
            LastLoginDate = DateTime.UtcNow;
        }

        public bool ValidatePassword(string password)
        {
            Ensure.Argument.NotNullOrEmpty(password, "password");
            return CryptoHelper.VerifyHashedPassword(Password, password);
        }

        public string GenerateResetToken(TimeSpan expiryTimeSpan)
        {
            Ensure.Argument.NotNull(expiryTimeSpan, "expiryTimeSpan");
            PasswordResetTokenExpiry = DateTime.UtcNow.Add(expiryTimeSpan);
            PasswordResetToken = Guid.NewGuid().ToString("N");
            return PasswordResetToken;
        }

        public bool ResetPassword(string resetToken, string newPassword)
        {
            Ensure.Argument.NotNullOrEmpty(resetToken, "resetToken");
            Ensure.Argument.NotNullOrEmpty(newPassword, "newPassword");

            if (resetToken.Equals(PasswordResetToken) && DateTime.UtcNow <= PasswordResetTokenExpiry)
            {
                SetPassword(newPassword);
                // expire token
                PasswordResetTokenExpiry = DateTime.UtcNow;
                return true;
            }

            return false;
        }

        public void AddSecurityQuestion(string question, string answer)
        {
            Ensure.Argument.NotNullOrEmpty(question, "question");
            Ensure.Argument.NotNullOrEmpty(answer, "answer");

            if (IsDuplicateQuestion(question))
            {
                throw new ArgumentException("The question '{0}' already exists.".FormatWith(question));
            }

            SecurityQuestions.Add(new SecurityQuestion(question, answer));
        }

		public void UpdateQuestion(Guid questionId, string question, string answer)
        {
            Ensure.Argument.NotNull(questionId, "questionId");
            Ensure.Argument.NotNullOrEmpty(question, "question");
            Ensure.Argument.NotNullOrEmpty(answer, "answer");

            var securityQuestion = SecurityQuestions.FirstOrDefault(q => q.Id == questionId);
            
            if (securityQuestion == null)
            {
                throw new ArgumentException("A security question with Id '{0}' does not exist.");
            }

            //if (IsDuplicateQuestion(question, securityQuestion.Id))
            //{
            //    throw new ArgumentException("The question '{0}' already exists.".FormatWith(question));
            //}

            securityQuestion.Update(question, answer);
        }

        public SecurityQuestion GetSecurityQuestion(Guid questionId)
        {
            Ensure.Argument.NotNull(questionId, "questionId");
            return SecurityQuestions.FirstOrDefault(q => q.Id == questionId);
        }

        public SecurityQuestion GetRandomSecurityQuestion()
        {
            if (!SecurityQuestions.Any())
            {
                throw new InvalidOperationException("No security questions exist.");
            }

            return SecurityQuestions.ElementAt(random.Next(SecurityQuestions.Count));
        }

        public void RemoveFromAllRoles()
        {
            RoleUsers.Clear();
        }

        private bool IsDuplicateQuestion(string question, Guid? questionId = null)
        {
            var existing = SecurityQuestions.FirstOrDefault(q => 
                q.Question.Equals(question, StringComparison.InvariantCultureIgnoreCase) && (questionId == null || q.Id != questionId));
            
            return existing != null;
        }
    }
}
