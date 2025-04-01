using Asu.Web.Models;
using Asu.Web.Repository;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class Spr_pviController : Controller
    {
        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomSearch(string pvi)
        {
            var result_pvi = db.Spr_pvi.Where(i => i.pvi == pvi).ToList();
            return PartialView("_GridView22Partial", result_pvi);
        }
        [Authorize]       
        [ValidateInput(false)]
        public ActionResult GridView22Partial(Spr_pvi item, string param)
        {
            var packageId = RouteData.Values.Values;

            IGenericRepository<Spr_pvi> generic_repository = new GenericRepository<Spr_pvi>();
            var model_spr_pvi = generic_repository.GetAll();
            if (ViewData["Insert"] != null)
            {
                generic_repository.Add(item);
                generic_repository.Save();
                return PartialView("_GridView22Partial", model_spr_pvi);
            }
            else if (param == "Update")
            {

            }
            return PartialView("_GridView22Partial", model_spr_pvi);
        }
        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewSprPviPartialDelete(System.Int64 Id)
        {
            var model = db.Spr_pvi;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                    {
                        item.link_status = 46;
                        item.operation = "cancelled";
                        item.link_user = (Int32)Session["UserId"];
                        item.operation_date = DateTime.Now;
                        item.period_close_date = DateTime.Now;
                        this.UpdateModel(item);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView22Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView22PartialAddNew(Asu.Web.Models.Spr_pvi item)
        {
            var model = db.Spr_pvi;
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
            return PartialView("_GridView22Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView22PartialUpdate(Asu.Web.Models.Spr_pvi item)
        {
            var model = db.Spr_pvi;
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
            return PartialView("_GridView22Partial", model.ToList());
        }
        
    }
}