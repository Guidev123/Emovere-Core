using Emovere.Infrastructure.Bus;
using Emovere.Infrastructure.Email;
using Emovere.Infrastructure.EventSourcing;
using Emovere.SharedKernel.Abstractions.Mediator;
using Emovere.SharedKernel.Notifications;
using Emovere.WebApi.Middlewares;
using Emovere.WebApi.Services;
using KeyPairJWT.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendGrid.Extensions.DependencyInjection;
using Serilog;

namespace Emovere.WebApi.Config
{
    public static class SharedConfig
    {
        public static WebApplicationBuilder AddSharedConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddEventStoreConfiguration();

            builder.Services.ConfigureEmailServices(builder.Configuration);
            builder.Services.ConfigureJwtAndAuthorization(builder.Configuration);
            builder.Services.ConfigureMiddlewaresAndHandlers(builder.Configuration);
            builder.Services.ConfigureMessageBus(builder.Configuration);
            builder.AddAspNetUser();
            builder.AddSerilog();

            return builder;
        }

        private static void ConfigureEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGrid(x =>
            {
                x.ApiKey = configuration["EmailSettings:ApiKey"];
            });
            services.AddScoped<IEmailService, EmailService>();
        }

        private static void ConfigureJwtAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtConfiguration(configuration);
            services.AddAuthorization();
        }

        private static void ConfigureMiddlewaresAndHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GlobalExceptionMiddleware>();
            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        private static void ConfigureMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MessageBusConnection") ?? string.Empty;
            services.AddMessageBus(connectionString);
        }

        public static WebApplication UseMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            return app;
        }

        public static WebApplication UseApiSecurityConfig(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);

                if (context.HostingEnvironment.IsDevelopment())
                    configuration.WriteTo.Debug();
            });

            return builder;
        }

        public static WebApplicationBuilder AddAspNetUser(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAspNetUserService, AspNetUserService>();
            return builder;
        }

        public static WebApplication UseSerilogSettings(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host);
                    diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                    diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                };
            });

            return app;
        }
    }
}