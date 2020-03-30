using FluentValidation;
using System;

namespace Arya.Application.Domain.Queries.User
{
    public class GetUserByIdQuery : UserBaseQuery
    {
        public GetUserByIdQuery(Guid id) => Id = id;
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
