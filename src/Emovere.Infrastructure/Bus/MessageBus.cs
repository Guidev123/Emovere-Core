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

        public void Publish<T>(T message) where T : IntegrationEvent
            => TryConnect().PubSub.Publish(message);

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent 
            => await TryConnect().PubSub.PublishAsync(message);

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class 
            => TryConnect().PubSub.Subscribe(subscriptionId, onMessage);

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
            => TryConnect().PubSub.SubscribeAsync(subscriptionId, onMessage);

        public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent
            where TResponse : class
            => TryConnect().Rpc.Request<TRequest, TResponse>(request);

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent where TResponse : class 
            => await TryConnect().Rpc.RequestAsync<TRequest, TResponse>(request);

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent where TResponse : class
            => TryConnect().Rpc.Respond(responder);

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent where TResponse : class 
            => TryConnect().Rpc.RespondAsync(responder).GetAwaiter().GetResult();

        private IBus TryConnect()
        {
            if (IsConnected) return _bus;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

            policy.Execute(() => {
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

        public void Dispose() => _bus?.Dispose();
    }
}
