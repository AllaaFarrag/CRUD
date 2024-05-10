using Demo.DAL.Entites;
using Demo.PL.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;

namespace Demo.PL.Utility
{
    public class MailSetting : IMailSettings
    {

        //public static void SendMail(Email email)
        //{
        //    //Mail Service Gmail
        //    //Client => Send Email
        //    var client = new SmtpClient("smtp.gmail.com", 587);
        //    client.EnableSsl = true;
        //    client.Credentials = new NetworkCredential("alaaabofarrag00@gmail.com", "pzetycwqzzfzvlkf");

        //    //2 Step Verification
        //    //App Password

        //    //Send Email
        //    client.Send("alaaabofarrag00@gmail.com", email.Recipient, email.Subject, email.Body);
        //}
        private readonly EmailSettings _options;

        public MailSetting(IOptions<EmailSettings> options)
        {
            _options = options.Value;
        }
        public void SendMail(Email email)
        {
            //zadzqjnihqfqdszl

            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject  = email.Subject,
            };

            mail.To.Add(MailboxAddress.Parse(email.Recipient));
            mail.From.Add(new MailboxAddress(_options.DisplayName , _options.Email));

            var builder = new BodyBuilder();    
            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_options.Host, _options.Port , SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email , _options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);


            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //    client.Authenticate("yourEmail@gmail.com", "yourGeneratedPassword");
            //    client.Send(mail);
            //    client.Disconnect(true);
            //}
        }
    }
}
