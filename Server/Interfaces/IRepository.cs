using Server.Models;

namespace Server.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<bool> Create(T entity);
        
        Task<bool> Delete(T entity);

        Task<bool> Update(T entity);
    }
}
