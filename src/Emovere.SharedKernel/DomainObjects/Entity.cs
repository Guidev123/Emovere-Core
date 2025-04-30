using Emovere.SharedKernel.Events;

namespace Emovere.SharedKernel.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private readonly List<Event> _events = [];

        public Guid Id { get; protected set; }
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
        protected abstract void Validate();    

        public void AddEvent(Event @event)
        {
            ArgumentNullException.ThrowIfNull(@event);
            _events.Add(@event);
        }

        public void ClearEvents()
            => _events.Clear();

        public void RemoveEvent(Event @event)
        {
            ArgumentNullException.ThrowIfNull(@event);
            _events.Remove(@event);
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (!(a is not null && b is not null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id = {Id}]";
    }
}
