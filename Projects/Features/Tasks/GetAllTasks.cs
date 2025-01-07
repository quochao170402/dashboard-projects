using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Entities;
using Projects.Exceptions;

namespace Projects.Features.Tasks;

public record GetAllTasks(
    string ProjectKey,
    int PageSize,
    int PageIndex) : IRequest<(List<TaskEntity> tasks, int count)>;

public class GetAllTasksQuery(ProjectContext context) : IRequestHandler<GetAllTasks, (List<TaskEntity> tasks, int count)>
{
    public async Task<(List<TaskEntity> tasks, int count)> Handle(GetAllTasks request, CancellationToken cancellationToken)
    {
        var project = await context.Projects
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.Key == request.ProjectKey,
                cancellationToken)
            ?? throw new EntityNotFoundException($"Not found project {request.ProjectKey}");

        var query = context.TaskEntities
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.ProjectId == project.Id)
            .AsQueryable();


        throw new NotImplementedException();
    }

}
