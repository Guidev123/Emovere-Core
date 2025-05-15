using Microsoft.Extensions.DependencyInjection;

namespace Emovere.WebApi.Services
{
    public static class AspNetUserDependencyInjector
    {
        public static IServiceCollection AddAspNetUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUserService, AspNetUserService>();
            return services;
        }
    }
}