using System;

namespace Arya.Domain
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }
        
        public TId Id { get; }

        public DateTime CreatedDate { get; }

        public DateTime? UpdatedDate { get; }

        public static bool operator !=(Entity<TId> a, Entity<TId> b) => !(a == b);

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public override bool Equals(object obj) => Equals(obj as Entity<TId>);

        public bool Equals(Entity<TId> other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (GetType().GetHashCode() * 97) ^ Id.GetHashCode();
            }
        }
    }
}
