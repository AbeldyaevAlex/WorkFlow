using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models;

namespace Asu.Web.Controllers
{
    public class Spr_PerizdController : Controller
    {
        private ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        // GET: Spr_Perizd
        public ActionResult Index()
        {
            var spr_Perizd = db.Spr_Perizd.Include(s => s.Status_dok).Include(s => s.Spr_tem).Include(s => s.User);
            return View(spr_Perizd.ToList());
        }

        // GET: Spr_Perizd/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spr_Perizd spr_Perizd = db.Spr_Perizd.Find(id);
            if (spr_Perizd == null)
            {
                return HttpNotFound();
            }
            return View(spr_Perizd);
        }

        // GET: Spr_Perizd/Create
        public ActionResult Create()
        {
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status");
            ViewBag.link_tema = new SelectList(db.Spr_tem, "Id", "nm_tem_p");
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name");
            return View();
        }

        // POST: Spr_Perizd/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,izdelie,kod_izd,nm_izd,ser_s,ser_po,kgk1_1,kgk1_n,kgk1_m,tek_ser_s,tek_ser_po,link_tema,link_status,link_user,period_open_date,period_close_date,operation,operation_date")] Spr_Perizd spr_Perizd)
        {
            if (ModelState.IsValid)
            {
                db.Spr_Perizd.Add(spr_Perizd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status", spr_Perizd.link_status);
            ViewBag.link_tema = new SelectList(db.Spr_tem, "Id", "nm_tem_p", spr_Perizd.link_tema);
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name", spr_Perizd.link_user);
            return View(spr_Perizd);
        }

        // GET: Spr_Perizd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spr_Perizd spr_Perizd = db.Spr_Perizd.Find(id);
            if (spr_Perizd == null)
            {
                return HttpNotFound();
            }
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status", spr_Perizd.link_status);
            ViewBag.link_tema = new SelectList(db.Spr_tem, "Id", "nm_tem_p", spr_Perizd.link_tema);
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name", spr_Perizd.link_user);
            return View(spr_Perizd);
        }

        // POST: Spr_Perizd/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,izdelie,kod_izd,nm_izd,ser_s,ser_po,kgk1_1,kgk1_n,kgk1_m,tek_ser_s,tek_ser_po,link_tema,link_status,link_user,period_open_date,period_close_date,operation,operation_date")] Spr_Perizd spr_Perizd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spr_Perizd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status", spr_Perizd.link_status);
            ViewBag.link_tema = new SelectList(db.Spr_tem, "Id", "nm_tem_p", spr_Perizd.link_tema);
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name", spr_Perizd.link_user);
            return View(spr_Perizd);
        }

        // GET: Spr_Perizd/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spr_Perizd spr_Perizd = db.Spr_Perizd.Find(id);
            if (spr_Perizd == null)
            {
                return HttpNotFound();
            }
            return View(spr_Perizd);
        }

        // POST: Spr_Perizd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spr_Perizd spr_Perizd = db.Spr_Perizd.Find(id);
            db.Spr_Perizd.Remove(spr_Perizd);
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
    public class list_Izd
    {
        public List<Spr_Perizd> listIzd { get; set; }
    }
}
