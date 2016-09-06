using System.Data.Entity.ModelConfiguration;
using SaaSPro.Domain;

namespace SaaSPro.Data.Mapping
{
	public class SecurityQuestionMap : EntityTypeConfiguration<SecurityQuestion>
	{
		public SecurityQuestionMap()
		{
			ToTable("UserSecurityQuestions");
			HasKey(q => q.Id);

			Property(q => q.Question);
			Property(q => q.Answer);

			HasRequired(t=>t.User).WithMany(t=>t.SecurityQuestions).HasForeignKey(t=>t.UserId).WillCascadeOnDelete(true);
            HasMany(t => t.ApiSessionTokens).WithRequired(t => t.SecurityQuestion).HasForeignKey(t => t.SecurityQuestionId).WillCascadeOnDelete(false);
        }
	}
}