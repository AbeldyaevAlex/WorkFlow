using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_eizmController : Controller
    {
        // GET: DirectoryOfMaterialCodifiers/Spr_eizm
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView5Partial()
        {
            var model = db.Spr_eizm;
            return PartialView("_GridView5Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialAddNew(Asu.Web.Models.Spr_eizm item)
        {
            var model = db.Spr_eizm;
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
            return PartialView("_GridView5Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialUpdate(Asu.Web.Models.Spr_eizm item)
        {
            var model = db.Spr_eizm;
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
            return PartialView("_GridView5Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialDelete(System.Int64 Id)
        {
            var model = db.Spr_eizm;
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
            return PartialView("_GridView5Partial", model.ToList());
        }
    }
}