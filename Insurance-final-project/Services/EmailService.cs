using Insurance_final_project.Dto;
using System.Net.Mail;
using System.Net;
using System.Text;
using Insurance_final_project.Repositories;
using Insurance_final_project.Models;
using Insurance_final_project.Exceptions;
using CloudinaryDotNet.Actions;


namespace Insurance_final_project.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Agent> _agentRepo;
        public EmailService(IConfiguration config,
            IRepository<Customer> customerRepo,
            IRepository<Agent> agentRepo
            )
        {
            _configuration = config;
            _customerRepo = customerRepo;
            _agentRepo = agentRepo;
        }

        public SmtpClient CreateClient()
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailSetting:Email")
                , _configuration.GetValue<string>("EmailSetting:Password"));
            return client;
        }
        public void SendUserDetailthroughEmail(string toEmail, string subject, UserDto userDets)
        {
            var client = CreateClient();
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

            client.Send(mailMessage);
        }

        public void SendMarketingMail(MarketingDto info)
        {
            var customer = _customerRepo.GetAll().FirstOrDefault(c => c.CustomerId == info.CustomerId);
            if (customer == null)
            {
                throw new InvalidGuidException("Customer not found");
            }

            var client = CreateClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("EmailSetting:Email"));
            mailMessage.To.Add(customer.EmailId);
            mailMessage.Subject = $"Insurance marketing mail by {info.AgentName}";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<h1>Get best policy {info.PolicyName}</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<h2>This policy is refered by agent {info.AgentName}</h2>");
            mailBody.AppendFormat($"<p><a href=\"{info.Url}\">Click here</a><p>");
            mailBody.AppendFormat("<p>Buy the policy using this simple link</p>");
            mailBody.AppendFormat("<p>Thank you</p>"); 
            mailMessage.Body = mailBody.ToString();

            client.Send(mailMessage);

        }
        public void RejectionMail(Guid customerId,string reason,string subject)
        {
            var customer = _customerRepo.GetAll().FirstOrDefault(c=>c.CustomerId==customerId);
            if (customer == null)
            {
                throw new InvalidGuidException("Customer not found");
            }

            var client = CreateClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("EmailSetting:Email"));
            mailMessage.To.Add(customer.EmailId);
            mailMessage.Subject = $"{subject}";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<h1>{reason}</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Thank you</p>");
            mailMessage.Body = mailBody.ToString();

            client.Send(mailMessage);
        }

        public void CommissionWithdrawalMail(Guid agentId, double amount)
        { 
            var agent = _agentRepo.Get(agentId);
            if (agent == null)
            {
                throw new InvalidGuidException("Agent not found");
            }

            var client = CreateClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("EmailSetting:Email"));
            mailMessage.To.Add(agent.Email);
            mailMessage.Subject = $"Commission Withdrawal successfull";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<h1>Comission amount {amount} is been withdrwal and transfered to you account</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Thank you</p>");
            mailMessage.Body = mailBody.ToString();

            client.Send(mailMessage);
        }

    }
}
