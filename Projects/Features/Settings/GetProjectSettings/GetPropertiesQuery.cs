using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Projects.Constants;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Models.Settings;

namespace Projects.Features.Settings.GetProjectSettings;

public class GetPropertiesQuery(ProjectContext context)
    : IRequestHandler<GetProperties, (List<ProjectSettingModel> properties, int count)>
{
    public async Task<(List<ProjectSettingModel> properties, int count)> Handle(GetProperties request, CancellationToken cancellationToken)
    {
        var query = context.Properties
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.PropertyType == request.type)
            .AsQueryable();

        var properties = await query.OrderBy(x => x.Name)
            .Skip((request.pageIndex - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        if (properties.Count == 0)
        {
            properties = typeof(DefaultProjectProperties)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.GetValue(null) != null)
                .Select(x => (Property)x.GetValue(null)!)
                .ToList();

            context.Properties.AddRange(properties);

            await context.SaveChangesAsync(cancellationToken);
        }

        var propertyIds = properties.Select(x => x.Id);

        var projectSettings = await context.PropertySettings
            .AsNoTracking()
            .Where(x => !x.IsDeleted && propertyIds.Any(y => y == x.PropertyId))
            .ToListAsync(cancellationToken);

        var settingMap = projectSettings.ToDictionary(x => x.PropertyId);

        return (properties: properties.Select(x =>
        {
            var isFound = settingMap.TryGetValue(x.Id, out var setting);
            return new ProjectSettingModel
            {
                Id = x.Id,
                Name = x.Name,
                Label = x.Label,
                Datatype = x.Datatype,
                Note = x.Note,
                IsDefault = x.IsDefault,
                IsUsed = isFound && setting!.IsUsed,
                Options = JsonConvert.DeserializeObject<List<string>>(x.Options ) ?? []
            };
        }).ToList(), count);
    }
}
