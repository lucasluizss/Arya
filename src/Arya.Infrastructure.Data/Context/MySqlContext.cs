using Arya.Domain;
using Arya.Domain.Entities;
using Arya.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arya.Infrastructure.Data.Context
{
	public class MySqlContext : DbContext
	{
		public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

		public DbSet<UserEntity> User { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			var createdDate = nameof(Entity<object>.CreatedDate);
			var updatedDate = nameof(Entity<object>.UpdatedDate);

			ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(createdDate) != default).ToList()
					.ForEach(entry =>
					{
						if (entry.State == EntityState.Added)
						{
							entry.Property(createdDate).CurrentValue = DateTime.Now;
						}

						if (entry.State == EntityState.Modified)
						{
							entry.Property(createdDate).IsModified = false;
							entry.Property(updatedDate).CurrentValue = DateTime.Now;
						}
					});

			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
