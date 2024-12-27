using Projects.Configurations;

namespace Projects.Extensions;

public static class CacheExtension
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisCacheSettingOption>(
            configuration.GetSection("Redis"));

        services.AddStackExchangeRedisCache(options =>
        {
            var redisSettings = configuration.GetSection("Redis").Get<RedisCacheSettingOption>()
                                ?? new RedisCacheSettingOption();

            options.Configuration = redisSettings.Host;
            options.InstanceName = redisSettings.Instance;
        });
        return services;
    }
}
