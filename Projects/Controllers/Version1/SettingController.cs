using System.Reflection;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Constants;
using Projects.Controllers.Base;
using Projects.Entities;
using Projects.Enums;
using Projects.Features.Settings.AddProjectProperty;
using Projects.Features.Settings.AddProperty;
using Projects.Features.Settings.GenerateProjectProperties;
using Projects.Features.Settings.GetProjectSettings;
using Projects.Features.Settings.GetProperties;
using Projects.Features.Settings.UpdateProjectSetting;
using Projects.Features.Settings.UpdateProperty;
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
    public async  Task<IActionResult> AddProjectSetting([FromBody] AddProjectProperty request)
    {
        var response = await mediator.Send(request);

        return OkResponse(response);
    }

    [HttpPost]
    public async  Task<IActionResult> UpdateProjectSetting([FromBody] UpdateProjectSettingRequest request)
    {
        var properties = await mediator.Send(request);

        return OkResponse(properties);
    }

    [HttpPost]
    public async  Task<IActionResult> AddProperty([FromBody] AddPropertyRequest request)
    {
        var properties = await mediator.Send(request);

        return OkResponse(properties);
    }

    [HttpPut("{id:guid}")]
    public async  Task<IActionResult> UpdateProperty([FromRoute] Guid id, [FromBody] AddPropertyRequest request)
    {
        var properties = await mediator.Send(new UpdatePropertyRequest
        {
            Name = request.Name,
            Label = request.Label,
            Datatype = request.Datatype,
            Note = request.Note,
            PropertyType = request.PropertyType,
            IsDefault = request.IsDefault,
            IsUsed = request.IsUsed,
            Id = id
        });

        return OkResponse(properties);
    }
}
