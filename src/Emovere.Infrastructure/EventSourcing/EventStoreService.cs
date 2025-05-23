﻿using EventStore.Client;
using Microsoft.Extensions.Configuration;

namespace Emovere.Infrastructure.EventSourcing
{
    public class EventStoreService(IConfiguration configuration) : IEventStoreService
    {
        public EventStoreClient GetStoreClientConnection()
        {
            var connectionString = configuration.GetConnectionString("EventStoreConnection") ?? string.Empty;
            return new EventStoreClient(EventStoreClientSettings.Create(connectionString));
        }
    }
}