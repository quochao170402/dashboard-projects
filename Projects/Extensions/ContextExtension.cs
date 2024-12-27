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

        services.AddHttpContextAccessor(); // Register HttpContextAccessor

        return services;
    }

    public static void AutoApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProjectContext>();
        context.Database.Migrate();

    }
}
