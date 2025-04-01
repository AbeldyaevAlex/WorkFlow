using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class PopupsaveChangeController : Controller
    {
        // GET: msi/PopupsaveChange
        public ActionResult Index()
        {
            ViewData["msg"] = "777";
            return View();
        }
    }
}