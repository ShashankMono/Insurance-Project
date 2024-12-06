using Insurance_final_project.Dto;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Insurance_final_project.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration config)
        {
            _configuration = config;
        }
        public void SendUserDetailthroughEmail(string toEmail, string subject, UserDto userDets)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailSetting:Email")
                , _configuration.GetValue<string>("EmailSetting:Password"));

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("EmailSetting:Email"));
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>User Registered</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<h2>Username : {userDets.Username}</h2>");
            mailBody.AppendFormat($"<h2>User password : {userDets.Password} </h2>");
            mailBody.AppendFormat("<p>Please login to portal and change you username and password</p>");
            mailBody.AppendFormat("<p>Thank you For Registering account</p>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}
