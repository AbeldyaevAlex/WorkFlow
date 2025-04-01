using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class Spr_specifController : Controller
    {
        // GET: Spr_specif
        public ActionResult Index()
        {
            Asu.Web.Models.ASU_AVIAEntities12 db777 = new Asu.Web.Models.ASU_AVIAEntities12();
            var period2 = db777.Spr_specif.OrderBy(x => x.Raz_det.sort).ToList();
            var period1 = db777.Spr_specif.Where(x => x.link_kts == 8).ToList();
            return View(period2);
            //using (var context = new ASU_AVIAEntities8())
            //{
            //    var result = (from specif in context.Spr_specif
            //                  join sort in context.Raz_det
            //                  on specif.link_razdizd equals sort.Id
            //                  where specif.link_kts == 8
            //                  orderby sort.sort
            //                  select specif).ToList();
            //    return View(result);
            //}
            //return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            //var model = db.Spr_specif;
            //return PartialView("_GridView1Partial", model.ToList());

            using (var context = new ASU_AVIAEntities12())
            {
                //var result = (from specif in context.Spr_specif
                //              join sort in context.Raz_det
                //              on specif.link_razdizd equals sort.Id
                //              orderby sort.sort
                //              select specif).ToList();
                var result = 1;
                return View(result);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(Asu.Web.Models.Spr_specif item)
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
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(Asu.Web.Models.Spr_specif item)
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
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int64 Id)
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
            return PartialView("_GridView1Partial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db1 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView2Partial()
        {
            var period2 = db1.Spr_specif.OrderBy(x => x.Raz_det.sort).ToList();
            //using (var context = new ASU_AVIAEntities8())
            //{
            //    var result = (from specif in context.Spr_specif
            //                  join sort in context.Raz_det
            //                  on specif.link_razdizd equals sort.Id
            //                  orderby sort.sort
            //                  select specif).ToList();
            //    return View(result);
            //}
            var model = db1.Spr_specif;
            return PartialView("_GridView2Partial", period2);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialAddNew(Asu.Web.Models.Spr_specif item)
        {
            var model = db1.Spr_specif;
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
            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialUpdate(Asu.Web.Models.Spr_specif item)
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
            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialDelete(System.Int64 Id)
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
            return PartialView("_GridView2Partial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db2 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult SpecificationGridViewPartial()
        {
            var model = db2.Spr_specif;
            return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecificationGridViewPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SpecificationGridViewPartialAddNew(Asu.Web.Models.Spr_specif item)
        {
            var model = db2.Spr_specif;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecificationGridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SpecificationGridViewPartialUpdate(Asu.Web.Models.Spr_specif item)
        {
            var model = db2.Spr_specif;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db2.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecificationGridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SpecificationGridViewPartialDelete(System.Int64 Id)
        {
            var model = db2.Spr_specif;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecificationGridViewPartial.cshtml", model.ToList());
        }
    }
}