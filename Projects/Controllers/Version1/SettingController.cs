using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Controllers.Base;
using Projects.Features.Projects.GetAllProjects;
using Projects.Features.Settings;

namespace Projects.Controllers.Version1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class SettingController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async  Task<IActionResult> GetProjectProperties()
    {
        var properties = await mediator.Send(new GetProperties());
        return OkResponse(properties);
    }
}
