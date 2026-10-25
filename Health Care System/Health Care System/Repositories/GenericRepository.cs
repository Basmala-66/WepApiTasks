using Health_Care_System.Data;
using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
                return false;

           _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
           var model = await GetByIdAsync(entity.Id);

            if (model is null)
                return false;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        
    }
}
