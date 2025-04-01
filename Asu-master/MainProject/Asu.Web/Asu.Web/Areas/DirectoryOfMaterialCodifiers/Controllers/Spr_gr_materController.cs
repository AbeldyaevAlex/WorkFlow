using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_gr_materController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView3Partial()
        {
            var model = db.Spr_GR_Mater;
            return PartialView("_GridView3Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialAddNew(Asu.Web.Models.Spr_GR_Mater item)
        {
            var model = db.Spr_GR_Mater;
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
            return PartialView("_GridView3Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialUpdate(Asu.Web.Models.Spr_GR_Mater item)
        {
            var model = db.Spr_GR_Mater;
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
            return PartialView("_GridView3Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialDelete(System.Int32 Id)
        {
            var model = db.Spr_GR_Mater;
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
            return PartialView("_GridView3Partial", model.ToList());
        }
    }
}