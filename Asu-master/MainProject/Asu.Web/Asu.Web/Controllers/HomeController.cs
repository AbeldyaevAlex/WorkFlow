using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Asu.Core;
using Asu.Core.Domain.Tasks;
using Asu.Core.Data;
using Asu.Core.Domain.Customers;

namespace Asu.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<UsersTasks> _UsersTasksRepository;
        

        public HomeController(IWorkContext workContext, IRepository<UsersTasks> UsersTasksRepository)
        {
            _workContext = workContext;
            _UsersTasksRepository = UsersTasksRepository;
        }


        public string GetFullName(string fullName)
        {
            var FIO = fullName.Split(new char[] { ' ' }, 3);
            string full_name = FIO[0] + ' ' + FIO[1].Substring(0, 1) + '.' + FIO[2].Substring(0, 1) + '.';
            return full_name;
        }

        public ActionResult Index()
        {
            IEnumerable<UsersTasks> qwery = _workContext.CurrentCustomer.UsersTask.Where(x => x.IdRoditel == null);
            var FullUserName = _workContext.CurrentCustomer.ShippingAddress.LastName + ' ' +_workContext.CurrentCustomer.ShippingAddress.FirstName + ' ' + _workContext.CurrentCustomer.ShippingAddress.MiddleName;
            TempData["UserTask"] = GetFullName(FullUserName);
            return View(qwery);
        }
    }
}