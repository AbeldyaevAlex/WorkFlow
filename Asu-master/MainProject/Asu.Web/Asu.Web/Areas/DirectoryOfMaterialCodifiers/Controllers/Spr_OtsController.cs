using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers
{
    public class Spr_OtsController : Controller
    {
        // GET: DirectoryOfMaterialCodifiers/Spr_Ots
        public ActionResult Index()
        {
            return View();
        }

        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView12Partial()
        {
            var model = db.SPR_OTS;
            return PartialView("~/Areas/DirectoryOfMaterialCodifiers/Views/Spr_Ots/_GridView12Partial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView12PartialAddNew(Asu.Web.Models.SPR_OTS item)
        {
            var model = db.SPR_OTS;
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
            return PartialView("~/Areas/DirectoryOfMaterialCodifiers/Views/Spr_Ots/_GridView12Partial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView12PartialUpdate(Asu.Web.Models.SPR_OTS item)
        {
            var model = db.SPR_OTS;
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
            return PartialView("~/Areas/DirectoryOfMaterialCodifiers/Views/Spr_Ots/_GridView12Partial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView12PartialDelete(System.Int64 Id)
        {
            var model = db.SPR_OTS;
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
            return PartialView("~/Areas/DirectoryOfMaterialCodifiers/Views/Spr_Ots/_GridView12Partial.cshtml", model.ToList());
        }
    }
}