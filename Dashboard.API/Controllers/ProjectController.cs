using System.Net;
using Dashboard.API.Controllers.Base;
using Dashboard.API.Payload;
using Dashboard.Application.Features.Projects.AddProject;
using Dashboard.Application.Features.Projects.DeleteProject;
using Dashboard.Application.Features.Projects.FilterProject;
using Dashboard.Application.Features.Projects.GetProductById;
using Dashboard.Application.Features.Projects.UpdateProject;
using Dashboard.Application.Features.Projects.UpdateProjectStatus;
using Dashboard.BuildingBlock.DTO;
using Dashboard.BuildingBlock.Helpers;
using Dashboard.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProjectController(IMediator mediator) : BaseAPIController
{
    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetProjectByIdRequest { Id = projectId }, cancellationToken);
        return OkResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery] FilterProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return OkResponse(new
        {
            Data = response.projects,
            Count = response.count
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return OkResponse(response, (int)HttpStatusCode.Created);
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

        return OkResponse(response);
    }

    [HttpPatch("{projectId:guid}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid projectId, [FromBody] UpdateProjectStatusRequest request,
        CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(ProjectStatus), request.Status))
            return ErrorResponse("Status not valid", (int)HttpStatusCode.BadRequest);
        
        request.Id = projectId;
        var response = await mediator.Send(request, cancellationToken);
        
        return OkResponse(response);
    }

    [HttpDelete("{projectId:guid}")]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteProjectRequest
        {
            Id = projectId
        }, cancellationToken);
        return OkResponse(response);
    }
}
