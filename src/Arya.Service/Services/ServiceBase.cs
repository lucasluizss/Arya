using Arya.Domain;
using Arya.Domain.Entitties;
using Arya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arya.Service
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : Entity
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository) => _repository = repository;

        public async Task ExecuteSqlRaw(string sql) => await _repository.ExecuteSqlRaw(sql);

        public async Task<TEntity> Add(TEntity entity) => await _repository.Add(entity);

        public async Task<TEntity> Get(Guid entityId) => await _repository.Get(entityId);

        public async Task<IEnumerable<TEntity>> GetAll() => await _repository.GetAll();

        public async Task<TEntity> Update(TEntity entity) => await _repository.Update(entity);

        public async Task<bool> Remove(Guid entityId) => await _repository.Remove(entityId);
    }
}
