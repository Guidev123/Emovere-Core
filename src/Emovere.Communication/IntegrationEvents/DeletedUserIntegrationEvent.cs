using Emovere.SharedKernel.Events;

namespace Emovere.Communication.IntegrationEvents
{
    public record DeletedUserIntegrationEvent(Guid UserId) : IntegrationEvent;
}