using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class skm_testController : Controller
    {
        // GET: skm_test
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            //var model = db.Spr_skm.Where(p => p.Id <= 100);
            //var model = db.Spr_skm;
            return PartialView("_GridViewPartial");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Asu.Web.Models.Spr_skm item)
        {
            var model = db.Spr_skm;
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
        public ActionResult GridViewPartialUpdate(Asu.Web.Models.Spr_skm item)
        {
            var model = db.Spr_skm;
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
        public ActionResult GridViewPartialDelete(System.Int64 Id)
        {
            var model = db.Spr_skm;
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