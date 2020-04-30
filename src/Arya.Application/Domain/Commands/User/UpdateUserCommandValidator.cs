using FluentValidation;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class UpdateUserCommandValidator : Validator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();
        }
    }
}
