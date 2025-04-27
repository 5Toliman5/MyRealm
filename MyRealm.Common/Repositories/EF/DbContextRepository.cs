using Microsoft.EntityFrameworkCore;
using MyRealm.Common.Entities;
namespace MyRealm.Common.Repositories.EF
{
    public class DbContextRepository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IIntIdEntity
        where TContext : DbContext
        where TKey : struct
	{
        protected readonly TContext Context;

        public DbContextRepository(TContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));
            Context = dbContext;
            if (Context.Set<TEntity>() is null)
                throw new ArgumentException($"No entity set of {typeof(TEntity)} was found in the db context");
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task InsertAsync(ICollection<TEntity> entitySet)
        {
            Context.Set<TEntity>().AddRange(entitySet);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
                await DeleteAsync(entity);
        }
    }
}
