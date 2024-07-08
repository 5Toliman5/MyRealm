using MyRealm.Common.Entities;

namespace MyRealm.Common.Repositories
{
    public interface IRepository<T> : IRepository<T, int> where T : class, IIntegerIdEntity
    {

    }
}
