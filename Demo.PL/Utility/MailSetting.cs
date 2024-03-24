using Demo.DAL.Entites;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Utility
{
	public static class MailSetting
	{
		public static void SendMail (Email email)
		{
			//Mail Service Gmail
			//Client => Send Email
			var client = new SmtpClient("smtp.gmail.com" , 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("alaaabofarrag00@gmail.com", "pzetycwqzzfzvlkf");

			//2 Step Verification
			//App Password

			//Send Email
			client.Send("alaaabofarrag00@gmail.com" , email.Recipient , email.Subject , email.Body);
		}
	}
}
