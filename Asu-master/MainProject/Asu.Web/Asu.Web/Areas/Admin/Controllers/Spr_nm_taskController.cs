using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models;
using System.IO;
using Asu.Web.Models.ContextDb;
using Asu.Core.Data;
using Asu.Core.Domain.Tasks;
using Asu.Core;
using Asu.Core.Domain.StatusDirectory;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Asu.Web.Areas.Admin.Controllers
{
    public class Spr_nm_taskController : Controller
    {
        private readonly IRepository<UsersTasks> _userTaskRepository;
        private readonly IRepository<DocumentStatus> _DocumentStatusRepository;
        private readonly IWorkContext _workContext;
        private AsuAviaDbContext db = new AsuAviaDbContext();

        public Spr_nm_taskController(IRepository<UsersTasks> userTaskRepository, IWorkContext workContext, IRepository<DocumentStatus> DocumentStatusRepository)
        {
            _userTaskRepository = userTaskRepository;
            _workContext = workContext;
            _DocumentStatusRepository = DocumentStatusRepository;
        }

        public ActionResult Index()
        {            
            return View();
        }       
        public ActionResult Create()
        {
            ViewBag.Id_Roditel = new SelectList(_userTaskRepository.Table, "Id", "NaimTask");
            //ViewBag.Id_Roditel = new SelectList(db.Spr_nm_task, "Id", "Task");
            //ViewBag.Status = new SelectList(db.Status_dok, "Id", "status");
            return View();
        }
        public ActionResult GetSubTask(string subTask)
        {
            int Id_SubTask;
            int.TryParse(subTask, out Id_SubTask);
            var sub = db.Spr_nm_task.Where(x => x.Id_Roditel == Id_SubTask).ToList();
            ViewData["Id_Sub_Task"] = subTask;
            return PartialView("_GridViewPartial", sub.ToList());
        }

        // POST: Admin/Spr_nm_task/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsersTasks img, HttpPostedFileBase Images_Path)
        {
            if (ModelState.IsValid && Images_Path != null)
            {
                byte[] imageData = null;
                using (var binayReader = new BinaryReader(Images_Path.InputStream))
                {
                    imageData = binayReader.ReadBytes(Images_Path.ContentLength);
                }
                img.Screen = imageData;
                img.CreatorId = _workContext.CurrentCustomer.Id;
                img.DocumentStatus = _DocumentStatusRepository.Table.Where(x => x.Id == 1).FirstOrDefault();
                if (img.IdRoditel == 0)
                {
                    img.IdRoditel = null;
                }
                _userTaskRepository.Insert(img);
                return RedirectToAction("Index", "Home", "Admin_default");
            }

            ViewBag.Id_Roditel = new SelectList(_userTaskRepository.Table, "Id", "Task", img.IdRoditel);
            return View(img);
        }

        // GET: Admin/Spr_nm_task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spr_nm_task = _userTaskRepository.GetById(id);
            if (spr_nm_task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Roditel = new SelectList(_userTaskRepository.Table, "Id", "NaimTask");
            return View(spr_nm_task);
        }

        // POST: Admin/Spr_nm_task/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsersTasks spr_nm_task, HttpPostedFileBase Images_Path)
        {
            if (ModelState.IsValid)
            {
                //if (Images_Path != null)
                //{
                //    byte[] imageData = null;
                //    using (var binayReader = new BinaryReader(Images_Path.InputStream))
                //    {
                //        imageData = binayReader.ReadBytes(Images_Path.ContentLength);
                //    }
                //    spr_nm_task.Screen = imageData;
                //}

                //spr_nm_task.CreatorId = _workContext.CurrentCustomer.Id;
                //spr_nm_task.DocumentStatus = _DocumentStatusRepository.Table.Where(x => x.Id == 1).FirstOrDefault();
                //if (spr_nm_task.IdRoditel.IsNullOrDefault() || spr_nm_task.IdRoditel == 0)
                //{
                //    spr_nm_task.IdRoditel = null;
                //}
                var task = _userTaskRepository.GetById(spr_nm_task.Id);
                task.ControllerName = spr_nm_task.ControllerName;
                task.ActionName = spr_nm_task.ActionName;
                task.RouteUrl = spr_nm_task.RouteUrl;

                _userTaskRepository.Update(task);
                return RedirectToAction("Index", "Home", "Admin_default");
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(spr_nm_task).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
           //ViewBag.Id_Roditel = new SelectList(db.Spr_nm_task, "Id", "Task", spr_nm_task.Id_Roditel);
            return View(spr_nm_task);
        }

        // GET: Admin/Spr_nm_task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.UsersTask.Spr_nm_task spr_nm_task = db.Spr_nm_task.Find(id);
            if (spr_nm_task == null)
            {
                return HttpNotFound();
            }
            return View(spr_nm_task);
        }

        // POST: Admin/Spr_nm_task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.UsersTask.Spr_nm_task spr_nm_task = db.Spr_nm_task.Find(id);
            db.Spr_nm_task.Remove(spr_nm_task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        ASU_AVIAEntities12 db1 = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db1.Spr_nm_task;
            Random random = new Random();
            var tick = random.Next(100);
            return PartialView("_GridViewPartial", model.ToList());
            
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Spr_nm_task item, HttpPostedFileBase Images_Path)
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
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Spr_nm_task item)
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
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Id)
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
            return PartialView("_GridViewPartial", model.ToList());
        }

        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControl", Spr_nm_taskControllerUploadControlSettings.UploadValidationSettings, Spr_nm_taskControllerUploadControlSettings.FileUploadComplete);
            return null;
        }

        

        [ValidateInput(false)]
        public ActionResult SprNmTaskGridViewPartial()
        {
            return PartialView("~/Areas/Admin/Views/Spr_nm_task/_SprNmTaskGridViewPartial.cshtml", _userTaskRepository.Table.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SprNmTaskGridViewPartialAddNew(Models.UsersTask.Spr_nm_task item)
        {
            var model = db.Spr_nm_task;
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
            return PartialView("~/Areas/Admin/Views/Spr_nm_task/_SprNmTaskGridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SprNmTaskGridViewPartialUpdate(Models.UsersTask.Spr_nm_task item)
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
            return PartialView("~/Areas/Admin/Views/Spr_nm_task/_SprNmTaskGridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SprNmTaskGridViewPartialDelete(System.Int32 Id)
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
            return PartialView("~/Areas/Admin/Views/Spr_nm_task/_SprNmTaskGridViewPartial.cshtml", model.ToList());
        }
    }
    public class Spr_nm_taskControllerUploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                // Save uploaded file to some location
            }
        }
    }

}
