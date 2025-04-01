using System.Collections.Generic;
using Asu.Core.Domain.Customers;

namespace Asu.Web.Models
{
    public class AspNetUserRolesViewModel
    {
        public List<Core.Domain.Customers.Customer> UserInfo { get; set; }
        public List<CustomerRole> RoleInfo { get; set; }
    }
}