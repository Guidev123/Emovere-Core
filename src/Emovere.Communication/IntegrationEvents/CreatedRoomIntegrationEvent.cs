using Emovere.SharedKernel.Events;

namespace Emovere.Communication.IntegrationEvents
{
    public record CreatedRoomIntegrationEvent : IntegrationEvent
    {
        public Guid RoomId { get; }
        public Guid HostId { get; }
        public string Name { get; }
        public string Details { get; }
        public int MaxParticipantsNumber { get; }
        public int Plan { get; }
        public int Status { get; }
        public int ParticipantsQuantity { get; }
        public decimal? Price { get; }
        public DateTime StartDate { get; }
        public DateTime? EndDate { get; }

        public CreatedRoomIntegrationEvent(Guid roomId, Guid hostId, string name, string details,
                                           int maxParticipantsNumber, int plan, int status,
                                           int participantsQuantity, decimal? price,
                                           DateTime startDate, DateTime? endDate = null)
        {
            AggregateId = roomId;
            CorrelationId = Guid.NewGuid();
            RoomId = roomId;
            HostId = hostId;
            Name = name;
            Details = details;
            MaxParticipantsNumber = maxParticipantsNumber;
            Plan = plan;
            Status = status;
            ParticipantsQuantity = participantsQuantity;
            Price = price;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}