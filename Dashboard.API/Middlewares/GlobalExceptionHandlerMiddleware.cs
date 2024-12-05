using System.Net;
using Dashboard.BuildingBlock.DTO;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.Domain.ProjectDomain;
using Newtonsoft.Json;

namespace Dashboard.API.Middlewares;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Handle exception in GlobalExceptionHandlerMiddleware");
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = exception switch
        {
            InvalidProjectException => (int)HttpStatusCode.BadRequest,
            EntityNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.BadRequest
        };

        context.Response.ContentType = "application/json";
        var response = new Response
        {
            Success = false,
            StatusCode = context.Response.StatusCode,
            Messages = exception.Message,
            Data = null
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}