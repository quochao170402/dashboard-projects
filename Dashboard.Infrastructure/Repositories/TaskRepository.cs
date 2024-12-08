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

    public async Task<List<TaskEntity>> FilterAsync(Guid projectId, string keyword, int pageSize, int pageIndex, CancellationToken cancellationToken)
    {
        var query = CreateQuery(projectId, keyword);
        query = query.Skip(pageSize * (pageIndex - 1))
           .Take(pageSize);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> CountByConditionAsync(Guid projectId, string keyword, CancellationToken cancellationToken)
    {
        var query = CreateQuery(projectId, keyword);
        return await query.CountAsync(cancellationToken);
    }

    private IQueryable<TaskEntity> CreateQuery(Guid projectId, string keyword)
    {
        var query = _entities.Where(x => !x.IsDeleted && x.ProjectId == projectId)
        .AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x =>
            x.Summary.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
            || x.Key.Contains(keyword, StringComparison.CurrentCultureIgnoreCase));
        }

        return query;
    }

}
