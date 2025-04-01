using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class SprOboznController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ASU_AVIAEntities12())
            {
                var result = (from izd in context.Spr_Perizd
                              select izd).ToList();
                return View(result);
            }
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.Spr_obozn;
            return PartialView("_GridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Asu.Web.Models.Spr_obozn item)
        {
            var model = db.Spr_obozn;
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
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Asu.Web.Models.Spr_obozn item)
        {
            var model = db.Spr_obozn;
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
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int64 Id)
        {
            var model = db.Spr_obozn;
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
            return PartialView("_GridViewPartial", model.ToList());
        }
        public ActionResult GridViewCustomActionPartial(string customAction, string customArg)
        {
            int id;
            int.TryParse(customAction, out id);

            if (customArg == "delete")
            {              
                var model = db.Spr_obozn;
                if (id >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.Id == id);
                        if (item != null)
                           model.Remove(item);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                return PartialView("~/Areas/msi/Views/SprObozn/_GridViewPartial.cshtml", model.ToList());
            }
            if (customArg == "specification")
            {
                    return RedirectToAction("Index", "SprSpecif", new { ID = id});
            }
            if (customArg == "izdelie")
            {
                return RedirectToAction("Index", "Vib_Izd", "msi_default");
            }
            if (customArg == "editRecord")
            {
                var model = db.Spr_obozn;

                    var modelItem = model.FirstOrDefault(it => it.Id == id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }                                     
                return PartialView("_GridViewPartial", model.ToList());
            }
            return View();
        }
    }
}