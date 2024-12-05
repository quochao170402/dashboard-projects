using Dashboard.API.Payload;
using Dashboard.Application.Features.Projects.AddProject;
using Dashboard.Application.Features.Projects.FilterProject;
using Dashboard.Application.Features.Projects.UpdateProject;
using Dashboard.BuildingBlock.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery] FilterProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return StatusCode(201, new Response
        {
            Success = true,
            StatusCode = 201,
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

        return StatusCode(201, new Response
        {
            Success = true,
            StatusCode = 201,
            Messages = null,
            Data = response
        });
    }
}
