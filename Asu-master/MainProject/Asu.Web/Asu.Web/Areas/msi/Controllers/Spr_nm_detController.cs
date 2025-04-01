using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Spr_nm_detController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult NmDetGridViewPartial()
        {
            var model = db.Spr_nmdet;
            return PartialView("~/Areas/msi/Views/Spr_nm_det/_NmDetGridViewPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult NmDetGridViewPartialAddNew(Asu.Web.Models.Spr_nmdet item)
        {
            var model = db.Spr_nmdet;
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
            return PartialView("~/Areas/msi/Views/Spr_nm_det/_NmDetGridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult NmDetGridViewPartialUpdate(Asu.Web.Models.Spr_nmdet item)
        {
            var model = db.Spr_nmdet;
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
            return PartialView("~/Areas/msi/Views/Spr_nm_det/_NmDetGridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult NmDetGridViewPartialDelete(System.Int64 Id)
        {
            var model = db.Spr_nmdet;
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
            return PartialView("~/Areas/msi/Views/Spr_nm_det/_NmDetGridViewPartial.cshtml", model.ToList());
        }
    }
}