using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Projects.Extensions;
using Projects.Filters;
using Projects.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();


builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddMapper();
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddDistributedTracing();

#region Versioning

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project API V1", Version = "v1.0" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Project API V1", Version = "v2.0" });
    c.OperationFilter<SwaggerOperationFilter>();

});
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("ProductionPolicy", policy =>
    {
        policy.WithOrigins("https://quochao.id.vn")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// app.Services.AutoApplyMigrations();

// Use appropriate CORS policy based on environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Project API v2");
    });    app.UseCors("DevelopmentPolicy");
}
else
{
    app.UseCors("ProductionPolicy");
}

// Add security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    await next();
});

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

await app.RunAsync();
