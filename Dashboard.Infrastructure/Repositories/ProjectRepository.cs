using Dashboard.Domain.ProjectDomain;
using Dashboard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(DashboardContext context) : base(context)
    {
    }

    public async Task<bool> IsKeyExist(string key, Guid existingId, CancellationToken cancellationToken)
    {
        if (existingId == Guid.Empty)
            return await _entities.AnyAsync(x => !x.IsDeleted && x.Key == key, cancellationToken);

        return await _entities.AnyAsync(x => !x.IsDeleted && x.Key == key && x.Id == existingId, cancellationToken);
    }

    public async Task<bool> IsNameExist(string name, Guid existingId, CancellationToken cancellationToken)
    {
        if (existingId == Guid.Empty)
            return await _entities.AnyAsync(x => !x.IsDeleted && x.Name == name, cancellationToken);

        return await _entities.AnyAsync(x => !x.IsDeleted && x.Name == name && x.Id == existingId, cancellationToken);
    }

    public async Task<(IEnumerable<Project> Projects, int Count)> FilterAsync(string keyword, int pageSize,
        int pageIndex, CancellationToken cancellationToken)
    {
        var query = _entities
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(x => x.Name.Equals(keyword, StringComparison.CurrentCultureIgnoreCase));
        var count = await query.CountAsync(cancellationToken);

        query = query.Skip(pageSize * (pageIndex - 1))
            .Take(pageSize);
        var projects = await query.ToListAsync(cancellationToken);

        return (projects, count);
    }

    public async Task<int> CountFilter(string keyword)
    {
        var query = _entities
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(x => x.Name.Equals(keyword, StringComparison.CurrentCultureIgnoreCase));

        return await query.CountAsync();
    }
}
