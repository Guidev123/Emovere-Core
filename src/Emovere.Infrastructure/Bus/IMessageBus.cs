using EasyNetQ;
using Emovere.SharedKernel.Events;

namespace Emovere.Infrastructure.Bus
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }

        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : Event;

        Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage, CancellationToken cancellationToken = default) where T : class;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : Event
            where TResponse : class;

        IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder, CancellationToken cancellationToken = default)
            where TRequest : Event
            where TResponse : class;
    }
}