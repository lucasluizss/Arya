using FluentValidation;

namespace Arya.Application.Domain.Queries.User
{
    public class GetUserByEmailQuery : UserBaseQuery 
    {
        public GetUserByEmailQuery(string email) => Email = email;
    }

    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required.").EmailAddress();
        }
    }
}
