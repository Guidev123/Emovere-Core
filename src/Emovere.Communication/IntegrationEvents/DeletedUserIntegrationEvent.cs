using Emovere.SharedKernel.Events;

namespace Emovere.Communication.IntegrationEvents
{
    public record DeletedUserIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public DeletedUserIntegrationEvent(Guid userId)
        {
            UserId = userId;
            AggregateId = userId;
            CorrelationId = Guid.NewGuid();
        }
    }
}