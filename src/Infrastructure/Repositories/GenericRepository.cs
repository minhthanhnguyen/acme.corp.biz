using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, T, TContext> : IGenericRepository<TEntity, T>
        where TEntity : class, IEntity<T>, new()
        where T : IComparable, IEquatable<T>
        where TContext : DbContext
    {
        private TContext _Context;
        protected readonly DbSet<TEntity> DbSet;

        protected TContext Context { get { return _Context; } }

        public GenericRepository(TContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            DbSet = context.Set<TEntity>();
            _Context = context;
        }

        public async Task<TEntity?> GetSingleAsync(T id)
        {
            if (id.Equals(default))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> condition)
        {
            ArgumentNullException.ThrowIfNull(condition);

            return await DbSet.SingleOrDefaultAsync(condition);
        }

        public IAsyncEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>>? condition = null)
        {
            return condition != null ? DbSet.Where(condition).AsAsyncEnumerable() : DbSet.AsAsyncEnumerable();
        }

        public async Task<T> AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            await DbSet.AddRangeAsync(entities);
            await Context.SaveChangesAsync();           
        }

        public async Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
