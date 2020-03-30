using System;

namespace Arya.Domain
{
    public abstract class Entity
    {
        protected Entity() => Id = Guid.NewGuid();

        public virtual Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
