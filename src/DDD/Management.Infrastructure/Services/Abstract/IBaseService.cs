using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services.Abstract
{
    public interface IBaseService<TEntity> where TEntity : class 
    {
        //find
        TEntity Find(object id);
        Task<TEntity> FindAsync(object id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        
        //getList
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter);
        
        //insert
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        
        //update
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        
        //delete
        Task<int> DeleteAsync(object id);
        Task<int> DeleteAsync(TEntity entity);
    }
}