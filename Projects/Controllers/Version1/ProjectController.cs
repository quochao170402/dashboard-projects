using System.Net;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Controllers.Base;
using Projects.Controllers.Payload;
using Projects.Features.Projects.CreateProject;
using Projects.Features.Projects.FilterProjects;
using Projects.Features.Projects.GetProjectOptions;
using Projects.Features.Projects.GetProjectsPaging;
using Projects.Services;

namespace Projects.Controllers.Version1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class ProjectController(IProjectService projectService, IMediator mediator) : BaseApiController
{
    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId)
    {
        var project = await projectService.GetById(projectId);
        return OkResponse(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetOptions()
    {
        var response = await mediator.Send(new GetProjectOptionRequest());

        return OkResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> Filter(int pageSize = 10, int pageIndex = 1)
    {
        var response = await mediator.Send(new GetProjectsPaging()
        {
            PageSize = pageSize,
            PageIndex = pageIndex
        });

        return OkResponse(new
        {
            Data = response.projects,
            Count = response.count
        });
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageSize = 10, int pageIndex = 1)
    {
        var response = await mediator.Send(new FilterProjects()
        {
            PageSize = pageSize,
            PageIndex = pageIndex
        });

        return OkResponse(new
        {
            Data = response.projects,
            Count = response.count
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddProject([FromBody] CreateProjectRequest request)
    {
        var response = await mediator.Send(request);
        return OkResponse(response, (int)HttpStatusCode.Created);
    }

    [HttpPut("{projectId:guid}")]
    public async Task<IActionResult> UpdateProject([FromRoute] Guid projectId, [FromBody] UpdateProjectRequest request)
    {
        var response = await projectService.Update(projectId, request);
        return OkResponse(response);
    }

    [HttpDelete("{projectId:guid}")]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid projectId)
    {
        var response = await projectService.Delete(projectId);
        return OkResponse(response);
    }
}
