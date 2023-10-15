using Autofac;
using Catalog.API.IntegrationEvents;
using Catalog.API.IntegrationEvents.EventHandling;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddIntegrationServicesAndEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IntegrationEventLogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp =>
                (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                ConnectionFactory factory = new()
                {
                    DispatchConsumersAsync = true,
                    HostName = "rabbitmq"
                };

                return new DefaultRabbitMQPersistentConnection(factory, logger);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionCLientName = "catalog";
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionCLientName);
            });

            services.AddTransient<OrderStockConfirmedIntegrationEventHandler>();
            services.AddTransient<StoreBlockedIntegrationEventHandler>();
            services.AddTransient<StoreUnblockedIntegrationEventHandler>();

            return services;
        }
    }
}