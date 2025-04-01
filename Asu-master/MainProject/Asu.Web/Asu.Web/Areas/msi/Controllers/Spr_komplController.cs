using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Spr_komplController : Controller
    {
        // GET: msi/Spr_kompl
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView26Partial()
        {
            var model = db.Spr_kompl;
            return PartialView("_GridView26Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView26PartialAddNew(Asu.Web.Models.Spr_kompl item)
        {
            var model = db.Spr_kompl;
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
            return PartialView("_GridView26Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView26PartialUpdate(Asu.Web.Models.Spr_kompl item)
        {
            var model = db.Spr_kompl;
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
            return PartialView("_GridView26Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView26PartialDelete(System.Int64 Id)
        {
            var model = db.Spr_kompl;
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
            return PartialView("_GridView26Partial", model.ToList());
        }
    }
}