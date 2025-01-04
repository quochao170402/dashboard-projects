using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;

namespace Projects.Features.Settings.UpdateSetting;

public class UpdateSettingCommand(ProjectContext context) : IRequestHandler<UpdateSettingRequest, bool>
{
    public async Task<bool> Handle(UpdateSettingRequest request, CancellationToken cancellationToken)
    {
        var settingModels = request.Settings;

        var propertyIds = settingModels.Select(x => x.PropertyId).ToList();

        var properties = await context.Properties
            .Where(x => propertyIds.Any(y => y == x.Id))
            .ToListAsync(cancellationToken);

        var propertyDict = settingModels.ToDictionary(x => x.PropertyId);

        properties.ForEach(x =>
        {
            if (propertyDict.TryGetValue(x.Id, out var value))
            {
                x.IsUsed = value.IsUsed;
            }
        });

        context.Properties.UpdateRange(properties);

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
