using System;
using Tyrion;

namespace Arya.Application.Domain.Commands
{
    public class UserBaseCommand : IRequest
    {
        public UserBaseCommand() { }

        public UserBaseCommand(Guid id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
