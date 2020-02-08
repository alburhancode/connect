using System.Configuration;
using Connect.Classes.DataModels;
using Connect.Classes.Verification;

namespace Connect.Classes.Helpers
{
	internal static class EmailHelper
	{
		internal static void SendEmail(string toEmail, string subjectPending, string bodyPending, string blindCopy = "")
		{
			var email = new Email
			{
				From = ConfigurationManager.AppSettings["FromEmail"],
				To = toEmail,
				SmtpServer = ConfigurationManager.AppSettings["SmtpServer"],
				Subject = subjectPending,
				Body = bodyPending,
				Bcc = blindCopy
			};

			EmailSender.SendEmail(email);
		}
	}
}