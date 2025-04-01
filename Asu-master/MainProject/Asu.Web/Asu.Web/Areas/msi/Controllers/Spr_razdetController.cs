using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi
{
    public class Spr_razdetController : Controller
    {
        // GET: msi/Spr_razdet
        public ActionResult Index()
        {
            return View();
        }

        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView24Partial()
        {
            var model = db.Raz_det;
            return PartialView("~/Areas/msi/Views/Spr_razdet/_GridView24Partial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView24PartialAddNew(Raz_det item)
        {
            var model = db.Raz_det;
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
            return PartialView("~/Areas/msi/Views/Spr_razdet/_GridView24Partial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView24PartialUpdate(Raz_det item)
        {
            var model = db.Raz_det;
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
            return PartialView("~/Areas/msi/Views/Spr_razdet/_GridView24Partial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView24PartialDelete(System.Int64 Id)
        {
            var model = db.Raz_det;
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
            return PartialView("~/Areas/msi/Views/Spr_razdet/_GridView24Partial.cshtml", model.ToList());
        }
    }
}