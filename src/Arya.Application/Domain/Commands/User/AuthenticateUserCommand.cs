using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class AuthenticateUserCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
