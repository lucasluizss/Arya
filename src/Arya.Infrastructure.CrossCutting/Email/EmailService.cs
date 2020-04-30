using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Arya.Infrastructure.CrossCutting.Email
{
    public sealed class EmailService : IEmailService
    {
        private readonly IConfiguration Configuration;

        public EmailService(IConfiguration configuration) => Configuration = configuration;

        public async Task Send(string destinatorEmail, string destinatorName, string subject, string plainTextContent, string htmlContent)
        {
            var client = new SendGridClient(Configuration.GetSection("SendGrid:Key").Value);

            var senderName = Configuration.GetSection("SendGrid:SenderName").Value;
            var senderMail = Configuration.GetSection("SendGrid:SenderMail").Value;

            var from = new EmailAddress(senderMail, senderName);
            var to = new EmailAddress(destinatorEmail, destinatorName);

            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(message);
        }
    }
}
