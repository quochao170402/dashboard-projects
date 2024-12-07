using Dashboard.Domain.TaskDomain;
using Dashboard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repositories;

public class TaskRepository(DashboardContext context) : Repository<TaskEntity>(context), ITaskRepository
{
    public async Task<bool> IsKeyExist(string key, Guid id, CancellationToken cancellationToken)
    {
        var query = _entities.AsNoTracking()
            .Where(x => x.Key == key)
            .AsQueryable();

        if (id != Guid.Empty) query = query.Where(x => x.Id != id);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<int> CountByProjectId(Guid projectId, CancellationToken cancellationToken)
    {
        return await _entities.Where(x => x.ProjectId == projectId).CountAsync(cancellationToken);
    }
}
