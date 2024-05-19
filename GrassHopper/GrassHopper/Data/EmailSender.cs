using System.Net.Mail;
using System.Net;

namespace GrassHopper.Data
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var mail = "orion123333@gmail.com";
            var pw = "OrionHill123";

            var client = new SmtpClient("orion1233333@gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            return client.SendMailAsync(
                new MailMessage(
                    from: mail,
                    to: email,
                    subject,
                    body
                    ));
                
         }
    }
}
