using Asu.Core;
using Asu.Services.UsersTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    [Authorize]
    public class MainSostavController : Controller
    {
        const string NaimTask = "Состав Изделия";
        private readonly IWorkContext _workContext;
        private readonly IUserTaskService _userTaskService;
        public MainSostavController(IWorkContext workContext, IUserTaskService userTaskService)
        {
            _workContext = workContext;
            _userTaskService = userTaskService;
        }
        public ActionResult GetSubSostavIzdelia()
        {
            var qwery = _workContext.CurrentCustomer.UsersTask.Where(x => x.IdRoditel == _userTaskService.GetSubTaskId(NaimTask));
            return View(qwery);
        }
    }
}