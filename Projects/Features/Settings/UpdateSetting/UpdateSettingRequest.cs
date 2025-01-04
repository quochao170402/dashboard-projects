using MediatR;

namespace Projects.Features.Settings.UpdateSetting;

public record UpdateSettingRequest(List<PropertySettingRequest> Settings) : IRequest<bool>;
public record PropertySettingRequest(Guid PropertyId, bool IsUsed);
