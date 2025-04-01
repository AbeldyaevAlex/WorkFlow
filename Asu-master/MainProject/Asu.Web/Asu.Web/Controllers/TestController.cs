using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            using (ASU_AVIAEntities11 db = new ASU_AVIAEntities11())
            {
                list = (from d in db.Spr_tem
                        select new SelectListItem
                        {
                            Value = d.Id.ToString(),
                            Text = d.nm_tem_p
                        }).ToList();
            }
            return View(list);
        }
        [HttpGet]
        public JsonResult Tema(int tema)
        {
            List<ElementJsonIntKey> la = new List<ElementJsonIntKey>();
            using (ASU_AVIAEntities11 db = new ASU_AVIAEntities11())
            {
                la = (from d in db.Spr_Perizd
                      where d.link_tema == tema
                      select new ElementJsonIntKey
                      {
                          Value = d.Id,
                          Text = d.nm_izd
                      }
                      ).ToList();
            }
            return Json(la, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Tema(Spr_tem tema)
        {
            return Json(tema);
        }
    }

    public class ElementJsonIntKey
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
