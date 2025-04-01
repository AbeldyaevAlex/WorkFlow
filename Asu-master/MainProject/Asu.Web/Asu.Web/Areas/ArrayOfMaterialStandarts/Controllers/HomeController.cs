using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.mmn.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            //IEnumerable<Spr_nm_task> qwery = GetSubTask();
            //return View(qwery);
            return View("Index");
        }
        public IEnumerable<Spr_nm_task> GetSubTask()
        {
            Asu.Web.Models.ASU_AVIAEntities12 context = new Asu.Web.Models.ASU_AVIAEntities12();
            var subtask = (from nm_task in context.Spr_nm_task
                           join tasks in context.Tasks
                           on nm_task.Id_Roditel equals tasks.link_nm_task
                           select nm_task).ToList();
            return subtask;
        }
    }
}