using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity : class 
    {
        private readonly IdentityContext _context;
        private DbSet<TEntity> _entities;
 
        public Repository(IdentityContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        /// <summary>
        ///     Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;
 
        /// <summary>
        ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
        ///     operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
 
        /// <summary>
        ///     Entities
        /// </summary>
        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());
        public TEntity Find(params object[] keyValues)
        {
            return _entities.Find(keyValues);
        }

        public IQueryable<TEntity> GetList()
        {
            return Table;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _entities.FindAsync(keyValues);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.FirstOrDefaultAsync(filter);
        }

        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Where(filter).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.Where(filter).ToListAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities); 
            _context.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
            
            /*Task.Run(() =>
            {
                _entities.Update(entity);
                _context.SaveChangesAsync();
                return entity;
            });*/
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            _context.SaveChangesAsync();
        }

        public void Delete(object id)
        {
            var entity = this.Find(id);
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(object id)
        {
            var entity = await this.FindAsync(id);
            _entities.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _entities.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetSql(string sql, params object[] parameters)
        {
            return _entities.FromSqlRaw(sql,parameters);
        }
    }
}
