using Asu.Core;
using Asu.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Asu.Web.Areas.msi.Controllers
{
    public class CustomerApiController : ApiController
    {
        public readonly IWorkContext _workContext;
        public CustomerApiController(IWorkContext workContext)
        {
            _workContext = workContext;
        }
        public IHttpActionResult GetCurrentCustomer()
        {
            var cuCustomer = _workContext.CurrentCustomer;
            if (cuCustomer != null && !cuCustomer.IsGuest())
            {
                var userName = cuCustomer.Username;
                return Ok(userName);
            }
            return Unauthorized();
        }
    }
}
