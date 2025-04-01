using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.mmn.Controllers
{
    public class Spr_metodicController : Controller
    {
        // GET: mmn/Spr_metodic
        public ActionResult Index()
        {
            return View();
        }

        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView16Partial()
        {
            var model = db.Spr_METODIC;
            return PartialView("_GridView16Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView16PartialAddNew(Asu.Web.Models.Spr_METODIC item)
        {
            var model = db.Spr_METODIC;
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
            return PartialView("_GridView16Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView16PartialUpdate(Asu.Web.Models.Spr_METODIC item)
        {
            var model = db.Spr_METODIC;
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
            return PartialView("_GridView16Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView16PartialDelete(System.Int64 Id)
        {
            var model = db.Spr_METODIC;
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
            return PartialView("_GridView16Partial", model.ToList());
        }
    }
}