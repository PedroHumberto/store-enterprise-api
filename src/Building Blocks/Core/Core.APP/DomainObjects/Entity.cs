using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.APP.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }
        //Operators to compare Entity
        public static bool operator == (Entity a, Entity b)
        {
            //Validation if you can compare this entity
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            //call equals internaly
            return a.Equals(b);
        }
        //if they're equals return true
        public static bool operator != (Entity a, Entity b) 
        { 
            return !(a == b); 
        }
        //------------------------------------------

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 987) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }


}
