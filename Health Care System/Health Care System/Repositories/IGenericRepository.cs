using Health_Care_System.Entities;

namespace Health_Care_System.Repositories
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
