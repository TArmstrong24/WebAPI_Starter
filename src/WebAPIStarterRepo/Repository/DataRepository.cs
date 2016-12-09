using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace StarterAPI.Repository
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entitySet;

        public DataRepository(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
            
            _entitySet = _context.Set<T>();
        }

        public T Add(T item)
        {
            _entitySet.Add(item);
            _context.SaveChanges();
            return item;
        }
        public bool Update(T item)
        {
            _entitySet.Attach(item);
            _context.SaveChanges();
            return true;
        }
        public bool Delete(T item)
        {
            _entitySet.Attach(item);
            _entitySet.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public T GetByID(int id)
        {
            return _entitySet.Find(id);
        }
        public bool DeleteByID(int id)
        {
            var item = _entitySet.Find(id);
            _entitySet.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return _entitySet;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> orderBy)
        {
            return _entitySet.OrderBy(orderBy);
        }
        public IQueryable<T> GetAll(int pageIndex, int pageSize)
        {
            return _entitySet.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<T> GetAll(int pageIndex, int pageSize, Expression<Func<T, bool>> orderBy)
        {
            return _entitySet.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(orderBy);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entitySet.Where(predicate);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, int count)
        {
            return _entitySet.Where(predicate).Take(count);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> orderby)
        {
            return _entitySet.Where(predicate).OrderBy(orderby);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, Expression<Func<T, bool>> orderby)
        {
            return _entitySet.Where(predicate).OrderBy(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<T> Find(string predicate)
        {
            return _entitySet.Where(predicate);
        }
        public IQueryable<T> Find(string predicate, int count)
        {
            return _entitySet.Where(predicate).Take(count);
        }
        public IQueryable<T> Find(string predicate, Expression<Func<T, bool>> orderby)
        {
            return _entitySet.Where(predicate).OrderBy(orderby);
        }
        public IQueryable<T> Find(string predicate, int pageIndex, int pageSize, Expression<Func<T, bool>> orderby)
        {
            return _entitySet.Where(predicate).OrderBy(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

    }
}
