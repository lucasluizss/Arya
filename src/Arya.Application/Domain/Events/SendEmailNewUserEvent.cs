using System;
using Tyrion;

namespace Arya.Application.Domain.Events
{
    public class SendEmailNewUserEvent : INotification
    {
        public SendEmailNewUserEvent(Guid id, string name, string email)
        {
            Id = id.ToString();
            Name = name;
            Email = email;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
