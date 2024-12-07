namespace Dashboard.Domain.TaskDomain;

public interface ITaskRepository
{
    Task<bool> IsKeyExist(string key, Guid id, CancellationToken cancellationToken);
    Task<int> CountByProjectId(Guid projectId, CancellationToken cancellationToken);
}
