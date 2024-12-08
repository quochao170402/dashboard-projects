using Asp.Versioning;
using AutoMapper;
using Dashboard.API.Controllers.Base;
using Dashboard.API.Payload.Tasks;
using Dashboard.Application.Features.Common;
using Dashboard.Application.Features.Tasks.CreateTask;
using Dashboard.Application.Features.Tasks.DeleteTask;
using Dashboard.Application.Features.Tasks.FilterTask;
using Dashboard.Application.Features.Tasks.UpdateTask;
using Dashboard.Domain.TaskDomain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers.Version1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class TaskController(IMediator mediator, IMapper mapper) : BaseApiController
{
    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> FilterTask([FromRoute] Guid projectId, [FromQuery] FilterRequest request)
    {
        var result = await mediator.Send(new FilterTaskRequest()
        {
            ProjectId = projectId,
            Keyword = request.Keyword,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        });
        return OkResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var result = await mediator.Send(request);
        return OkResponse(result, 201);
    }


    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> UpdateTask([FromRoute] Guid taskId, [FromBody] CreateTaskRequest request)
    {
        var updateRequest = mapper.Map<CreateTaskRequest, UpdateTaskRequest>(request);

        updateRequest.Id = taskId;
        var result = await mediator.Send(updateRequest);

        return OkResponse(result, 201);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
    {
        var result = await mediator.Send(new DeleteTaskRequest()
        {
            Id = taskId
        });

        return OkResponse(result, 201);
    }
}
