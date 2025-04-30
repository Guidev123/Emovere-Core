using MidR.Interfaces;

namespace Emovere.SharedKernel.Events
{
    public abstract record Event : INotification
    {
        protected Event()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.UtcNow;
        }

        public Guid AggregateId { get; protected set; }
        public DateTime Timestamp { get; }
        public string MessageType { get; }
    }
}
