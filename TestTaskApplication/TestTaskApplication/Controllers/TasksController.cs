using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFCore.Model;

namespace TestTaskApplication.Controllers
{
    public class TasksController : Controller
    {
        private ShopDBEntities1 db = new ShopDBEntities1();

        // GET: Tasks
        public ActionResult Index()
        {
            var task = db.Task.Include(t => t.Customer).Include(t => t.CustomerRole).Include(t => t.StatusTask);
            return View(task.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName");
            ViewBag.RoleId = new SelectList(db.CustomerRole, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.StatusTask, "Id", "Status");
            return View();
        }

        // POST: Tasks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,TaskName,OpenDate,ClosedDate,CustomerId,StatusId,RoleId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Task.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", task.CustomerId);
            ViewBag.RoleId = new SelectList(db.CustomerRole, "Id", "Name", task.RoleId);
            ViewBag.StatusId = new SelectList(db.StatusTask, "Id", "Status", task.StatusId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", task.CustomerId);
            ViewBag.RoleId = new SelectList(db.CustomerRole, "Id", "Name", task.RoleId);
            ViewBag.StatusId = new SelectList(db.StatusTask, "Id", "Status", task.StatusId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,TaskName,OpenDate,ClosedDate,CustomerId,StatusId,RoleId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", task.CustomerId);
            ViewBag.RoleId = new SelectList(db.CustomerRole, "Id", "Name", task.RoleId);
            ViewBag.StatusId = new SelectList(db.StatusTask, "Id", "Status", task.StatusId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Task.Find(id);
            db.Task.Remove(task);
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
    }
}
