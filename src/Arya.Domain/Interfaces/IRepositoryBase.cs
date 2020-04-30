using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Arya.Domain.Entities
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Get(Guid entityId);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Update(TEntity entity);

        Task<bool> Remove(Guid entityId);

        Task<bool> ExecuteSqlRaw(string sql);
    }
}
