namespace Dashboard.Domain.TaskDomain;

public interface ITaskRepository
{
    Task<bool> IsKeyExist(string key, Guid id, CancellationToken cancellationToken);
    Task<int> CountByProjectId(Guid projectId, CancellationToken cancellationToken);
    Task<List<TaskEntity>> FilterAsync(Guid projectId, string keyword, int pageSize, int pageIndex, CancellationToken cancellationToken);
    Task<int> CountByConditionAsync(Guid projectId, string keyword, CancellationToken cancellationToken);

}
