using Asu.Web.Models;
using Asu.Web.ViewModel;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Gost_MaterController : Controller
    {
        private ASU_AVIAEntities12 db;
        public Gost_MaterController()
        {
            db = new ASU_AVIAEntities12();
        }
        public ActionResult Index()
        {
            return View();
        }       
        [ValidateInput(false)]
        public ActionResult GridView10Partial()
        {
            List<GOST_mater> gostMaterList = db.GOST_mater.ToList();
            GostMaterViewModel gmView = new GostMaterViewModel();
            List<GostMaterViewModel> gmViewList = gostMaterList.Select(x => new GostMaterViewModel
            {
                Id = x.Id,
                gost = x.gost,
                link_status = x.link_status,
                link_user = x.link_user,
                operation = x.operation,
                operation_date = x.operation_date,
                period_close_date = x.period_close_date,
                period_open_date = x.period_open_date
            }
            ).ToList();
            //var model = db.GOST_mater;
            return PartialView("_GridView10Partial", gmViewList);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView10PartialAddNew(Asu.Web.Models.GOST_mater item)
        {
            var model = db.GOST_mater;
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
            return PartialView("_GridView10Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView10PartialUpdate(Asu.Web.Models.GOST_mater item)
        {
            var model = db.GOST_mater;
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
            return PartialView("_GridView10Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView10PartialDelete(System.Int64 Id)
        {
            var model = db.GOST_mater;
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
            return PartialView("_GridView10Partial", model.ToList());
        }
    }
}