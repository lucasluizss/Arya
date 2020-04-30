using FluentValidation;
using System.Text.RegularExpressions;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class AddUserCommandValidator : Validator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Must(password => ValidPassword(password))
                .WithMessage("Password must have minimum eight characters, at least one letter, one number and one special character.");
        }

        private static bool ValidPassword(string password) => new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").IsMatch(password);
    }
}
