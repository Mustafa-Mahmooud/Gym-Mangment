using Core.Entites;

namespace Presentation.Interfaces
{
    public interface IGeneric<T>where T:BaseEntity
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);    
    
    }
}
