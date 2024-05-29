using GrassHopper.Data;
using System.Net.Mail;
using System.Net;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string sender, string subject, string message)
    {
        var ownerEmail = "orion123333@gmail.com"; // Specify the website owner's email address

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential("grasshopperquotes@gmail.com", "akmrtwgvfzndxbee")
        };

        var mail = new MailMessage
        {
            From = new MailAddress(sender),
            Subject = subject,
            Body = message
        };

        mail.To.Add(ownerEmail); 

        return client.SendMailAsync(mail);
    }
}