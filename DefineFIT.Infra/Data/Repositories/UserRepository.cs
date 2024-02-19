using DefineFIT.Domain.Entities;
using DefineFIT.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DefineFIT.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.Users.AnyAsync(x => x.Cpf == cpf);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
