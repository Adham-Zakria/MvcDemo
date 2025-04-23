using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Helper
{
	public static class EmailSettings
	{
		public static bool SendEmail(Email email)
		{
			// Mail server : Gmail
			// Protocol : Simple Mail Transfer Protocol (SMTP)
			// Host : smtp.gmail.com
			// Port : 587
			try
			{
				var client = new SmtpClient("smtp.gmail.com" , 587);
				client.EnableSsl = true;

				// Sender Data
				client.Credentials = new NetworkCredential("adhamzakria2003@gmail.com", "mfcupkngumpxlneu");
				client.Send(from: "adhamzakria2003@gmail.com" , 
					        recipients: email.To , 
							subject: email.Subject , 
							body: email.Body );
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
