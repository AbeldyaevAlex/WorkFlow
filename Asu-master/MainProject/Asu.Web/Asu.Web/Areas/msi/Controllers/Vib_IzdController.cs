using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Vib_IzdController : Controller
    {
        Asu.Web.Models.ASU_AVIAEntities12 db777 = new Asu.Web.Models.ASU_AVIAEntities12();
        public ActionResult Index()
        {
            using (var context = new ASU_AVIAEntities12())
            {
                var result = (from izd in context.Spr_Perizd
                              select izd).ToList();
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult Index(Spr_Perizd item)
        {
            //return View();
            return RedirectToAction("Get_Sostav_Izdelia", "Sostav_Izdelia", "msi_default");
        }
    }
}