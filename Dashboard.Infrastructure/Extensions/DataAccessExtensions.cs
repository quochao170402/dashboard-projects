using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.ProjectDomain;
using Dashboard.Domain.TaskDomain;
using Dashboard.Infrastructure.DataAccess;
using Dashboard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Infrastructure.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services.AddDatabaseContext()
            .AddRepositories();
    }

    private static IServiceCollection AddDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContext<DashboardContext>(options =>
                options.UseInMemoryDatabase("Dashboard"))
            .AddLogging();

        return services;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();

        return services;
    }
}
