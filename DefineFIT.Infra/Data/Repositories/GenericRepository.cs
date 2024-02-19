using DefineFIT.Domain.Entities;
using DefineFIT.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DefineFIT.Infra.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _set;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync() =>
            await _set.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(long id) =>
            await _set.FindAsync(id);

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            _set.Remove(entity);
            return await _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
        }
    }
}
