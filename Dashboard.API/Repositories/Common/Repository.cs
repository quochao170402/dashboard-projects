using Dashboard.API.Contexts;
using Dashboard.API.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Repositories.Common;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly DashboardContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DashboardContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> Get()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<bool> AddMany(IEnumerable<T> entities)
    {
        var enumerable = entities.ToList();
        _dbSet.AddRange(enumerable);
        return await _context.SaveChangesAsync() == enumerable.Count;
    }

    public async Task<bool> Add(T entity)
    {
        _dbSet.Add(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> Update(T entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> UpdateMany(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _dbSet.FindAsync(id)
                     ?? throw new ArgumentException("Entity not foundw");

        entity.LatestUpdatedAt = DateTime.Now;
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> DeleteMany(IEnumerable<Guid> ids)
    {
        return await _dbSet
                   .Where(b => ids.Contains(b!.Id))
                   .ExecuteUpdateAsync(setters => setters.SetProperty(b => b!.IsDeleted, b => true))
               == ids.Count();
    }
}