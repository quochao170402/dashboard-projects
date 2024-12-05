using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application.Extensions;

public static class FeatureExtensions
{
    public static void AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
