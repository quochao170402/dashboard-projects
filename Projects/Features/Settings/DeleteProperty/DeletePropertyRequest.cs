using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Exceptions;

namespace Projects.Features.Settings.DeleteProperty;

public record DeletePropertyRequest(Guid propertyId) : IRequest<bool>;

public class DeletePropertyCommand(ProjectContext context) : IRequestHandler<DeletePropertyRequest, bool>
{
    public async Task<bool> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
    {
        var property = await context.Properties.FirstOrDefaultAsync(x=> x.Id == request.propertyId, cancellationToken)
            ?? throw new EntityNotFoundException("Not found property");

        context.Properties.Remove(property);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
