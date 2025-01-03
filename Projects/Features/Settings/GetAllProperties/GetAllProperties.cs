using MediatR;
using Projects.Enums;
using Projects.Models.Settings;

namespace Projects.Features.Settings.GetAllProperties;

public class GetAllProperties : IRequest<List<ProjectSettingModel>>
{
    public PropertyType Type { get; set; } = PropertyType.Project;
}
