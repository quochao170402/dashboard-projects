using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Constants;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Models.Properties;

namespace Projects.Features.Settings.GenerateProjectProperties;

public class GenerateProjectPropertiesCommand(ProjectContext context, IMapper mapper)
    : IRequestHandler<GenerateProjectProperties, List<PropertyModel>>
{

    public async Task<List<PropertyModel>> Handle(GenerateProjectProperties request, CancellationToken cancellationToken)
    {
        var defaultProperties = await context.Properties
            .Where(x => !x.IsDeleted && x.PropertyType == PropertyType.Project && x.IsDefault)
            .ToListAsync(cancellationToken);

        if (defaultProperties.Count > 0)
        {
            return mapper.Map<List<Property>, List<PropertyModel>>(defaultProperties);
        }

        defaultProperties = typeof(DefaultProjectProperties)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x=> x.GetValue(null) != null)
            .Select(x=>(Property)x.GetValue(null)!)
            .ToList();

        context.Properties.AddRange(defaultProperties.ToList());

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<List<Property>, List<PropertyModel>>(defaultProperties);
    }
}
