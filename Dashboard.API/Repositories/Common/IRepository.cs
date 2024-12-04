using Dashboard.API.Entities.Common;

namespace Dashboard.API.Repositories.Common;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetById(Guid id);
    Task<List<T>> Get();
    Task<bool> AddMany(IEnumerable<T> entities);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> UpdateMany(IEnumerable<T> entities);
    Task<bool> Delete(Guid id);
    Task<bool> DeleteMany(IEnumerable<Guid> ids);
}