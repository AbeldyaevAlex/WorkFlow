using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_materController : Controller
    {
        // GET: DirectoryOfMaterialCodifiers/Spr_mater
        public ActionResult Index()
        {
            return View();
        }

        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView4Partial()
        {
            var model = db.Spr_mater;
            return PartialView("_GridView4Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialAddNew(Asu.Web.Models.Spr_mater item)
        {
            var model = db.Spr_mater;
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
            return PartialView("_GridView4Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialUpdate(Asu.Web.Models.Spr_mater item)
        {
            var model = db.Spr_mater;
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
            return PartialView("_GridView4Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialDelete(System.Int64 Id)
        {
            var model = db.Spr_mater;
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
            return PartialView("_GridView4Partial", model.ToList());
        }
    }
}