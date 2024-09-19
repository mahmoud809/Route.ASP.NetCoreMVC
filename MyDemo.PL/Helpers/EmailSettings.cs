using MyDemo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MyDemo.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client  = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("elshokhaby1999@gmail.com", "oosz kxll njpl trfn");
			client.Send("elshokhaby1999@gmail.com", email.To, email.Subject, email.Body);

        }
	}
}
