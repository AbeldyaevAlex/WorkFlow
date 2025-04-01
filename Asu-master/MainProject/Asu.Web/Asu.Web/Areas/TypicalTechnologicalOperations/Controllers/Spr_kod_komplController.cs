using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.TypicalTechnologicalOperations.Controllers
{
    public class Spr_kod_komplController : Controller
    { 
        public ActionResult Index()
        {
            return View();
        }
        ASU_AVIAEntities12 db1 = new ASU_AVIAEntities12();
        [ValidateInput(false)]
        public ActionResult GridViewKodKomp(string link_kod_tto)
        {
            int Id_link_kod_tto;
            int.TryParse(link_kod_tto, out Id_link_kod_tto);
            var model = db1.Spr_tto.Where(i => i.link_kod_TTO == Id_link_kod_tto).ToList();

            var model1 = db1.Spr_tto.ToList();
            return PartialView("_GridView_Kod_Komp", model1);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(Asu.Web.Models.Spr_tto item)
        {
            var model = db1.Spr_tto;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView_Kod_Komp", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(Asu.Web.Models.Spr_tto item)
        {
            var model = db1.Spr_tto;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db1.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView_Kod_Komp", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int64 Id)
        {
            var model = db1.Spr_tto;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView_Kod_Komp", model);
        }
    }
}