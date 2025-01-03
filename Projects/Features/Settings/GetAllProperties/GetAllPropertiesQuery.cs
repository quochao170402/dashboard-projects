using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Projects.Constants;
using Projects.Context;
using Projects.Entities;
using Projects.Models.Settings;

namespace Projects.Features.Settings.GetAllProperties;

public class GetAllPropertiesQuery(ProjectContext context, IMapper mapper)
    : IRequestHandler<GetAllProperties, List<ProjectSettingModel>>
{
    public async Task<List<ProjectSettingModel>> Handle(GetAllProperties request, CancellationToken cancellationToken)
    {
        var properties = await context.Properties
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.PropertyType == request.Type)
            .ToListAsync(cancellationToken);

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

        return properties.Select(x =>
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
        }).ToList();
    }
}
