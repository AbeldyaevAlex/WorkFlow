using Asu.Core.CustomerAsu;
using Asu.Data;
using Asu.Services.Workshop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.CustomerServices
{
    public class BaseCustomerReposirory<T> : ICustomerRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseCustomerReposirory()
        {
            _context = new ApplicationDbContext();
        }

        public T Get(string id)
        {
            return _context.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public T FindByLogin(string login)
        {
            return _context.Set<T>().First();

        }
    }
}
