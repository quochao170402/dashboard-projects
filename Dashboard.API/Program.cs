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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

await app.RunAsync();
