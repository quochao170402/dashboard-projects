using Dashboard.API.Payload;
using Dashboard.Application.Features.Projects.AddProject;
using Dashboard.Application.Features.Projects.DeleteProject;
using Dashboard.Application.Features.Projects.FilterProject;
using Dashboard.Application.Features.Projects.GetProductById;
using Dashboard.Application.Features.Projects.UpdateProject;
using Dashboard.Application.Features.Projects.UpdateProjectStatus;
using Dashboard.BuildingBlock.DTO;
using Dashboard.BuildingBlock.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetProjectByIdRequest { Id = projectId }, cancellationToken);
        return StatusCode(200, new Response
        {
            Success = true,
            StatusCode = 200,
            Messages = null,
            Data = response
        });
    }

    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery] FilterProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return StatusCode(200, new Response
        {
            Success = true,
            StatusCode = 200,
            Messages = null,
            Data = new
            {
                Data = response.projects,
                Count = response.count
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return StatusCode(201, new Response
        {
            Success = true,
            StatusCode = 201,
            Messages = null,
            Data = response
        });
    }

    [HttpPut("{projectId:guid}")]
    public async Task<IActionResult> UpdateProject([FromRoute] Guid projectId, [FromBody] UpdateProject request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateProjectRequest
        {
            ExistingId = projectId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Name = request.Name,
            Description = request.Description,
            Url = request.Url
        }, cancellationToken);

        return StatusCode(200, new Response
        {
            Success = true,
            StatusCode = 200,
            Messages = null,
            Data = response
        });
    }

    [HttpPatch("{projectId:guid}/{status:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid projectId, [FromRoute] int status,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateProjectStatusRequest
        {
            Id = projectId,
            Status = status.CastToProjectStatus()
        }, cancellationToken);
        return StatusCode(200, new Response
        {
            Success = true,
            StatusCode = 200,
            Messages = null,
            Data = response
        });
    }

    [HttpDelete("{projectId:guid}")]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteProjectRequest
        {
            Id = projectId
        }, cancellationToken);
        return StatusCode(200, new Response
        {
            Success = true,
            StatusCode = 200,
            Messages = null,
            Data = response
        });
    }
}
