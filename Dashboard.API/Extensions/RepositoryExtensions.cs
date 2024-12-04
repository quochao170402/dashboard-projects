using Dashboard.API.Repositories;
using Dashboard.API.Repositories.Common;

namespace Dashboard.API.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        return services;
    }
}