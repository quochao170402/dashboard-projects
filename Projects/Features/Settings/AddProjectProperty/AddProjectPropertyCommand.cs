using MediatR;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Exceptions;

namespace Projects.Features.Settings.AddProjectProperty;

public class AddProjectPropertyCommand(ProjectContext context) : IRequestHandler<AddProjectProperty, bool>
{
    public async Task<bool> Handle(AddProjectProperty request, CancellationToken cancellationToken)
    {
        if (context.Properties.Any(x=>x.Name == request.Name))
        {
            throw new InvalidProjectException("Project property name was existed");
        }

        context.Properties.Add(new Property
        {
            Name = request.Name,
            Label = request.Name.ToLower(),
            Datatype = request.Datatype,
            PropertyType = PropertyType.Project
        });

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
