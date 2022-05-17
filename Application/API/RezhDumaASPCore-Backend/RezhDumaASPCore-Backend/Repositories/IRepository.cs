using System.Collections.Generic;
using System.Threading.Tasks;

namespace RezhDumaASPCore_Backend.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        Task<T> Add(T entity);
        Task<T> Update(string id, T newEntity);
        Task<T> Delete(string id);
    }
}
