using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace eparafia.Helpers;


public class EmailManager : IEmailManager
{
    public async Task SendEmail(string email)
    {
        using MailMessage msg = new();
        msg.From = new MailAddress("rzejan@gmail.com");
        msg.To.Add(email);
        msg.Subject = "Eparafia Test";
        msg.Body = "Test";
        msg.Priority = MailPriority.High;
        msg.IsBodyHtml = true;

        using SmtpClient client = new ();
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("eparafiadev@gmail.com", "g8vYZOna8j231H7"); 
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network; 
                
        await client.SendMailAsync(msg);
    }
}