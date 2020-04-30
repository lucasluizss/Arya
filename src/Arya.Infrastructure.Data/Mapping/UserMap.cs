using Arya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arya.Infrastructure.Data.Mapping
{
    public sealed class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).HasColumnName(nameof(UserEntity.Id)).IsRequired();

            builder.Property(user => user.Status).HasColumnName(nameof(UserEntity.Status)).IsRequired();

            builder.Property(user => user.CreatedDate).HasColumnName(nameof(UserEntity.CreatedDate)).ValueGeneratedOnAdd().IsRequired();

            builder.Property(user => user.UpdatedDate).HasColumnName(nameof(UserEntity.UpdatedDate)).ValueGeneratedOnUpdate();

            builder.Property(user => user.Name).IsRequired().HasColumnName(nameof(UserEntity.Name)).HasMaxLength(100);
            
            builder.OwnsOne(user => user.Email, ownedBuilder =>
            {
                ownedBuilder.Property(email => email.Address).HasColumnName(nameof(UserEntity.Email)).HasMaxLength(300).IsRequired();
                ownedBuilder.HasIndex(email => email.Address).IsUnique();
            });

            builder.Property(user => user.Password).IsRequired().HasColumnName(nameof(UserEntity.Password)).HasMaxLength(100);
        }
    }
}
