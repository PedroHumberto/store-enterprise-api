using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.APP.Messages;

namespace Core.APP.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _events;

        //Só posso ler essa coleção, não pode adicionar, é uma lista de somente de leitura
        public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();

        public void AddEvent(Event myEvent)
        {
            //se a notificação não existe, é criada uma lista nova e adiciona o evento.
            _events = _events ?? new List<Event>();
            _events.Add(myEvent);
        }
        public void RemoveEvent(Event eventItem)
        {
            _events?.Remove(eventItem);
        }
        public void CleanEvents()
        {
            _events?.Clear(); 
        }

        #region (Equals)
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
        #endregion
    }


}
