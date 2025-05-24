using Emovere.SharedKernel.Events;

namespace Emovere.Communication.IntegrationEvents
{
    public record ParticipantAddedIntegrationEvent : IntegrationEvent
    {
        public Guid RoomId { get; }
        public Guid CustomerId { get; }
        public string Email { get; }

        public ParticipantAddedIntegrationEvent(Guid roomId, Guid customerId, string email)
        {
            AggregateId = roomId;
            CorrelationId = Guid.NewGuid();
            RoomId = roomId;
            CustomerId = customerId;
            Email = email;
        }
    }
}