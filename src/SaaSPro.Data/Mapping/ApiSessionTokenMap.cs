using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class ApiSessionTokenMap : AuditedClassMap<ApiSessionToken>
	{
		public ApiSessionTokenMap()
		{
			ToTable("ApiSessionTokens");
			HasKey(c => c.Id);

			Property(c => c.Token).IsRequired();
			Property(c => c.QuestionAnswered).IsRequired();
            Property(c => c.ExpirationDate).IsRequired();
            Property(c => c.UserId).IsRequired();
		    Property(c => c.SecurityQuestionId);

            HasRequired(t => t.User).WithMany(t => t.ApiSessionTokens).HasForeignKey(t => t.UserId).WillCascadeOnDelete(true);
            HasRequired(t => t.SecurityQuestion).WithMany(t => t.ApiSessionTokens).HasForeignKey(t => t.SecurityQuestionId).WillCascadeOnDelete(false);
        }
	}
}