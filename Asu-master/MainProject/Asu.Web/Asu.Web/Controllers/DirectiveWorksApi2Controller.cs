using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFDBFirst;

namespace Asu.Web.Controllers
{
    public class DirectiveWorksApi2Controller : Controller
    {
        private AsuAviaTestVersionBetaEntities1 db = new AsuAviaTestVersionBetaEntities1();

        // GET: DirectiveWorksApi2
        public ActionResult Index()
        {
            var directiveWork = db.DirectiveWork.Include(d => d.ExceptionForWork);
            return View(directiveWork.ToList());
        }

        // GET: DirectiveWorksApi2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectiveWork directiveWork = db.DirectiveWork.Find(id);
            if (directiveWork == null)
            {
                return HttpNotFound();
            }
            return View(directiveWork);
        }

        // GET: DirectiveWorksApi2/Create
        public ActionResult Create()
        {
            ViewBag.ExceptionForWorkId = new SelectList(db.ExceptionForWork, "Id", "ShortName");
            return View();
        }

        // POST: DirectiveWorksApi2/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PkpId,OboznId,CexIzgId,CexPotrId,link_uch,Directive_work_sdeln_izg,Directive_work_povr_izg,Directive_work_sdeln_usl,Directive_work_povr_usl,DirectoryOfTypesOfWorkId,Prim,NomDok,SprPviId,CustomerId,DocumentStatusId,Operation,OperationDate,PeriodOpenDate,PeriodCloseDate,ExceptionForWorkId")] DirectiveWork directiveWork)
        {
            if (ModelState.IsValid)
            {
                db.DirectiveWork.Add(directiveWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExceptionForWorkId = new SelectList(db.ExceptionForWork, "Id", "ShortName", directiveWork.ExceptionForWorkId);
            return View(directiveWork);
        }

        // GET: DirectiveWorksApi2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectiveWork directiveWork = db.DirectiveWork.Find(id);
            if (directiveWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExceptionForWorkId = new SelectList(db.ExceptionForWork, "Id", "ShortName", directiveWork.ExceptionForWorkId);
            return View(directiveWork);
        }

        // POST: DirectiveWorksApi2/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PkpId,OboznId,CexIzgId,CexPotrId,link_uch,Directive_work_sdeln_izg,Directive_work_povr_izg,Directive_work_sdeln_usl,Directive_work_povr_usl,DirectoryOfTypesOfWorkId,Prim,NomDok,SprPviId,CustomerId,DocumentStatusId,Operation,OperationDate,PeriodOpenDate,PeriodCloseDate,ExceptionForWorkId")] DirectiveWork directiveWork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directiveWork).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExceptionForWorkId = new SelectList(db.ExceptionForWork, "Id", "ShortName", directiveWork.ExceptionForWorkId);
            return View(directiveWork);
        }

        // GET: DirectiveWorksApi2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectiveWork directiveWork = db.DirectiveWork.Find(id);
            if (directiveWork == null)
            {
                return HttpNotFound();
            }
            return View(directiveWork);
        }

        // POST: DirectiveWorksApi2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DirectiveWork directiveWork = db.DirectiveWork.Find(id);
            db.DirectiveWork.Remove(directiveWork);
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
