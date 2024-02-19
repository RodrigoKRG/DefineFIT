using DefineFIT.Domain.Entities;
using DefineFIT.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DefineFIT.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}
    

