using Arya.Infrastructure.Core.Domain.ValueObjects;
using Arya.Infrastructure.CrossCutting.Enums;
using System;

namespace Arya.Domain.Entities
{
    public class UserEntity : Entity<Guid>
    {
        protected UserEntity() { }

        public UserEntity(Guid id) : base(id) { }

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

        public UserEntity(Guid id, string name, Email email, string password) : base(id)
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

        public bool IsInactive() => Status == Status.Inactive;

        public void Activate() => Status = Status.Active;

        public void Inactivate() => Status = Status.Inactive;

        public void ChangeEmail(string email) => Email = new Email(email);
    }
}
