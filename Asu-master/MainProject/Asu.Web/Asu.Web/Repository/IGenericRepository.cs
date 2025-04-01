using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Web.Repository
{
    interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        void Add(T obj);
        void Update(T obj);
        void Delete(T Id);
        void Save();
    }
}
