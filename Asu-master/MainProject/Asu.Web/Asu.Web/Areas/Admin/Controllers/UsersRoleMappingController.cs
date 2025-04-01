using Asu.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Asu.Core;
using Asu.Services.Customers;
using Asu.Core.Domain.Customers;
using Asu.Core.Data;


namespace Asu.Web.Areas.Admin.Controllers
{
    public class UsersRoleMappingController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        public UsersRoleMappingController(
               IWorkContext workContext,
               ICustomerService customerService,
               IRepository<Customer> customerRepository,
               IRepository<CustomerRole> customerRoleRepository)
        {
            _workContext = workContext;
            _customerService = customerService;
            _customerRepository = customerRepository;
            _customerRoleRepository = customerRoleRepository;
        }

        //public ActionResult CreateUserRoleMapping()
        //{
        //    List<AspNetRolesViewModel> listRoles = new List<AspNetRolesViewModel>();
        //    var model = new AspNetUserRolesViewModel();
        //    model.UserInfo = _customerService.GetAllCustomers().Where(x => x.Active == true && x.BillingAddress != null).ToList();
        //    model.RoleInfo = _customerService.GetAllRoles();
        //    return View(model);
        //}

        [HttpPost]
        public ActionResult CreateUserRoleMapping(IEnumerable<string> arrayRoleId, int CustomerId)
        {
            var customer = _customerService.GetCustomerById(CustomerId);

            foreach (var item in arrayRoleId)
            {              
                if (!customer.CustomerRoles.Contains(_customerService.GetCustomerRoleBySystemName(item)))
                {
                    var customerRole = _customerService.GetCustomerRoleBySystemName(item);
                    customer.CustomerRoles.Add(customerRole);
                }
            }
            _customerRepository.Update(customer);
            return RedirectToAction("CreateUserRoleMapping");
        }
    }
}