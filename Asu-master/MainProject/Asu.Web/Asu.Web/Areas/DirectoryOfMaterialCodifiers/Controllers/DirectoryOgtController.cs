using Asu.Mapping.Skm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class DirectoryOgtController : Controller
    {
        private readonly IOgtService _ogtService;
        private readonly IGrMaterService _grMaterService;
        private readonly ISprPrKmService _sprPrKmService;
        public DirectoryOgtController(IOgtService ogtService, IGrMaterService grMaterService, ISprPrKmService sprPrKmService)
        {
            _ogtService = ogtService;
            _grMaterService = grMaterService;
            _sprPrKmService = sprPrKmService;
        }
        public ActionResult MainOgt()
        {
            return View("~/Areas/DirectoryOfMaterialCodifiers/Views/DirectoryOgt/MainOgt.cshtml");
        }
        [ValidateInput(false)]
        public ActionResult OgtDirectoryGridViewPartial()
        {
            GetTempDataForOgt();
            return PartialView("~/Areas/DirectoryOfMaterialCodifiers/Views/DirectoryOgt/_OgtDirectoryGridViewPartial.cshtml", _ogtService.GetAllOgt());
        }
        public void GetTempDataForOgt()
        {
            ViewBag.PrKm = _sprPrKmService.GetAllPrKmToList();
            ViewBag.GrMater = _grMaterService.GetAllGrMaterList();
        }
        public ActionResult Test(int ogtId)
        {
            return View("~/Areas/DirectoryOfMaterialCodifiers/Views/DirectoryOgt/Test.cshtml");
        }
        public ActionResult GetTab4()
        {
            return PartialView("~/Areas/DirectoryOfMaterialCodifiers/Views/DirectoryOgt/_TestPartialViewForTab.cshtml");
        }
        
    }
}