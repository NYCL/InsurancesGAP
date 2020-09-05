using InsurancesGAP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InsurancesGAP.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DataContext _context;
        internal DbSet<TEntity> _dbSet;

        public EFRepository(DataContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null && includeProperties.Count() > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity Get(params object[] id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        public TEntity Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            return entityToDelete;
        }
        public virtual TEntity Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            return _dbSet.Remove(entityToDelete).Entity;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
