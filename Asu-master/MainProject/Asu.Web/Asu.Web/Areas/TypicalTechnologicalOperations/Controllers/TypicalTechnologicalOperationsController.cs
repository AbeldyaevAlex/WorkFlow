using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.TypicalTechnologicalOperations;
using Asu.Mapping.Skm;
using Asu.Mapping.TTO;
using Asu.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace Asu.Web.Areas.TypicalTechnologicalOperations.Controllers
{
    public partial class TtoViewModel
    {
        public IList<Group_TTO> UniqKodKomp { get; set; }
        public IQueryable<Spr_tto> SprTto { get; set; }
    }
    public class TypicalTechnologicalOperationsController : Controller
    {
        private readonly ITtoService _tTOService;
        private readonly IRepository<SprSkm> _sprSkmRepository;
        private readonly IRepository<MarkMater> _markMaterRepository;
        public TypicalTechnologicalOperationsController(ITtoService tTOService, IRepository<SprSkm> sprSkmRepository, IRepository<MarkMater> markMaterRepository)
        {
            _tTOService = tTOService;
            _sprSkmRepository = sprSkmRepository;
            _markMaterRepository = markMaterRepository;
        }
        public ActionResult MainTypicalTechnologicalOperations()
        {
            ViewBag.full_km_tto = _tTOService.GetFullSkmInfo().ToList();
            return View(new TtoViewModel { UniqKodKomp = _tTOService.GetUniQTTO(), SprTto = _tTOService.Get_TTO(null) });
        }
        public ActionResult GridViewPartialUniq()
        {
            return PartialView("_GridViewPartialUniq", _tTOService.GetUniQTTO());
        }
        public ActionResult GridViewPartialTTO(string kod_komp)
        {
            ViewBag.full_km_tto = _tTOService.GetFullSkmInfo().ToList();
            ViewBag.Kodtto = _sprSkmRepository.Table.ToList();
            if (kod_komp != null)
            {
                var kod_komp_id = int.Parse(kod_komp);
                ViewData["_kod_komp"] = kod_komp_id;
                return PartialView("_GridViewPartialTTO", _tTOService.Get_TTO(kod_komp_id));
            }
            else
            {
                var tto_Id = long.Parse(Request.Params["MasterRowKey"]);
                Session["TTOId"] = tto_Id;
                var TipicalTehnOper = _sprSkmRepository.Table.Where(i => i.Id == tto_Id).Select(c => c.Km).FirstOrDefault();
                Session["TipicalTehnOper"] = TipicalTehnOper;
                return PartialView("_GridViewPartialTTO", _tTOService.Get_TTO(Request.Params["MasterRowKey"]));
            }
        }
        [HttpPost]
        public ActionResult AddNewTtoGridViewPartial(Group_TTO model)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewKodKompTtoGridViewPartial(Spr_tto model)
        {
            model.KodTTOId = int.Parse(Session["TTOId"].ToString());
            return View();
        }
    }
}