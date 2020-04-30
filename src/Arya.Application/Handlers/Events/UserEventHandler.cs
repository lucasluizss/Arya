using Arya.Application.Domain.Events;
using Arya.Infrastructure.CrossCutting.Email;
using Arya.Infrastructure.CrossCutting.Resources;
using System.Threading.Tasks;
using Tyrion;

namespace Arya.Application.Handlers.Events
{
    public sealed class UserEventHandler : INotificationHandler<SendEmailNewUserEvent>
    {
        private readonly IEmailService _emailService;

        public UserEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Publish(SendEmailNewUserEvent request)
        {
            var subject = $"Welcome {request.Name}";

            var htmlContent = Resources.TemplateEmailNewUser
                                .Replace("{UserName}", request.Name)
                                .Replace("{UserId}", request.Id.ToString());

            await _emailService.Send(request.Email, request.Name, subject, string.Empty, htmlContent);
        }
    }
}
