using MediatR;
using Projects.Models.Settings;

namespace Projects.Features.Settings.GetProjectSettings;

public class GetProjectSetting : IRequest<List<ProjectSettingModel>>
{

}
