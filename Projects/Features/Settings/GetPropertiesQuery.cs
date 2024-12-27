using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Enums;
using Projects.Models;
using Projects.Models.Properties;

namespace Projects.Features.Settings;

public class GetPropertiesQuery(ProjectContext context, IMapper mapper) : IRequestHandler<GetProperties, List<PropertyModel>>
{

    public async Task<List<PropertyModel>> Handle(GetProperties request, CancellationToken cancellationToken)
    {
        var query = context.Properties
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (request.Type != null)
        {
            query = query.Where(x => x.PropertyType == (PropertyType)request.Type);
        }

        var properties = await query.ToListAsync(cancellationToken);

        return mapper.Map<List<PropertyModel>>(properties);
    }
}
