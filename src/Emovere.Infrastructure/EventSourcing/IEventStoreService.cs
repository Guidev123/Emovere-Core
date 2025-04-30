using EventStore.Client;

namespace Emovere.Infrastructure.EventSourcing
{
    public interface IEventStoreService
    {
        EventStoreClient GetStoreClientConnection();
    }
}