using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
    {
        public TId Id { get; set; }
        public bool IsDeleted { get; private set; } = false;
        public DateTime DeletedTimeUtc { get; private set; }
        [ConcurrencyCheck]
        public DateTime LastUpdateUtc { get; private set; }

        protected Entity(TId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }


        protected Entity() { }

        public void IsDeletedChange()
        {
            IsDeleted = true;
            DeleteTime();
        }

        public void DeleteTime()
        {
            DeletedTimeUtc = DateTime.UtcNow;
        }

        public void LastUpdate()
        {
            LastUpdateUtc = DateTime.UtcNow;
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public override bool Equals(Object? obj)
        {
            if(obj is not Entity<TId> other)
            {
                return false;
            }

            if(ReferenceEquals(this, other))
            {
                return true;
            }

            if(GetType() != other.GetType())
            {
                return false;
            }

            if(Id.Equals(default) || other.Id.Equals(default)){
                return false;
            }

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
        {
            return !(left == right);
        }
    }
}
