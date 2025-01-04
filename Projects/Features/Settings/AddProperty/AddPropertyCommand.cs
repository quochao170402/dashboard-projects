using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Projects.Context;
using Projects.Entities;

namespace Projects.Features.Settings.AddProperty;

public class AddPropertyCommand(ProjectContext context) : IRequestHandler<AddPropertyRequest, bool>
{
    public async Task<bool> Handle(AddPropertyRequest request, CancellationToken cancellationToken)
    {
        if (await IsExisted(request.Name, cancellationToken))
        {
            throw new ArgumentException("Property name is existed");
        }

        var property = new Property
        {
            Name = request.Name,
            Label = request.Label,
            Datatype = request.Datatype,
            Note = request.Note,
            PropertyType = request.PropertyType,
            IsDefault = request.IsDefault,
            Options = JsonConvert.SerializeObject(request.Options),
            IsUsed = request.IsUsed,
        };

        context.Properties.Add(property);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    private async Task<bool> IsExisted(string name, CancellationToken cancellationToken)
    {
        return await context.Properties.AnyAsync(x => !x.IsDeleted && x.Name == name, cancellationToken);
    }
}
