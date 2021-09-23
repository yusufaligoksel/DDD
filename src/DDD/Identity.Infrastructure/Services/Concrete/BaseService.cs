using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;

namespace Identity.Infrastructure.Services.Concrete
{
    public class BaseService<TEntity>:IBaseService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public TEntity Find(object id)
        {
            return _repository.Find(id);
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await _repository.FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.FindAsync(filter);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.GetWhere(filter);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.GetWhereAsync(filter);
        }

        public TEntity Insert(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _repository.Insert(entities);
        }

        public TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _repository.Update(entities);
        }

        public IEnumerable<TEntity> GetSql(string sql, params object[] parameters)
        {
            return _repository.GetSql(sql, parameters);
        }
    }
}