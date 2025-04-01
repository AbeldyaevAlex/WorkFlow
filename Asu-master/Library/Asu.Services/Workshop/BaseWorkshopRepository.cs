using Asu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Workshop
{
    public class BaseWorkshopRepository<T> : IWoksopRepository<T> where T : class
    {
        private readonly WorkContext _db;

        public BaseWorkshopRepository()
        {
            _db = new WorkContext();
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
