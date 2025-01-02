using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Controllers.Base;
using Projects.Features.Projects.Version2.CreateProject;
using Projects.Models.Projects;
using Projects.Services;

namespace Projects.Controllers.Version2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class ProjectController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProjectRequest request)
    {
        var response = await mediator.Send(request);
        return OkResponse(response);
    }

    [HttpPut("id:guid")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateProjectRequest request)
    {
        return OkResponse("");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return OkResponse("");
    }

    [HttpPatch]
    public async Task<IActionResult> DeleteMany()
    {
        return OkResponse("");
    }
}
