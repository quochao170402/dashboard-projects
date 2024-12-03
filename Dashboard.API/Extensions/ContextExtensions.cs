using Dashboard.API.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Extensions;

public static class ContextExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        services.AddDbContext<DashboardContext>(options => { options.UseInMemoryDatabase("Dashboard"); });

        return services;
    }
}
