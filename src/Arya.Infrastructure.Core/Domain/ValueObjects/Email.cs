using Arya.Infrastructure.Core.Domain.Models;
using System.Collections.Generic;

namespace Arya.Infrastructure.Core.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Address { get; private set; }

        protected Email() { }

        public Email(string email) => Address = email;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
        }
    }
}
