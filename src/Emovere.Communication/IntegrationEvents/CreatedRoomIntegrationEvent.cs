using Emovere.SharedKernel.Events;

namespace Emovere.Communication.IntegrationEvents
{
    public record CreatedRoomIntegrationEvent(Guid RoomId, Guid HostId, string Name, string Details, 
                                              int MaxParticipantsNumber, int Plan, int Status,
                                              int ParticipantsQuantity, decimal? Price,
                                              DateTime StartDate, DateTime? EndDate = null) : IntegrationEvent;
}
