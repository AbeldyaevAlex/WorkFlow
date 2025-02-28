using DomainCore;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTaskApplication.Controllers
{
    public class HomeController : Controller
    {
        ShopDBEntities2 db;
        public HomeController()
        {
            db = new ShopDBEntities2();
        }
        List<Order> order = new List<Order>();
        public ActionResult Index()
        {

            var ListBrigade = GetBrigadeList();
            var ListStatus = GetStatusList();
            var model = new TaskDomain { StatusList = ListStatus, BrigadeList = ListBrigade };
            ViewBag.Title = "Home Page";

            return View(model);
        }
        public ActionResult Report()
        {
            return View();
        }
        public IList<SelectListItem> GetBrigadeList()
        {
            var listRole = db.CustomerRole.ToList();
            return listRole.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }
        public IList<SelectListItem> GetStatusList()
        {
            var listStatus = db.StatusTask.ToList();
            return listStatus.Select(x => new SelectListItem { Text = x.Status, Value = x.Id.ToString() }).ToList();
        }
    }
}
