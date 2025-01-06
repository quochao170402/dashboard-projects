using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projects.Controllers.Base;
using Projects.Enums;
using Projects.Features.Settings.AddProperty;
using Projects.Features.Settings.GetAllProperties;
using Projects.Features.Settings.UpdateProperty;
using Projects.Features.Settings.UpdatePropertySetting;
using Projects.Features.Settings.UpdateSetting;
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

    [HttpPut]
    public async Task<IActionResult> UpdateSetting([FromBody] UpdateSettingRequest request)
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
            Id = id,
            Options = request.Options
        });

        return OkResponse(properties);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePropertySetting([FromBody] UpdatePropertySettingRequest request)
    {
        var response = await mediator.Send(request);

        return OkResponse(response);
    }

}
