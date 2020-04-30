using System;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class ActivateUserCommand : IRequest
    {
        public ActivateUserCommand(Guid id) => Id = id;

        public Guid Id { get; }
    }
}
