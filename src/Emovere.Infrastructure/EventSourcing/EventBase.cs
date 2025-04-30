namespace Emovere.Infrastructure.EventSourcing
{
    public record EventBase
    {
        public DateTime Timestamp { get; set; }
    }
}