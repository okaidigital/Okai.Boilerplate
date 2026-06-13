namespace Okai.Boilerplate.Domain.Entities.Abstract
{
    public abstract class Entity
    {
        public Guid GlobalId { get; set; }
        public int Id { get; set; }

        private readonly List<Event> _events = new();
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        protected Entity()
        {
            GlobalId = Guid.NewGuid();
        }

        protected Entity(List<Event> events,
            Guid globalId,
            int id)
        {
            _events = events ?? throw new ArgumentNullException(nameof(events));
            GlobalId = globalId;
            Id = id;
        }

        public void AddEvent(Event @event)
        {
            _events.Add(@event);
        }

        public void RemoveEvent(Event @event)
        {
            _events.Remove(@event);
        }

        public void CleanEvents()
        {
            _events.Clear();
        }

        public override bool Equals(object? obj)
        {
            var entity = obj as Entity;

            if (ReferenceEquals(this, entity)) return true;
            return entity is not null && GlobalId.Equals(entity.GlobalId);
        }

        public static bool operator ==(Entity? first, Entity? second)
        {
            if (first is null && second is null) return true;
            if (first is null || second is null) return false;

            return first.Equals(second);
        }

        public static bool operator !=(Entity? first, Entity? second)
        {
            return !(first == second);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + GlobalId.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [GlobalId = {GlobalId}]";
        }
    }
}
