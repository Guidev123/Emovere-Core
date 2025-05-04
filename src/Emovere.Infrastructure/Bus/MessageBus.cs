using EasyNetQ;
using Emovere.SharedKernel.Events;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace Emovere.Infrastructure.Bus
{
    public sealed class MessageBus : IMessageBus
    {
        private IBus _bus = default!;
        private IAdvancedBus _advancedBus = default!;
        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public IAdvancedBus AdvancedBus => _bus.Advanced;

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : Event
            => await TryConnect().PubSub.PublishAsync(message, cancellationToken);

        public async Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage, CancellationToken cancellationToken = default)
            where T : class
            => await TryConnect().PubSub.SubscribeAsync(subscriptionId, onMessage, cancellationToken);

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : Event where TResponse : class
            => await TryConnect().Rpc.RequestAsync<TRequest, TResponse>(request, cancellationToken);

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder, CancellationToken cancellationToken = default)
            where TRequest : Event where TResponse : class
            => TryConnect().Rpc.RespondAsync(responder, cancellationToken).GetAwaiter().GetResult();

        private IBus TryConnect()
        {
            if (IsConnected) return _bus;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect!;
            });

            return _bus;
        }

        private void OnDisconnect(object x, EventArgs y)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            _bus?.Dispose();
            _advancedBus?.Dispose();
        }
    }
}