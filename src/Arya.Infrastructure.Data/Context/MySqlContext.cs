using Arya.Domain.Entitties;
using Arya.Domain.Interfaces;
using Arya.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Arya.Infrastructure.Data.Context
{
    public class MySqlContext : DbContext, IUnitOfWork
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        public DbSet<UserEntity> User { get; set; }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
        }
    }
}
