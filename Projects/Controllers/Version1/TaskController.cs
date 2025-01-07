using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Controllers.Base;

namespace Projects.Controllers.Version1;

public class TaskController(IMediator mediator) : BaseApiController
{
    [HttpGet("{projectKey}")]
    public async Task<IActionResult> FilterTasks([FromRoute] string projectKey)
    {
        return OkResponse("");
    }
}
