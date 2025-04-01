using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_Nm_MaterController : Controller
    {
        // GET: DirectoryOfMaterialCodifiers/Spr_Nm_Mater
        public ActionResult Index()
        {
            return View();
        }

        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView8Partial()
        {
            var model = db.Nm_mater;
            return PartialView("_GridView8Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView8PartialAddNew(Asu.Web.Models.Nm_mater item)
        {
            var model = db.Nm_mater;
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
            return PartialView("_GridView8Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView8PartialUpdate(Asu.Web.Models.Nm_mater item)
        {
            var model = db.Nm_mater;
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
            return PartialView("_GridView8Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView8PartialDelete(System.Int64 Id)
        {
            var model = db.Nm_mater;
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
            return PartialView("_GridView8Partial", model.ToList());
        }
    }
}