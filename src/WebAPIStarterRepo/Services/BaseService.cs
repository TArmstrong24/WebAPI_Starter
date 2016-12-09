using StarterAPI.Models;
using StarterAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarterAPI.Services
{
    public class BaseService<T> where T : class
    {
        protected IRepository<T> _repository;
        public BaseService(IRepository<T> repository)
        {
            this._repository = repository;
        }
        public T Add(T item)
        {
            return _repository.Add(item);
        }
        public bool Update(T item)
        {
            return _repository.Update(item);
        }
        public bool Delete(T item)
        {
            return _repository.Delete(item);
        }
        public T GetByID(int id)
        {
            return _repository.GetByID(id);
        }
        public bool DeleteByID(int id)
        {
            return _repository.DeleteByID(id);
        }
        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> orderBy)
        {
            return _repository.GetAll(orderBy);
        }
        public IQueryable<T> GetAll(int pageIndex, int pageSize)
        {
            return _repository.GetAll(pageIndex, pageSize);
        }
        public IQueryable<T> GetAll(int pageIndex, int pageSize, Expression<Func<T, bool>> orderBy)
        {
            return _repository.GetAll(pageIndex, pageSize, orderBy);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _repository.Find(predicate);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, int count)
        {
            return _repository.Find(predicate, count);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> orderby)
        {
            return _repository.Find(predicate, orderby);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize,
            Expression<Func<T, bool>> orderBy)
        {
            return _repository.Find(predicate, pageIndex, pageSize, orderBy);
        }
        public IQueryable<T> Find(string predicate)
        {
            return _repository.Find(predicate);
        }
        public IQueryable<T> Find(string predicate, int count)
        {
            return _repository.Find(predicate, count);
        }
        public IQueryable<T> Find(string predicate, Expression<Func<T, bool>> orderby)
        {
            return _repository.Find(predicate, orderby);
        }
        public IQueryable<T> Find(string predicate, int pageIndex, int pageSize,
            Expression<Func<T, bool>> orderBy)
        {
            return _repository.Find(predicate, pageIndex, pageSize, orderBy);
        }

    }
}
