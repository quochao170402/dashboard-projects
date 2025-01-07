using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Exceptions;
using Projects.Models.Properties;

namespace Projects.Features.Settings.InitTaskProperties;

public record InitTaskProperties(string ProjectKey) : IRequest<List<PropertyModel>>;

public class InitTaskPropertyCommand(ProjectContext context) : IRequestHandler<InitTaskProperties, List<PropertyModel>>
{
    public async Task<List<PropertyModel>> Handle(InitTaskProperties request, CancellationToken cancellationToken)
    {
        var project = await context.Projects.FirstOrDefaultAsync(x => x.Key == request.ProjectKey, cancellationToken)
            ?? throw new EntityNotFoundException($"Not found project {request.ProjectKey}");
        throw new NotImplementedException();
    }
}
