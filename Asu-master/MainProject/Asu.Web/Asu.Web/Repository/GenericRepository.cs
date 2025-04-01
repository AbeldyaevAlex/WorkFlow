using Asu.Web.Models;
using Asu.Web.Models.ContextDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Asu.Web.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AsuAviaDbContext _context = null;

        private DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new AsuAviaDbContext();
            table = _context.Set<T>();
        }
        public void Add(T obj)
        {
            table.Add(obj);
        }

        public void Delete(T obj)
        {
            table.Remove(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object Id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}