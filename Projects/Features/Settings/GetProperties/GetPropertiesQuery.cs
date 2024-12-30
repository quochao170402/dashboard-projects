using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Constants;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Models.Properties;

namespace Projects.Features.Settings.GetProperties;

public class GetPropertiesQuery(ProjectContext context, IMapper mapper) : IRequestHandler<GetProperties, List<PropertyModel>>
{
    public async Task<List<PropertyModel>> Handle(GetProperties request, CancellationToken cancellationToken)
    {
        var query = context.Properties
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.PropertyType == request.Type)
            .AsQueryable();

        var properties = await query.ToListAsync(cancellationToken);

        if (properties.Count != 0) return mapper.Map<List<PropertyModel>>(properties);

        if (request.Type != PropertyType.Project) return mapper.Map<List<PropertyModel>>(properties);

        properties = typeof(DefaultProjectProperties)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x=> x.GetValue(null) != null)
            .Select(x=>(Property)x.GetValue(null)!)
            .ToList();

        context.Properties.AddRange(properties);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<List<PropertyModel>>(properties);
    }
}
