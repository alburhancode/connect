using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Connect.Classes.DataModels;

namespace Connect.Classes.Verification
{
	public static class EmailSender
	{
		public static void SendEmail(Email emailDetails)
		{
            var username = "Admin@AlburhanConnect.com";
			var password = "Htuh!497";
			var port = 25;

			string to = emailDetails.To;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["OverrideEmail"]))
            {
                to = ConfigurationManager.AppSettings["OverrideEmail"];
            }

            string from = emailDetails.From;
			MailMessage message = new MailMessage(from, to);

			if (!string.IsNullOrEmpty(emailDetails.Bcc))
				message.Bcc.Add(emailDetails.Bcc);

			message.Subject = emailDetails.Subject;// "You are ready for Step 2 (Your Details)";
			message.Body = emailDetails.Body;
			SmtpClient client = new SmtpClient(emailDetails.SmtpServer);
			// Credentials are necessary if the server requires the client 
			// to authenticate before it will send e-mail on the client's behalf.
//			client.UseDefaultCredentials = true;

			client.Credentials = new NetworkCredential(username, password);
			client.Port = port;

			try
			{
				if(bool.Parse(ConfigurationManager.AppSettings["SendEmails"]))
                {
                    client.Send(message);
                }
            }
			catch (Exception ex)
			{
				// log the exception using log4net
				//Console.WriteLine("There was a problem with sending the email to {0}: ", 
				//	ex.ToString());
			}
		}
	}
}