using Dashboard.API.Controllers.Base;
using Dashboard.API.Payload.Tasks;
using Dashboard.Application.Features.Tasks.CreateTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers.Version1;

public class TaskController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Filter()
    {
        return OkResponse(new { status = "ok" });
    }

    [HttpGet]
    public async Task<IActionResult> CreateTask([FromBody] CreateTask request)
    {
        var result = await mediator.Send(new CreateTaskRequest
        {
            ProjectId = request.ProjectId,
            Summary = request.Summary
        });
        return OkResponse(result, 201);
    }
}
