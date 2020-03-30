using System;
using Tyrion;

namespace Arya.Application.Domain.Commands.User
{
    public class RemoveUserCommand : IRequest
    {
        public RemoveUserCommand(Guid id) => Id = id;

        public Guid Id { get; set; }
    }
}
