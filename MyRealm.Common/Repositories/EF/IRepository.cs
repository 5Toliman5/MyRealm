namespace MyRealm.Common.Repositories.EF
{
    public interface IRepository<TEntity, TId> 
        where TEntity : class 
        where TId : struct
    {
        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(TId id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task InsertAsync(ICollection<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteByIdAsync(TId id);
    }
}
