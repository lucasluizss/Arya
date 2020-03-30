using Arya.Infrastructure.Core.Domain.ValueObjects;
using Arya.Infrastructure.CrossCutting.Enums;

namespace Arya.Domain.Entitties
{
    public class UserEntity : Entity
    {
        protected UserEntity() { }

        public UserEntity(string name, Email email)
        {
            Name = name;
            Email = email;
        }

        public UserEntity(string name, Email email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; private set; }

        public Email Email { get; private set; }

        public string Password { get; private set; }

        public Status Status { get; private set; }

        public bool IsActive() => Status == Status.Active;

        public void Activate() => Status = Status.Active;

        public void Inactivate() => Status = Status.Inactive;
    }
}
