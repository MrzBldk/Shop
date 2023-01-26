using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Application.Commands.AddOrderItem;
using Ordering.Application.Commands.CreateOrder;
using Ordering.Application.Commands.SetAwaitingValidationStatus;
using Ordering.Application.Commands.SetCancelledStatus;
using Ordering.Application.Commands.SetShippedStatus;
using Ordering.Application.Commands.SetStockConfirmedStatus;
using Ordering.Application.Queries;
using Ordering.Application.Repositories;
using Ordering.Infrastructure.DataAccess;
using Ordering.Infrastructure.DataAccess.Queries;
using Ordering.Infrastructure.DataAccess.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IOrderReadOnlyRepository, OrderRepository>();
            services.AddTransient<IOrderWriteOnlyRepository, OrderRepository>();
            services.AddTransient<IOrderQueries, OrderQueries>();
            services.AddTransient<IAddOrderItemUseCase, AddOrderItemUseCase>();
            services.AddTransient<ICreateOrderUseCase, CreateOrderUseCase>();
            services.AddTransient<ISetAwaitingValidationStatusUseCase, SetAwaitingValidationStatusUseCase>();
            services.AddTransient<ISetCancelledStatusUseCase, SetCancelledStatusUseCase>();
            services.AddTransient<ISetShippedStatusUseCase, SetShippedStatusUseCase>();
            services.AddTransient<ISetStockConfirmedStatusUseCase, SetStockConfirmedStatusUseCase>();
            return services;
        }
    }
}
