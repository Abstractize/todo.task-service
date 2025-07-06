using System.Linq.Expressions;
using Data.Context;
using Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementation;

public abstract class BaseRepository<TEntity>(DatabaseContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DatabaseContext _context = context;

    public async Task AddAsync(TEntity token)
    {
        await _context.Set<TEntity>().AddAsync(token);

        await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
    {
        _context.RemoveRange(await _context.Set<TEntity>().Where(filter).ToListAsync());
        return await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> source = _context.Set<TEntity>().AsNoTracking();

        if (filter != null)
        {
            source = source.Where(filter);
        }

        return await source.ToListAsync();
    }

    public async Task UpdateAsync(TEntity token)
    {
        _context.Set<TEntity>().Update(token);
        await _context.SaveChangesAsync();
    }
}