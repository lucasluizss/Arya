using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class AuthenticateUserCommand : IRequest
    {
        public AuthenticateUserCommand() { }

        public AuthenticateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
