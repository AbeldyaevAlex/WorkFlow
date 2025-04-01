using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models;

namespace Asu.Web.Areas.msi.Controllers
{
    public class RazuzlovanieController : Controller
    {
        public ActionResult Razuzlov()
        {
            ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
            SelectList izd = new SelectList(db.Spr_Perizd, "Id", "nm_izd");
            ViewBag.Izd = izd;
            return View();
        }
        [HttpPost]
        public void Razuzlov(Spr_Perizd param)
        {

        }
    }
}