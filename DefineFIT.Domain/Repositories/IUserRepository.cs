using DefineFIT.Domain.Entities;

namespace DefineFIT.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<User?> GetByEmailAsync(string email);
    }
}
