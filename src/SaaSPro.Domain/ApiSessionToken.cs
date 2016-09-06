using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    /// <summary>
    /// Represents an Api Token
    /// </summary>
    public class ApiSessionToken : AuditedEntity
    {
        /// <summary>
        /// Gets the token value.
        /// </summary>
        public virtual string Token { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        public virtual Guid SecurityQuestionId { get; protected set; }

        public virtual bool QuestionAnswered { get; protected set; }

        public virtual DateTime ExpirationDate { get; protected set; }

        public virtual User User { get; protected set; }

        public virtual SecurityQuestion SecurityQuestion { get; protected set; }

        /// <summary>
        /// Creates a new <see cref="ApiSessionToken"/> instance.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="timeout">Session timeout minutes</param>
        public ApiSessionToken(User user, int timeout)
        {
            Ensure.Argument.NotNull(user, nameof(user));
            
            Token = GenerateToken();
            UserId = user.Id;
            QuestionAnswered = false;
            UpdateExpirationDate(timeout);

            var question = user.GetRandomSecurityQuestion();
            UpdateSecurityQuestion(question);
        }

        private static string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public void UpdateExpirationDate(int timeout)
        {
            ExpirationDate = DateTime.Now.AddMinutes(timeout);
        }

        public void UpdateQuestionAnswered()
        {
            QuestionAnswered = true;
        }

        public void UpdateSecurityQuestion(SecurityQuestion question)
        {
            SecurityQuestionId = question.Id;
        }

        protected ApiSessionToken() { }
    }
}