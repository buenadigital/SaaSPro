using System;
using System.Collections.Generic;
using SaaSPro.Common.Helpers;
using SaaSPro.Common;

namespace SaaSPro.Domain
{
    public class SecurityQuestion : Entity
    {
        public string Question { get; set; }
        public string Answer { get; set; }

		public Guid UserId { get; set; }
		public virtual User User { get; set; }

        public virtual ICollection<ApiSessionToken> ApiSessionTokens { get; protected set; } = new HashSet<ApiSessionToken>();

        internal SecurityQuestion(string question, string answer)
        {
            Update(question, answer);
        }

        public bool ValidateAnswer(string answer)
        {
            Ensure.Argument.NotNullOrEmpty(answer);
            return CryptoHelper.VerifyHashedPassword(Answer, answer);
        }

        protected internal void Update(string question, string answer)
        {
            Ensure.Argument.NotNullOrEmpty(question, "question");
            Ensure.Argument.NotNullOrEmpty(answer, "answer");

            Question = question;
            Answer = CryptoHelper.HashPassword(answer);
        }

		public SecurityQuestion()
	    {
		    
	    }

        public override string ToString()
        {
            return Question;
        }
    }
}
