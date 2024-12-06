using Dashboard.BuildingBlock.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers.Base;

public class BaseApiController : ControllerBase
{
    internal IActionResult OkResponse<T>(T data, int statusCode = 200)
    {
        return StatusCode(statusCode, new Response
        {
            Success = true,
            StatusCode = statusCode,
            Messages = null,
            Data = data
        });
    }

    internal IActionResult ErrorResponse(string message = "", int statusCode = 500)
    {
        return StatusCode(statusCode, new Response
        {
            Success = false,
            StatusCode = statusCode,
            Messages = message,
            Data = null
        });
    }
}