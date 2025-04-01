using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Spr_skmController : Controller
    {
        private ASU_AVIAEntities12 db = new ASU_AVIAEntities12();

        // GET: msi/Spr_skm
        public ActionResult Index()
        {
            var spr_skm = db.Spr_skm.Include(s => s.GOST_mater).Include(s => s.Mark_mater).Include(s => s.Nm_mater).Include(s => s.Spr_balsch).Include(s => s.Spr_eizm).Include(s => s.Spr_GR_Mater).Include(s => s.Spr_kgr).Include(s => s.SPR_OGT).Include(s => s.SPR_OTS).Include(s => s.SPR_PRKM).Include(s => s.Status_dok).Include(s => s.User);
            return View(spr_skm.ToList());
        }

        // GET: msi/Spr_skm/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spr_skm spr_skm = db.Spr_skm.Find(id);
            if (spr_skm == null)
            {
                return HttpNotFound();
            }
            return View(spr_skm);
        }

        // GET: msi/Spr_skm/Create
        public ActionResult Create()
        {
            ViewBag.link_gost = new SelectList(db.GOST_mater, "Id", "gost");
            ViewBag.link_marka = new SelectList(db.Mark_mater, "Id", "marka_mater");
            ViewBag.link_nm_skm = new SelectList(db.Nm_mater, "Id", "nm_mater1");
            ViewBag.link_balsch = new SelectList(db.Spr_balsch, "Id", "operation");
            ViewBag.link_eizm = new SelectList(db.Spr_eizm, "Id", "krat_naim_eizm");
            ViewBag.link_GR_Mater = new SelectList(db.Spr_GR_Mater, "Id", "nm_gr_mater");
            ViewBag.link_kgr = new SelectList(db.Spr_kgr, "Id", "fio");
            ViewBag.link_ogt = new SelectList(db.SPR_OGT, "Id", "naim_ogt");
            ViewBag.link_ots = new SelectList(db.SPR_OTS, "Id", "per");
            ViewBag.link_prkm = new SelectList(db.SPR_PRKM, "Id", "prkm");
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status");
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name");
            return View();
        }

        // POST: msi/Spr_skm/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,km,dbt,dsh,ves,link_nm_skm,link_marka,link_gost,link_eizm,link_kgr,link_ots,link_ogt,link_balsch,link_prkm,link_status,link_user,operation,operation_date,period_open_date,period_close_date,link_GR_Mater,nomenkl_nomer")] Spr_skm spr_skm)
        {
            if (ModelState.IsValid)
            {
                db.Spr_skm.Add(spr_skm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.link_gost = new SelectList(db.GOST_mater, "Id", "gost", spr_skm.link_gost);
            ViewBag.link_marka = new SelectList(db.Mark_mater, "Id", "marka_mater", spr_skm.link_marka);
            ViewBag.link_nm_skm = new SelectList(db.Nm_mater, "Id", "nm_mater1", spr_skm.link_nm_skm);
            ViewBag.link_balsch = new SelectList(db.Spr_balsch, "Id", "operation", spr_skm.link_balsch);
            ViewBag.link_eizm = new SelectList(db.Spr_eizm, "Id", "krat_naim_eizm", spr_skm.link_eizm);
            ViewBag.link_GR_Mater = new SelectList(db.Spr_GR_Mater, "Id", "nm_gr_mater", spr_skm.link_GR_Mater);
            ViewBag.link_kgr = new SelectList(db.Spr_kgr, "Id", "fio", spr_skm.link_kgr);
            ViewBag.link_ogt = new SelectList(db.SPR_OGT, "Id", "naim_ogt", spr_skm.link_ogt);
            ViewBag.link_ots = new SelectList(db.SPR_OTS, "Id", "per", spr_skm.link_ots);
            ViewBag.link_prkm = new SelectList(db.SPR_PRKM, "Id", "prkm", spr_skm.link_prkm);
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status", spr_skm.link_status);
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name", spr_skm.link_user);
            return View(spr_skm);
        }

        // GET: msi/Spr_skm/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spr_skm spr_skm = db.Spr_skm.Find(id);
            if (spr_skm == null)
            {
                return HttpNotFound();
            }
            ViewBag.link_gost = new SelectList(db.GOST_mater, "Id", "gost", spr_skm.link_gost);
            ViewBag.link_marka = new SelectList(db.Mark_mater, "Id", "marka_mater", spr_skm.link_marka);
            ViewBag.link_nm_skm = new SelectList(db.Nm_mater, "Id", "nm_mater1", spr_skm.link_nm_skm);
            ViewBag.link_balsch = new SelectList(db.Spr_balsch, "Id", "operation", spr_skm.link_balsch);
            ViewBag.link_eizm = new SelectList(db.Spr_eizm, "Id", "krat_naim_eizm", spr_skm.link_eizm);
            ViewBag.link_GR_Mater = new SelectList(db.Spr_GR_Mater, "Id", "nm_gr_mater", spr_skm.link_GR_Mater);
            ViewBag.link_kgr = new SelectList(db.Spr_kgr, "Id", "fio", spr_skm.link_kgr);
            ViewBag.link_ogt = new SelectList(db.SPR_OGT, "Id", "naim_ogt", spr_skm.link_ogt);
            ViewBag.link_ots = new SelectList(db.SPR_OTS, "Id", "per", spr_skm.link_ots);
            ViewBag.link_prkm = new SelectList(db.SPR_PRKM, "Id", "prkm", spr_skm.link_prkm);
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status", spr_skm.link_status);
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name", spr_skm.link_user);
            return View(spr_skm);
        }

        // POST: msi/Spr_skm/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,km,dbt,dsh,ves,link_nm_skm,link_marka,link_gost,link_eizm,link_kgr,link_ots,link_ogt,link_balsch,link_prkm,link_status,link_user,operation,operation_date,period_open_date,period_close_date,link_GR_Mater,nomenkl_nomer")] Spr_skm spr_skm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spr_skm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.link_gost = new SelectList(db.GOST_mater, "Id", "gost", spr_skm.link_gost);
            ViewBag.link_marka = new SelectList(db.Mark_mater, "Id", "marka_mater", spr_skm.link_marka);
            ViewBag.link_nm_skm = new SelectList(db.Nm_mater, "Id", "nm_mater1", spr_skm.link_nm_skm);
            ViewBag.link_balsch = new SelectList(db.Spr_balsch, "Id", "operation", spr_skm.link_balsch);
            ViewBag.link_eizm = new SelectList(db.Spr_eizm, "Id", "krat_naim_eizm", spr_skm.link_eizm);
            ViewBag.link_GR_Mater = new SelectList(db.Spr_GR_Mater, "Id", "nm_gr_mater", spr_skm.link_GR_Mater);
            ViewBag.link_kgr = new SelectList(db.Spr_kgr, "Id", "fio", spr_skm.link_kgr);
            ViewBag.link_ogt = new SelectList(db.SPR_OGT, "Id", "naim_ogt", spr_skm.link_ogt);
            ViewBag.link_ots = new SelectList(db.SPR_OTS, "Id", "per", spr_skm.link_ots);
            ViewBag.link_prkm = new SelectList(db.SPR_PRKM, "Id", "prkm", spr_skm.link_prkm);
            ViewBag.link_status = new SelectList(db.Status_dok, "Id", "status", spr_skm.link_status);
            ViewBag.link_user = new SelectList(db.User, "Id", "First_Name", spr_skm.link_user);
            return View(spr_skm);
        }

        // GET: msi/Spr_skm/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spr_skm spr_skm = db.Spr_skm.Find(id);
            if (spr_skm == null)
            {
                return HttpNotFound();
            }
            return View(spr_skm);
        }

        // POST: msi/Spr_skm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Spr_skm spr_skm = db.Spr_skm.Find(id);
            db.Spr_skm.Remove(spr_skm);
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
