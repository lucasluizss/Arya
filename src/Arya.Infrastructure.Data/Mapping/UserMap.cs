using Arya.Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arya.Infrastructure.Data.Mapping
{
    public sealed class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            
            builder.HasKey(c => c.Id);
            
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            
            builder.Property(x => x.Status).IsRequired();
            
            builder.Property(c => c.CreatedDate).ValueGeneratedOnAdd();
            
            builder.Property(c => c.UpdatedDate).ValueGeneratedOnUpdate();
            
            builder.Property(c => c.Name).IsRequired().HasColumnName(nameof(UserEntity.Name)).HasMaxLength(100);

            builder.OwnsOne(x => x.Email, y =>
            {
                y.Property(x => x.Address).IsRequired().HasColumnName(nameof(UserEntity.Email)).HasMaxLength(100);
                y.HasIndex(x => x.Address).IsUnique();
            });

            builder.Property(c => c.Password).IsRequired().HasColumnName(nameof(UserEntity.Password)).HasMaxLength(100);
        }
    }
}
