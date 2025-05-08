using Emovere.SharedKernel.Events;
using Emovere.SharedKernel.EventSourcing;
using Emovere.SharedKernel.Responses;
using MidR.Interfaces;

namespace Emovere.SharedKernel.Abstractions.Mediator
{
    public sealed class MediatorHandler(IMediator mediator,
                                        IEventSourcingRepository eventSourcingRepository)
                                      : IMediatorHandler
    {
        public async Task PublishEventAsync<T>(T @event) where T : Event
        {
            await mediator.NotifyAsync(@event);

            if (!@event.GetType().BaseType!.Name.Equals(nameof(DomainEvent)))
                await eventSourcingRepository.SaveAsync(@event);
        }

        public async Task<Response<T>> SendCommand<T>(Command<T> command) => await mediator.DispatchAsync(command);

        public async Task<Response<T>> SendQuery<T>(IQuery<T> query) => await mediator.DispatchAsync(query);

        public async Task<PagedResponse<T>> SendQuery<T>(IPagedQuery<T> query) => await mediator.DispatchAsync(query);
    }
}