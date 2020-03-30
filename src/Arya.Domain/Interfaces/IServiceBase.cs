using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arya.Domain.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : Entity
    {
        Task ExecuteSqlRaw(string sql);

        Task<TEntity> Add(TEntity entity);
        
        Task<TEntity> Update(TEntity entity);

        Task<bool> Remove(Guid entityId);

        Task<TEntity> Get(Guid entityId);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
