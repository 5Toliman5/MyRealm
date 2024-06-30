using MyRealm.Domain.Common.Entities;
namespace MyRealm.Domain.Common.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(int id);
    }
}
