namespace DefineFIT.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(long id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
    }
}
