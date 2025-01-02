using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Entities;
using Projects.Exceptions;

namespace Projects.Features.Settings.UpdateProperty;

public class UpdatePropertyCommand(ProjectContext context) : IRequestHandler<UpdatePropertyRequest, bool>
{
    public async Task<bool> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
    {
        var existing = await context.Properties
                           .FirstOrDefaultAsync(x=> !x.IsDeleted && x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Not found property");

        if (await IsExisted(existing.Id, request.Name, cancellationToken))
        {
            throw new ArgumentException("Property name is existed");
        }

        existing.Name = request.Name;
        existing.Label = request.Label;
        existing.Datatype = request.Datatype;
        existing.Note = request.Note;
        existing.PropertyType = request.PropertyType;
        existing.IsDefault = request.IsDefault;

        var setting = await context.PropertySettings
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.PropertyId == existing.Id,
                cancellationToken);

        if (setting is not null)
        {
            setting.IsUsed = request.IsUsed;
            context.PropertySettings.Update(setting);
        }
        else
        {
            var projectSetting = new PropertySetting()
            {
                Property = existing,
                IsUsed = request.IsUsed
            };
            context.PropertySettings.Add(projectSetting);
        }

        context.Properties.Update(existing);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    private async Task<bool> IsExisted(Guid id, string name, CancellationToken cancellationToken)
    {
        return await context.Properties.AnyAsync(x => !x.IsDefault
                                                      && x.Id != id
                                                      && x.Name == name,
            cancellationToken);
    }
}
