using Emovere.SharedKernel.Events;

namespace Emovere.SharedKernel.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveAsync<TEvent>(TEvent @event) where TEvent : Event;

        Task<IEnumerable<StoredEvent>> GetAllAsync(Guid aggregateId);
    }
}