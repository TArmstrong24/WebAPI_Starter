using System;
using System.Linq;
using System.Linq.Expressions;

namespace StarterAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        T Add(T item);
        bool Update(T item);
        bool Delete(T item);

        T GetByID(string id);
        bool DeleteByID(string id);

        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> orderBy);
        IQueryable<T> GetAll(int pageIndex, int pageSize);
        IQueryable<T> GetAll(int pageIndex, int pageSize, Expression<Func<T, bool>> orderBy);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, int count);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> orderBy);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize,
            Expression<Func<T, bool>> orderBy);


        IQueryable<T> Find(string predicate);
        IQueryable<T> Find(string predicate, int count);
        IQueryable<T> Find(string predicate, Expression<Func<T, bool>> orderBy);
        IQueryable<T> Find(string predicate, int pageIndex, int pageSize,
            Expression<Func<T, bool>> orderBy);
    }
}
