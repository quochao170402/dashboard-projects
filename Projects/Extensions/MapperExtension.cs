using AutoMapper;
using Projects.Profiles;

namespace Projects.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var profiles = new MapperConfiguration(
            configure =>
            {
                configure.AddProfile(new ProjectProfile());
                configure.AddProfile(new PropertyProfile());
            }
        );

        services.AddSingleton(profiles.CreateMapper());
        return services;
    }
}
