using Dashboard.API.Middlewares;
using Dashboard.Application.Extensions;
using Dashboard.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Add dependencies

builder.Services.AddDataAccess();
builder.Services.AddFeatures();

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy configuration
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

// Use appropriate CORS policy based on environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevelopmentPolicy");
}
else
{
    app.UseCors("ProductionPolicy");
}

// Add security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

await app.RunAsync();
