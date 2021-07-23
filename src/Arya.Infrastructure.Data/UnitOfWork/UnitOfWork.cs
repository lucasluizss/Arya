using Arya.Domain.Interfaces;
using Arya.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace Arya.Infrastructure.Data.UnitOfWork
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly MySqlContext _context;

		public UnitOfWork(MySqlContext context) => _context = context;

		public async Task<bool> Commit() => await _context.SaveChangesAsync(default) > 0;
	}
}
