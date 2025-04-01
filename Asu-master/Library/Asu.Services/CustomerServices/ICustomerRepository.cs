using Asu.Core.CustomerAsu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.CustomerServices
{
    public interface ICustomerRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        T FindByLogin(string login);
        //void Add(T entity);
        //void Delete(T entity);
        //void Update(T entity);
    }
}
