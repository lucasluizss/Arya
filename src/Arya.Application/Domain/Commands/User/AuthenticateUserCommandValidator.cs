using FluentValidation;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class AuthenticateUserCommandValidator : Validator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {            
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
