using Arya.Domain;
using Arya.Domain.Entitties;
using Arya.Domain.Interfaces;
using Arya.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Arya.Infrastructure.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly MySqlContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public RepositoryBase(MySqlContext context) => _context = context;

        public async Task<bool> ExecuteSqlRaw(string sql) => await _context.Database.ExecuteSqlRawAsync(sql) > 0;

        public async Task<TEntity> Add(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;

            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(Guid entityId) => await _context.Set<TEntity>().FindAsync(entityId);

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> where) => await _context.Set<TEntity>().FirstOrDefaultAsync(where);

        public async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToArrayAsync();

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where) => await _context.Set<TEntity>().Where(where).ToListAsync();

        public async Task<TEntity> Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            
            _context.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Remove(Guid entityId)
        {
            _context.Set<TEntity>().Remove(await Get(entityId).ConfigureAwait(false));

            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose() => _context?.Dispose();
    }
}
