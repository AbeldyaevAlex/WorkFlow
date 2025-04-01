using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.Msi;
using Asu.Framework.Themes;
using Asu.Mapping.DocumentStatusService;
using Asu.Mapping.Metrology;
using Asu.Mapping.Msi;
using Asu.Services.Customers;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.Metrology.Controllers
{
    public class MainController : Controller
    {
        private const string PARAMETER_RODPOVERK_COOKIE_KEY = "WC.Parameter.RodPoverk.Cookie";
        private const string WORKSHOP_METROLOGY_COOKIE_KEY = "WC.Workshop.Metrology.Cookie";
        private const string WORKSHOPID_METROLOGY_COOKIE_KEY = "WC.WorkshopId.Metrology.Cookie";

        private readonly IRepository<Spr_cex> _sprCexRepository;
        private readonly IWorkContext _workContext;
        private readonly IMetrologyHelper _MetrologyHelper;
        private readonly HttpContextBase httpContext;
        private readonly IMetrologyService _MetrologyService;
        private readonly IRodPoverkService _RodPoverkService;
        private readonly IDocumentStatus _DocumentStatusService;
        private readonly ICustomerService _CustomerService;
        private readonly IPodgrPribService _PodgrPribService;
        private readonly INaznPribService _NaznPribService;
        private readonly IWorkShopService _WorkShopService;
        private readonly IPeriodPoverkService _PeriodPoverkService;
        private readonly IThemeContext _IThemeContext;
        private readonly ITipPribService _TipPribService;
        private readonly IKonservService _KonservService;





        public MainController(IRepository<Spr_cex> sprCexRepository, IWorkContext workContext, IMetrologyHelper MetrologyHelper, HttpContextBase httpContext,
            IMetrologyService MetrologyService, IRodPoverkService RodPoverkService, IDocumentStatus documentStatusService, ICustomerService CustomerService,
            IPodgrPribService podgrPribService, INaznPribService naznPribService, IWorkShopService WorkShopService, IPeriodPoverkService PeriodPoverkService,
            IThemeContext IThemeContext, ITipPribService TipPribService, IKonservService konservService)
        {
            _sprCexRepository = sprCexRepository;
            _workContext = workContext;
            _MetrologyHelper = MetrologyHelper;
            this.httpContext = httpContext;
            _MetrologyService = MetrologyService;
            _RodPoverkService = RodPoverkService;
            _DocumentStatusService = documentStatusService;
            _CustomerService = CustomerService;
            _PodgrPribService = podgrPribService;
            _NaznPribService = naznPribService;
            _WorkShopService = WorkShopService;
            _PeriodPoverkService = PeriodPoverkService;
            _IThemeContext = IThemeContext;
            _TipPribService = TipPribService;
            _KonservService = konservService;
        }

        public ActionResult MetrologyDirectory()
        {
            string RodPoverk = "";
            string Workshop = "";
            var RodPoverkCookie = this.httpContext.Request.Cookies.Get(PARAMETER_RODPOVERK_COOKIE_KEY);
            if (RodPoverkCookie != null)
            {
                RodPoverk = RodPoverkCookie.Value;
            }
            var WorkshopCookie = this.httpContext.Request.Cookies.Get(WORKSHOP_METROLOGY_COOKIE_KEY);
            if (WorkshopCookie != null)
            {
                Workshop = WorkshopCookie.Value;
            }
            @ViewData["RodPoverk"] = RodPoverk;
            @ViewData["Workshop"] = Workshop;
            return View();
        }
        public ActionResult VerificationMode()
        {
            ViewBag.Cex = new SelectList(_sprCexRepository.Table.Where(x => x.NaimCex.Contains("метролог")), "Id", "NaimCex");
            return View();
        }
        [HttpPost]
        public ActionResult VerificationMode(Spr_cex model, string Parameter)
        {
            _MetrologyHelper.SetParametrRodPoverkToCookies(Parameter);
            _MetrologyHelper.SetWorkshopToCookies(model.Id);
            _MetrologyHelper.SetWorkshopIdToCookies(model.Id);
            return RedirectToAction("MetrologyDirectory");
        }
        public ActionResult GetMetrologyDirectory()
        {
            string RodPoverk = "";
            string Workshop = "";
            var RodPoverkCookie = this.httpContext.Request.Cookies.Get(PARAMETER_RODPOVERK_COOKIE_KEY);
            if (RodPoverkCookie != null)
            {
                RodPoverk = RodPoverkCookie.Value;
            }
            var WorkshopCookie = this.httpContext.Request.Cookies.Get(WORKSHOP_METROLOGY_COOKIE_KEY);
            if (WorkshopCookie != null)
            {
                Workshop = WorkshopCookie.Value;
            }
            @ViewData["RodPoverk"] = RodPoverk;
            @ViewData["Workshop"] = Workshop;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartialGetMetrologyDirectory()
        {
            ViewBag.ListStatus = _DocumentStatusService.GetAllStatusList();
            ViewBag.RodPoverk = _RodPoverkService.GetRodPoverkList();
            ViewBag.Customer = _CustomerService.GetAllCustomers();
            ViewBag.PodgrPrib = _PodgrPribService.GetExtendedDirectoryOfPodgrPrib();
            ViewBag.NaznPrib = _NaznPribService.GetNaznPribList();
            ViewBag.WorkShop = _WorkShopService.GetWorkShopList();
            ViewBag.PeriodPover = _PeriodPoverkService.GetPeriodPoverList();
            ViewBag.MestoPoverk = _WorkShopService.GetWorkShopList();
            ViewBag.TipPrib = _TipPribService.GetTipPribList();

            string RodPoverk = "";
            int workshopId = 0;
            var RodPoverkCookie = this.httpContext.Request.Cookies.Get(PARAMETER_RODPOVERK_COOKIE_KEY);
            if (RodPoverkCookie != null)
            {
                RodPoverk = RodPoverkCookie.Value;
            }
            var WorkshopCookieId = this.httpContext.Request.Cookies.Get(WORKSHOPID_METROLOGY_COOKIE_KEY);
            if (WorkshopCookieId != null)
            {
                try
                {
                    int.TryParse(WorkshopCookieId.Value, out int WorkshopId);
                    workshopId = WorkshopId;
                }
                catch (Exception)
                {

                    throw;
                }                
            }

            var metrologyDirectory = _MetrologyService.GetMetrologyDirectory(workshopId, RodPoverk);
            return PartialView("_GridViewPartialGetMetrologyDirectory", metrologyDirectory);
        }
        [HttpPost]
        public ActionResult GridViewPartialMetrologyDirectoryAddNew(Spr_metrol model)
        {          
            return PartialView("_GridViewPartialGetMetrologyDirectory");
        }
    }
}