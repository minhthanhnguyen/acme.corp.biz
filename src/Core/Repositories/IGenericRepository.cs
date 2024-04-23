using Core.Entities;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IGenericRepository<TEntity, T>
        where TEntity : class, IEntity<T>, new()
        where T : IComparable, IEquatable<T>
    {
        Task<TEntity?> GetSingleAsync(T id);

        Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> condition);

        IAsyncEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>>? condition = null);

        Task<T> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
