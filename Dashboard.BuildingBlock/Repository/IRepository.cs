using System.Linq.Expressions;
using Dashboard.Domain.Common;

namespace Dashboard.BuildingBlock.Repository;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<bool> AddAsync(T entity, CancellationToken cancellationToken);
    Task<bool> AddManyAsync(List<T> entities, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateManyAsync(List<T> entities, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<bool> DeleteManyAsync(List<T> entities, CancellationToken cancellationToken);
}
