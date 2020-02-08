namespace Connect.Classes.DataModels
{
	public class Email
	{
		public string SmtpServer { get; set; }
		public string Subject { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Body { get; set; }
		public string Bcc { get; set; }
	}
}