using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;

namespace Projects.Features.Settings.UpdateProjectSetting;

public class UpdateProjectSettingCommand(ProjectContext context) : IRequestHandler<UpdateProjectSettingRequest, bool>
{
    public async Task<bool> Handle(UpdateProjectSettingRequest request, CancellationToken cancellationToken)
    {
        var settings = request.Settings;

        await context.PropertySettings
            .Where(x => !x.IsDeleted && x.Type == PropertyType.Project)
            .ExecuteDeleteAsync(cancellationToken);

        context.PropertySettings.AddRange(settings.Select(x=> new PropertySetting
        {
            IsUsed = x.IsUsed,
            Type = PropertyType.Project,
            PropertyId = x.PropertyId
        }));

        return await context.SaveChangesAsync(cancellationToken) == settings.Count;
    }
}
