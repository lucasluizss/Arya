using System.Threading.Tasks;

namespace Arya.Infrastructure.CrossCutting.Email
{
    public interface IEmailService
    {
        Task Send(string destinatorEmail, string destinatorName, string subject, string plainTextContent, string htmlContent);
    }
}
