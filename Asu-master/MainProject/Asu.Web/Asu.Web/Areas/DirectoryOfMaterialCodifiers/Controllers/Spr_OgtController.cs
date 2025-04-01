using Asu.Mapping.DocumentStatusService;
using Asu.Mapping.Skm;
using Asu.Services.SprPkp;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_OgtController : Controller
    {
        private readonly IOgtService _ogtService;
        private readonly IGrMaterService _grMaterService;
        private readonly ISprPrKmService _sprPrKmService;

        public Spr_OgtController(IOgtService ogtService, IGrMaterService grMaterService, ISprPrKmService sprPrKmService)
        {
            _ogtService = ogtService;
            _grMaterService = grMaterService;
            _sprPrKmService = sprPrKmService;
        }
        public void GetTempDataForOgt()
        {
            ViewBag.PrKm = _sprPrKmService.GetAllPrKmToList();
            ViewBag.GrMater = _grMaterService.GetAllGrMaterList();
        }
        public ActionResult Index()
        {
            return View("~/DirectoryOfMaterialCodifiers/Spr_Ogt/Index.chtml");
        }
        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();
        [ValidateInput(false)]
        public ActionResult OgtDirectoryGridViewPartial()
        {
            GetTempDataForOgt();
            return PartialView("_OgtDirectoryGridViewPartial", _ogtService.GetAllOgt());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView11PartialAddNew(Asu.Web.Models.SPR_OGT item)
        {
            var model = db.SPR_OGT;
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
            return PartialView("_GridView11Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView11PartialUpdate(Asu.Web.Models.SPR_OGT item)
        {
            var model = db.SPR_OGT;
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
            return PartialView("_GridView11Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView11PartialDelete(System.Int64 Id)
        {
            var model = db.SPR_OGT;
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
            return PartialView("_GridView11Partial", model.ToList());
        }
    }
}