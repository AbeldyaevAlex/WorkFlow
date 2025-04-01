using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class SprSpecifController : Controller
    {
        public string ControllerName = "SprSpecifController";

        //public int? globalId;
        Asu.Web.Models.ASU_AVIAEntities12 db777 = new Asu.Web.Models.ASU_AVIAEntities12();
        public ActionResult Index(int? id)
        {
            var period2 = db777.Spr_specif.Where(x => x.link_kts == id).OrderBy(y => y.Raz_det.sort).ToList();
            var kts = db777.Spr_obozn.Where(x => x.Id == id).Select(j => j.obozn_p).ToList()[0];
            ViewData["KTS_NAIM"] = kts;
            Session["globalId"] = (long?)id;
            Session["period2"] = period2;
            ViewData["ConName"] = ControllerName;
            return View(period2);
        }
        Asu.Web.Models.ASU_AVIAEntities12 db1 = new Asu.Web.Models.ASU_AVIAEntities12();
        [ValidateInput(false)]
        public ActionResult GridViewPartial(string ControllerName)
        {
            ViewData["ConName"] = ControllerName;
            long? globalIp = (long)Session["globalId"];
            var model = db1.Spr_specif;
            var period2 = db777.Spr_specif.Where(x => x.link_kts == globalIp).OrderBy(y => y.Raz_det.sort).ToList();
            return PartialView("_GridViewPartialSprSpecif", period2);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Asu.Web.Models.Spr_specif item)
        {
            var model = db1.Spr_specif;
            //var model = db777.Spr_specif.Where(x => x.link_kts == item.link_kts).OrderBy(y => y.Raz_det.sort).ToList();
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
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            var model1 = db777.Spr_specif.Where(x => x.link_kts == item.link_kts).OrderBy(y => y.Raz_det.sort).ToList();
            return PartialView("_GridViewPartialSprSpecif", model1.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Asu.Web.Models.Spr_specif item)
        {
            var model = db1.Spr_specif;
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
            var view_specif = db777.Spr_specif.Where(x => x.link_kts == item.link_kts).OrderBy(y => y.Raz_det.sort).ToList();
            return PartialView("_GridViewPartialSprSpecif", view_specif.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int64 Id)
        {
            var model = db1.Spr_specif;
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
            return PartialView("_GridViewPartial", model.ToList());
        }
        public ActionResult GridViewCustomActionPartial(string customAction, string customArg)
        {
            int id;
            int.TryParse(customAction, out id);

            if (customArg == "delete")
            {
                var model = db777.Spr_obozn;
                if (id >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.Id == id);
                        if (item != null)
                            model.Remove(item);
                        db777.SaveChanges();
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
                return RedirectToAction("Index", "SprSpecif", new { ID = id });
            }
            if (customArg == "izdelie")
            {
                return RedirectToAction("Index", "Vib_Izd");
            }
            return View();
        }


        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridViewPartialSprSpecif()
        {
            var model = db.Spr_specif;
            return PartialView("_GridViewPartialSprSpecif", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialSprSpecifAddNew(Asu.Web.Models.Spr_specif item)
        {
            var model = db.Spr_specif;
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
            return PartialView("_GridViewPartialSprSpecif", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialSprSpecifUpdate(Asu.Web.Models.Spr_specif item)
        {
            var model = db.Spr_specif;
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
            return PartialView("_GridViewPartialSprSpecif", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialSprSpecifDelete(System.Int64 Id)
        {
            var model = db.Spr_specif;
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
            return PartialView("_GridViewPartialSprSpecif", model.ToList());
        }
    }
}