using Arya.Domain.Entities;
using Arya.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Arya.Infrastructure.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MySqlContext _context;

        public RepositoryBase(MySqlContext context) => _context = context;

        public async Task<bool> ExecuteSqlRaw(string sql) => await _context.Database.ExecuteSqlRawAsync(sql) > 0;

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().AnyAsync(predicate);

        public async Task<TEntity> Get(Guid entityId) => await _context.Set<TEntity>().FindAsync(entityId);

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToArrayAsync();

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Update(entity);
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        public async Task<bool> Remove(Guid entityId)
        {
            var result = _context.Set<TEntity>().Remove(await Get(entityId).ConfigureAwait(false));

            return result.State == EntityState.Deleted;
        }

        public void Dispose() => _context?.Dispose();
    }
}
