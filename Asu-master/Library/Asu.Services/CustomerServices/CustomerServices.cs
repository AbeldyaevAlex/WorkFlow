using Asu.Core.CustomerAsu;
using Asu.Services.CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.CustomerServices
{
    public class CustomerServices
    {
        private ICustomerRepository<ApplicationUser> _customerRepository;
        public CustomerServices(ICustomerRepository<ApplicationUser> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ApplicationUser GetById(string id)
        {
            return _customerRepository.Get(id);
        }
        public List<ApplicationUser> GetAll()
        {
            return _customerRepository.GetAll().ToList();
        }
        public ApplicationUser FindByLogin(string login)
        {
            return _customerRepository.FindByLogin(login);
        }
        public virtual ApplicationUser GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _customerRepository.GetAll()
                        orderby c.Id
                        where c.Email == username
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }
    }
}
