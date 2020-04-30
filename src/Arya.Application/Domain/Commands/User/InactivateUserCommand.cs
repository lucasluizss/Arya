using System;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class InactivateUserCommand : IRequest
    {
        public InactivateUserCommand(Guid id) => Id = id;

        public Guid Id { get; }
    }
}
