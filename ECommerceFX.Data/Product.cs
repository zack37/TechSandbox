using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ECommerceFX.Data
{
    [DebuggerDisplay("Id: {Id} Name: {Name}")]
    public class Product : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        protected bool Equals(Product other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}