using MediatR;
using Projects.Enums;
using Projects.Models.Settings;

namespace Projects.Features.Settings.GetProjectSettings;

public record GetProperties(PropertyType type = PropertyType.Project, int pageSize = 10, int pageIndex = 1)
    : IRequest<(List<ProjectSettingModel> properties, int count)>;
