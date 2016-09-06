namespace SaaSPro.Domain
{
	public class EmailTemplate : AuditedEntity
	{
		public string TemplateName { get; set; }
		public string FromEmail { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string To { get; set; }
		public string Template { get; set; }

		public EmailTemplate()
		{

		}

		public EmailTemplate(string templateName, string body, string fromEmail, string subject)
		{
			TemplateName = templateName;
			FromEmail = fromEmail;
			Subject = subject;
			Body = body;
		}

		public void Update(string templateName, string body, string fromEmail, string subject)
		{
			Body = body;
			FromEmail = fromEmail;
			Subject = subject;
			UpdatedOn = System.DateTime.Now;
		}
	}
}