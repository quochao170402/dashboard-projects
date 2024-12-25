using Microsoft.EntityFrameworkCore;
using Projects.Context;

namespace Projects.Extensions;

public static class ContextExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
