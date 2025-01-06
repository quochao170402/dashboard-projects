using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Entities;
using Projects.Exceptions;

namespace Projects.Features.Settings.UpdatePropertySetting;

public class UpdatePropertySettingCommand(ProjectContext context) : IRequestHandler<UpdatePropertySettingRequest, bool>
{
    public async Task<bool> Handle(UpdatePropertySettingRequest request, CancellationToken cancellationToken)
    {
        var propertyValue = await context.PropertyValues
            .FirstOrDefaultAsync(x => !x.IsDeleted
                                      && x.PropertyId == request.PropertyId &&
                                      x.EntityId == request.EntityId,
                cancellationToken: cancellationToken);

        if (propertyValue != null)
        {
            propertyValue.Value = request.Value;
            context.PropertyValues.Update(propertyValue);
        }
        else
        {
            propertyValue = new PropertyValue
            {
                EntityId = request.EntityId,
                Value = request.Value,
                PropertyId = request.PropertyId,
            };

            context.PropertyValues.Add(propertyValue);
        }

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
