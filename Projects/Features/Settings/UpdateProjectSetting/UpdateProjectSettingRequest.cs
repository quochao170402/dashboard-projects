using MediatR;

namespace Projects.Features.Settings.UpdateProjectSetting;

public class UpdateProjectSettingRequest : IRequest<bool>
{
    public List<PropertySettingRequest> Settings { get; set; } = [];
}

public class PropertySettingRequest
{
    public Guid PropertyId { get; set; }
    public bool IsUsed { get; set; }
}
