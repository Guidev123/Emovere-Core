using Emovere.SharedKernel.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

namespace Emovere.Infrastructure.EventSourcing
{
    public static class DependencyInjection
    {
        public static void AddEventStoreConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
        }
    }
}