using DefineFIT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefineFIT.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Cpf).HasColumnName("Cpf").HasColumnType("varchar(15)").IsRequired();
            builder.Property(p => p.BirthDate).HasColumnName("BirthDate");
            builder.Property(p => p.Email).HasColumnName("Email").HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Password).HasColumnName("Password").HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber").HasColumnType("varchar(15)");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime").IsRequired();
            builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("datetime");
            builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(50)");
            builder.Property(p => p.Active).HasColumnName("Active").HasColumnType("bit").HasDefaultValue(true).IsRequired();
        }
   
    }
}
