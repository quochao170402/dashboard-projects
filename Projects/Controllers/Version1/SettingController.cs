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
using Projects.Features.Settings.GetAllProperties;
using Projects.Features.Settings.GetProjectSettings;
using Projects.Features.Settings.UpdateProjectSetting;
using Projects.Features.Settings.UpdateProperty;
using Projects.Models.Properties;
using GetProperties = Projects.Features.Settings.GetProjectSettings.GetProperties;

namespace Projects.Controllers.Version1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class SettingController(IMediator mediator, IMapper mapper) : BaseApiController
{
    [HttpGet("{type}")]
    public async Task<IActionResult> GetAllProperties([FromRoute] PropertyType type)
    {
        var response = await mediator.Send(new GetAllProperties() { Type = type });

        return OkResponse(response);
    }

    [HttpGet("{type}")]
    public async Task<IActionResult> GetProperties([FromRoute] PropertyType type, [FromQuery] int pageSize,
        [FromQuery] int pageIndex)
    {
        var filter = new GetProperties(type, pageSize, pageIndex);
        var response = await mediator.Send(filter);

        return OkResponse(new
        {
            Data = response.properties,
            Count = response.count
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddProjectSetting([FromBody] AddProjectProperty request)
    {
        var response = await mediator.Send(request);

        return OkResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProjectSetting([FromBody] UpdateProjectSettingRequest request)
    {
        var properties = await mediator.Send(request);

        return OkResponse(properties);
    }

    [HttpPost]
    public async Task<IActionResult> AddProperty([FromBody] AddPropertyRequest request)
    {
        var properties = await mediator.Send(request);

        return OkResponse(properties);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProperty([FromRoute] Guid id, [FromBody] AddPropertyRequest request)
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
