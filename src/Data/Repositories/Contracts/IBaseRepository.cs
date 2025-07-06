using System.Linq.Expressions;

namespace Data.Repositories.Contracts;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity token);
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> filter);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task UpdateAsync(TEntity token);
}
