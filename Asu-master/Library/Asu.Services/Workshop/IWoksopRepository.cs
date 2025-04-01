using System.Collections.Generic;

namespace Asu.Services.Workshop
{
    public interface IWoksopRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
