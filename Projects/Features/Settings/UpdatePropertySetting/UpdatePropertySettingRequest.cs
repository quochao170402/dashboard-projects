using MediatR;
using Projects.Enums;

namespace Projects.Features.Settings.UpdatePropertySetting;

public record UpdatePropertySettingRequest(
    Guid PropertyId,
    Guid EntityId,
    string Value) : IRequest<bool>;
