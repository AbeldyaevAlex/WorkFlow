using Asu.Web.Models.ContextDb;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_cen_matController : Controller
    {
        public ActionResult Index(string cena)
        {
            int Id_Cena;
            int.TryParse(cena, out Id_Cena);            
            ViewData["cena_mater"] = Id_Cena;          
            return View();
        }

        AsuAviaDbContext db = new AsuAviaDbContext();

        [ValidateInput(false)]
        public ActionResult GridViewPartial(string cena)
        {
            int Id_Cena;
            int.TryParse(cena, out Id_Cena);           
            ViewData["cena_mater"] = Id_Cena;
            var list_cen = db.Spr_cen_mater.Where(x => x.link_SKM == Id_Cena).ToList();
            return PartialView("_GridViewPartial", list_cen.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Models.DirectoryOfMaterialCodifiers.Spr_cen_mater item)
        {
            var model = db.Spr_cen_mater;
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
            return PartialView("_GridViewPartial");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Models.DirectoryOfMaterialCodifiers.Spr_cen_mater item)
        {
            var model = db.Spr_cen_mater;
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
            return PartialView("_GridViewPartial");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(long Id)
        {
            var model = db.Spr_cen_mater;
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
            return PartialView("_GridViewPartial");
        }
    }
}