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
                    .AddAspNetCoreInstrumentation()     // Trace HTTP requests
                    .AddHttpClientInstrumentation()    // Trace outgoing HTTP calls
                    .AddEntityFrameworkCoreInstrumentation(options =>
                    {
                        // Optionally, customize EF Core tracing
                        options.SetDbStatementForText = true; // Includes SQL queries in traces
                        options.SetDbStatementForStoredProcedure = true; // Includes stored procedure calls
                    })
                    .AddConsoleExporter()              // Console exporter for debugging purposes
                    .AddOtlpExporter()
                    .AddJaegerExporter();
            });

        return services;

    }
}
