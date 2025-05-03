using MidR.Interfaces;

namespace Emovere.SharedKernel.Events
{
    public abstract record Event : Message, INotification
    {
        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; }
    }
}