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

        return (properties: properties.Select(x => new ProjectSettingModel
        {
            Id = x.Id,
            Name = x.Name,
            Label = x.Label,
            Datatype = x.Datatype,
            Note = x.Note,
            IsDefault = x.IsDefault,
            IsUsed = x.IsUsed,
            Options = JsonConvert.DeserializeObject<List<string>>(x.Options ) ?? []
        }).ToList(), count);
    }
}
