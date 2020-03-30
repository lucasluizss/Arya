using Arya.Domain.Interfaces;
using Arya.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace Arya.Infrastructure.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MySqlContext _context;

        public UnitOfWork(MySqlContext context) => _context = context;

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
