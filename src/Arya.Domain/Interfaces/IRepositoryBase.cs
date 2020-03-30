using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Arya.Domain.Entitties
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Get(Guid entityId);
        
        Task<TEntity> Get(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where);

        Task<TEntity> Update(TEntity entity);

        Task<bool> Remove(Guid entityId);

        Task<bool> ExecuteSqlRaw(string sql);
    }
}
