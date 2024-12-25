using AutoMapper;
using Projects.Profiles;

namespace Projects.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var profiles = new MapperConfiguration(
            _ => { _.AddProfile(new ProjectProfile()); }
        );


        services.AddSingleton(profiles.CreateMapper());
        return services;
    }
}
