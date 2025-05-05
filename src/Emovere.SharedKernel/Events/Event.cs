using MidR.Interfaces;

namespace Emovere.SharedKernel.Events
{
    public abstract record Event : Message, INotification
    {
        protected Event()
        {
            Timestamp = DateTime.UtcNow;
            EventId = Guid.NewGuid();
        }

        public DateTime Timestamp { get; }
        public Guid EventId { get; }
        public Guid CorrelationId { get; protected set; }
    }
}