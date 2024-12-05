using System.Linq.Expressions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.Common;
using Dashboard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly DashboardContext _context;
    protected readonly DbSet<T> _entities;

    public Repository(DashboardContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var results = await _entities.Where(predicate).ToListAsync(cancellationToken);
        return results;
    }

    public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken)
    {
        _entities.Add(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> AddManyAsync(List<T> entities, CancellationToken cancellationToken)
    {
        _entities.AddRange(entities);

        return await _context.SaveChangesAsync(cancellationToken) == entities.Count;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _entities.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateManyAsync(List<T> entities, CancellationToken cancellationToken)
    {
        _entities.UpdateRange(entities);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        _entities.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteManyAsync(List<T> entities, CancellationToken cancellationToken)
    {
        entities.ForEach(entity => entity.IsDeleted = false);
        _entities.UpdateRange(entities);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
