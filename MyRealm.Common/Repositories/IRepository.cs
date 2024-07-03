namespace MyRealm.Common.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class where TId : struct
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TId id);
        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(TId id);
    }

}
