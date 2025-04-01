using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_Mark_MaterController : Controller
    {
        // GET: DirectoryOfMaterialCodifiers/Spr_Mark_Mater
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView9Partial()
        {
            var model = db.Mark_mater;
            return PartialView("_GridView9Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView9PartialAddNew(Asu.Web.Models.Mark_mater item)
        {
            var model = db.Mark_mater;
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
            return PartialView("_GridView9Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView9PartialUpdate(Asu.Web.Models.Mark_mater item)
        {
            var model = db.Mark_mater;
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
            return PartialView("_GridView9Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView9PartialDelete(System.Int64 Id)
        {
            var model = db.Mark_mater;
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
            return PartialView("_GridView9Partial", model.ToList());
        }
    }
}