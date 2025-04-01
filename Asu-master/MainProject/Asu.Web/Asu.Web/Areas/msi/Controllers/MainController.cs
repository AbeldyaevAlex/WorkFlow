using Asu.Web.Data;
using Asu.Web.Models.ContextDb;
using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Core;
using Asu.Services.UsersTasks;

namespace Asu.Web.Areas.msi.Controllers
{
    public class MainController : Controller
    {
        const string NaimTask = "Состав Изделия";
        private readonly IWorkContext _workContext;
        private readonly IUserTaskService _userTaskService;

        public MainController(IWorkContext workContext, IUserTaskService userTaskService)
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