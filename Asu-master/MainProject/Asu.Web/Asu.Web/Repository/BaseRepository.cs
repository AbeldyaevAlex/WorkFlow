using Asu.Web.Models.ContextDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly AsuAviaDbContext _db;

        public BaseRepository()
        {
            _db = new AsuAviaDbContext();
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}