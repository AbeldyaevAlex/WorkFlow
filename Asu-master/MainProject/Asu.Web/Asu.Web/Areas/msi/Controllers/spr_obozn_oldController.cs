using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Controllers;
using Asu.Web.Model;
using Asu.Web.Models;

namespace Asu.Web.Areas.msi.Controllers
{
    public class spr_obozn_oldController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //Asu.Web.Models.ASU_AVIAEntities7 db = new Asu.Web.Models.ASU_AVIAEntities7();
        //[ValidateInput(false)]
        //public ActionResult _sprOboznPartial()
        //{
        //    var model = db.Spr_obozn;
        //    return PartialView("__sprOboznPartial", model.ToList());
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult _sprOboznPartialAddNew(Asu.Web.Models.Spr_obozn item)
        //{
        //    var model = db.Spr_obozn;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            model.Add(item);
        //            db.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return PartialView("__sprOboznPartial", model.ToList());
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult _sprOboznPartialUpdate(Asu.Web.Models.Spr_obozn item)
        //{
        //    var model = db.Spr_obozn;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
        //            if (modelItem != null)
        //            {
        //                this.UpdateModel(modelItem);
        //                db.SaveChanges();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return PartialView("__sprOboznPartial", model.ToList());
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult GridViewCustomActionPartial(string customAction, string customArg)
        //{
        //    if (customArg == "delete")
        //    {
        //        int id;
        //        int.TryParse(customAction, out id);

        //        var model = db.Spr_cex;
        //        if (id >= 0)
        //        {
        //            try
        //            {
        //                var item = model.FirstOrDefault(it => it.Id == id);
        //                if (item != null)
        //                    model.Remove(item);
        //                db.SaveChanges();
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        return PartialView("~/Views/spr_cex/_GridViewPartial.cshtml", model.ToList());
        //    }
        //    if (customArg == "specification")
        //    {
        //        using (var context = new ASU_AVIAEntities7())
        //        {
        //            var result = (from specif in context.Spr_specif
        //                          join obozn in context.Spr_obozn
        //                          on specif.link_kts equals obozn.Id
        //                          select specif).ToList();
        //        }
        //    }
        //    return View();
        //}
        Asu.Web.Models.ASU_AVIAEntities12 db1 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db1.Spr_cex;
            return PartialView("~/Views/spr_obozn/_sprOboznPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Asu.Web.Models.Spr_obozn item)
        {
            var model = db1.Spr_obozn;
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
            return PartialView("~/Views/spr_obozn/_sprOboznPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Asu.Web.Models.Spr_obozn item)
        {
            var model = db1.Spr_obozn;
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
            return PartialView("~/Views/spr_cex/_GridViewPartial.cshtml", model.ToList());
        }
        public ActionResult GridViewCustomActionPartial(string customAction, string customArg)
        {
            if (customArg == "delete")
            {
                int id;
                int.TryParse(customAction, out id);

                var model = db1.Spr_cex;
                if (id >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.Id == id);
                        if (item != null)
                            model.Remove(item);
                        db1.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                return PartialView("~/Views/spr_cex/_GridViewPartial.cshtml", model.ToList());
            }
            if (customArg == "specification")
            {
                using (var context = new ASU_AVIAEntities12())
                {
                    var result = (from specif in context.Spr_specif
                                  join obozn in context.Spr_obozn
                                  on specif.link_kts equals obozn.Id
                                  select specif).ToList();
                }
            }
            return View();
        }
    }
}