using Microsoft.EntityFrameworkCore;
using MyRealm.Common.Entities;
namespace MyRealm.Common.Repositories
{
    public class DbContextRepository<TContext, TEntity> : IRepository<TEntity>
            where TEntity : class, IIntegerIdEntity
            where TContext : DbContext
    {
        protected readonly TContext DbContext;
        public DbContextRepository(TContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));
            DbContext = dbContext;
            if (DbContext.Set<TEntity>() is null)
                throw new ArgumentException($"No entity set of {typeof(TEntity)} was found in the db context");
        }
        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task InsertAsync(IEnumerable<TEntity> entitySet)
        {
            DbContext.Set<TEntity>().AddRange(entitySet);
            await DbContext.SaveChangesAsync();
        }
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
                await DeleteAsync(entity);
        }
    }

}
