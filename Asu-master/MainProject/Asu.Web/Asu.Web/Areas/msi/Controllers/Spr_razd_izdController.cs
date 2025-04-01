using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi
{
    public class Spr_razd_izdController : Controller
    {
        // GET: msi/Spr_razd_izd
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView23Partial()
        {
            var model = db.Spr_Razd_Izd;
            return PartialView("~/Areas/msi/Views/Spr_razd_izd/_GridView23Partial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView23PartialAddNew(Asu.Web.Models.Spr_Razd_Izd item)
        {
            var model = db.Spr_Razd_Izd;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Areas/msi/Views/Spr_razd_izd/_GridView23Partial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView23PartialUpdate(Asu.Web.Models.Spr_Razd_Izd item)
        {
            var model = db.Spr_Razd_Izd;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Areas/msi/Views/Spr_razd_izd/_GridView23Partial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView23PartialDelete(System.Int64 Id)
        {
            var model = db.Spr_Razd_Izd;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Areas/msi/Views/Spr_razd_izd/_GridView23Partial.cshtml", model.ToList());
        }
    }
}