using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region GetData
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> GetList();
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter);
        #endregion
        
        #region Data Action Medhods
        /// <summary>
        /// Veri/Verileri Kaydetme
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        void InsertAsync(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Veri/Verileri Güncelleme
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        void UpdateAsync(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Veri/Verileri Silme
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        void DeleteAsync(object id);
        void Delete(TEntity entity);
        void DeleteAsync(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        #endregion

        #region SqlQuery
        /// <summary>
        /// Sql sorgusu ile veri çekme methodu
        /// </summary>
        /// <param name="sql">sql sorgusu</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetSql(string sql, params object[] parameters);
        #endregion
    }
}
