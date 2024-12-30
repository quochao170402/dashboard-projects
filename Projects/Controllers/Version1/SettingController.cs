using System.Reflection;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Constants;
using Projects.Controllers.Base;
using Projects.Entities;
using Projects.Enums;
using Projects.Features.Settings.GenerateProjectProperties;
using Projects.Features.Settings.GetProjectSettings;
using Projects.Features.Settings.GetProperties;
using Projects.Features.Settings.UpdateProjectSetting;
using Projects.Models.Properties;

namespace Projects.Controllers.Version1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class SettingController(IMediator mediator, IMapper mapper) : BaseApiController
{
    [HttpGet("{type}")]
    public async  Task<IActionResult> GetProperties([FromRoute] PropertyType type)
    {
        var properties = await mediator.Send(new GetProjectSetting());

        return OkResponse(properties);
    }

    [HttpPost]
    public async  Task<IActionResult> UpdateProjectSetting([FromBody] UpdateProjectSettingRequest request)
    {
        var properties = await mediator.Send(request);

        return OkResponse(properties);
    }

    // [HttpGet]
    // public async  Task<IActionResult> GetProjectPropertiesByProjectIds([FromBody] )
    // {
    //     var properties = await mediator.Send(new GetProperties());
    //
    //     if (properties.Count == 0)
    //     {
    //         properties = await mediator.Send(new GenerateProjectProperties());
    //     }
    //
    //     return OkResponse(properties);
    // }
}
