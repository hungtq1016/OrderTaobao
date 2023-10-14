
using BaseSource.Dto;
using MailKit.Net.Smtp;
using MimeKit;


namespace BaseSource.BackendAPI.Services
{
    public interface IMailService
    {
        void SendMail(MailRequest request);
    }

    public class MailService : IMailService
    {
        public void SendMail(MailRequest request)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Sender Name", "hungbanghung@gmail.com"));
                email.To.Add(new MailboxAddress("Receiver Name", "hungtq1016@gmail.com"));

                email.Subject = "Testing out email sending";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = "Hello all the way from the land of C#"
                };
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("hungbanghung@gmail.com", "blhm pkoy hdzb dihm");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                //Error
                Console.WriteLine(ex.Message);
            }
        }
    }
}
