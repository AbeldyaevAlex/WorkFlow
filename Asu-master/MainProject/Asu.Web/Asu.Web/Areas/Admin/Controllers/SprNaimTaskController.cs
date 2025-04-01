using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.Admin.Controllers
{
    public class SprNaimTaskController : Controller
    {
        // GET: Admin/SprNaimTask
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult TreeListPartial()
        {
            var model = db.Spr_nm_task;
            return PartialView("_TreeListPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialAddNew(Asu.Web.Models.Spr_nm_task item, HttpPostedFileBase Images_Path)
        {
            if (ModelState.IsValid && Images_Path != null)
            {
                byte[] imageData = null;
                using (var binayReader = new BinaryReader(Images_Path.InputStream))
                {
                    imageData = binayReader.ReadBytes(Images_Path.ContentLength);
                }
                item.screen = imageData;
                db.Spr_nm_task.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", "Admin_default");
            }

            ViewBag.Id_Roditel = new SelectList(db.Spr_nm_task, "Id", "Task", item.Id_Roditel);
            return View(item);

            //var model = db.Spr_nm_task;
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        model.Add(item);
            //        db.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        ViewData["EditError"] = e.Message;
            //    }
            //}
            //else
            //    ViewData["EditError"] = "Please, correct all errors.";
            //return PartialView("_TreeListPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialUpdate(Asu.Web.Models.Spr_nm_task item)
        {
            var model = db.Spr_nm_task;
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
            return PartialView("_TreeListPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialDelete(System.Int32 Id)
        {
            var model = db.Spr_nm_task;
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
            return PartialView("_TreeListPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialMove(System.Int32 Id, System.Int32? Id_Roditel)
        {
            var model = db.Spr_nm_task;
            try
            {
                var item = model.FirstOrDefault(it => it.Id == Id);
                if (item != null)
                    item.Id_Roditel = Id_Roditel;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("_TreeListPartial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db1 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult CardViewPartial()
        {
            var model = db1.Spr_nm_task;
            return PartialView("_CardViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CardViewPartialAddNew(Asu.Web.Models.Spr_nm_task item)
        {
            var model = db1.Spr_nm_task;
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
            return PartialView("_CardViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CardViewPartialUpdate(Asu.Web.Models.Spr_nm_task item)
        {
            var model = db1.Spr_nm_task;
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
            return PartialView("_CardViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CardViewPartialDelete(System.Int32 Id)
        {
            var model = db1.Spr_nm_task;
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
            return PartialView("_CardViewPartial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db2 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult DataViewPartial()
        {
            var model = db2.Spr_nm_task;
            return PartialView("_DataViewPartial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db3 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridLookupPartial()
        {
            var model = db3.Spr_nm_task;
            return PartialView("_GridLookupPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridLookupPartialAddNew(Asu.Web.Models.Spr_nm_task item)
        {
            var model = db3.Spr_nm_task;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db3.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridLookupPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridLookupPartialUpdate(Asu.Web.Models.Spr_nm_task item)
        {
            var model = db3.Spr_nm_task;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db3.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridLookupPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridLookupPartialDelete(System.Int32 Id)
        {
            var model = db3.Spr_nm_task;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db3.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridLookupPartial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db4 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult PivotGridPartial()
        {
            var model = db4.Spr_nm_task;
            return PartialView("_PivotGridPartial", model.ToList());
        }
    }
}