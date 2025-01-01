using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Projects.Extensions;

public static class TracingExtensions
{
    public static IServiceCollection AddDistributedTracing(this IServiceCollection services)
    {
        const string serviceName = "project-api";
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(options =>
                    {
                        options.SetDbStatementForText = true;
                        options.SetDbStatementForStoredProcedure = true;
                    })
                    .AddConsoleExporter()
                    .AddOtlpExporter()
                    .AddJaegerExporter();
            });

        return services;

    }
}
