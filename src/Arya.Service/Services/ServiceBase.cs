using Arya.Domain.Entities;
using Arya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Arya.Service
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository) => _repository = repository;

        public Task ExecuteSqlRaw(string sql) => _repository.ExecuteSqlRaw(sql);

        public Task<TEntity> Add(TEntity entity) => _repository.Add(entity);

        public Task<TEntity> Get(Guid entityId) => _repository.Get(entityId);

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => _repository.Get(predicate);

        public Task<IEnumerable<TEntity>> GetAll() => _repository.GetAll();

        public Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate) => _repository.GetAll(predicate);

        public Task<TEntity> Update(TEntity entity) => _repository.Update(entity);

        public Task<bool> Remove(Guid entityId) => _repository.Remove(entityId);
    }
}
