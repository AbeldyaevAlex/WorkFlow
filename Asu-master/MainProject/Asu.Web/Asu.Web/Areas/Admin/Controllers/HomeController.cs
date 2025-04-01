using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Asu.Core;
using Asu.Data;
using Asu.Services.UsersTasks;
using Asu.Web.Models;
using Asu.Web.Models.ContextDb;

namespace Asu.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        const string NaimTask = "Администрирование";
        private readonly IWorkContext _workContext;
        private readonly IUserTaskService _userTaskService;
        
        public HomeController(IWorkContext workContext, IUserTaskService userTaskService)
        {
            _workContext = workContext;
            _userTaskService = userTaskService;
        }       
        public ActionResult Index()
        {                     
            var qwery = _workContext.CurrentCustomer.UsersTask.Where(x => x.IdRoditel == _userTaskService.GetSubTaskId(NaimTask));
            return View(qwery);
        }
    }
}