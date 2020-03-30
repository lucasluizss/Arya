using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Arya.Infrastructure.CrossCutting.Email
{
    public sealed class EmailService : IEmailService
    {
        private readonly string SendGridKey;

        public EmailService(string sendGridKey) => SendGridKey = sendGridKey;

        public async Task Send(string destinatorEmail, string destinatorName, string subject, string plainTextContent, string htmlContent)
        {
            var client = new SendGridClient(SendGridKey);
            
            var from = new EmailAddress("suport@arya.com", "Arya");
            
            var to = new EmailAddress(destinatorEmail, destinatorName);
                        
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            await client.SendEmailAsync(msg);
        }
    }
}
