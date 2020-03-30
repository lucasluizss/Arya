using FluentValidation;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class RemoveUserCommandValidator : Validator<RemoveUserCommand>
    {
        public RemoveUserCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
