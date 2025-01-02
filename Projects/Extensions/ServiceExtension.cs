using System.Reflection;
using MediatR;
using Projects.Features.Projects.Version1.CreateProject;
using Projects.Services;

namespace Projects.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheService, CacheService>();

        services.AddScoped<IProjectService, ProjectService>();

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        // services.AddMediatR(typeof(Program));

        // Commands
        services.AddMediatR(typeof(CreateProjectCommand));
        // services.AddMediatR(typeof(UpdateCartCommandHandler));
        // services.AddMediatR(typeof(ProcessOrderCommandHandler));
        // services.AddMediatR(typeof(CreateOrderCommandHandler));
        // services.AddMediatR(typeof(RemoveItemCommandHandler));
        //
        // // Queries
        // services.AddMediatR(typeof(GetAllOrderQueryHandler));
        // services.AddMediatR(typeof(GetCustomerCurrentCartQueryHandler));
        // services.AddMediatR(typeof(GetOrdersOfCustomerQueryHandler));
        // services.AddMediatR(typeof(GetOrderDetailQueryHandler));
        // services.AddMediatR(typeof(GetTotalRevenueHandler));
        //
        // services.AddMediatR(typeof(OrderReportByStatusQueryHandler));
        // services.AddMediatR(typeof(OrderReportQueryHandler));
        // services.AddMediatR(typeof(GetOrderReportInRangeHandler));
        // services.AddMediatR(typeof(GetCustomerSalesHandler));
        return services;
    }
}
