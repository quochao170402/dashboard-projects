namespace Dashboard.Domain.ProjectDomain;

public interface IProjectRepository
{
    Task<bool> IsNameExist(string name, Guid existingId, CancellationToken cancellationToken);
    Task<bool> IsKeyExist(string key, Guid existingId, CancellationToken cancellationToken);

    Task<(IEnumerable<Project> Projects, int Count)> FilterAsync(string keyword, int pageSize, int pageIndex,
        CancellationToken cancellationToken);

    Task<int> CountFilter(string keyword);
}
