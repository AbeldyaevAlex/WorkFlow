using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.GoogleTagManager;
using Asu.Core.Domain.Localization;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using Asu.Data;
using Asu.Mapping.DocumentStatusService;
using Asu.Mapping.Malahit;
using Asu.Mapping.Skm;
using Asu.Services.Catalog;
using Asu.Services.Customers;
using Asu.Services.CustomerServices;
using Asu.Services.Localization;
using Asu.Services.Security;
//using Asu.Web.Models;
using Asu.Web.Models.Catalog;
using Asu.Web.Models.DirectoryOfMaterialCodifiers;
using Asu.Web.ViewModel;
using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using static Kendo.Mvc.UI.UIPrimitives;


namespace Asu.Web.Controllers
{
    public class SkmController : Controller
    {
        private const string FILTER_NAIM_ID_COOKIE_KEY = "WC.FilterNaim.Id.Cookie";
        private const string FILTER_MARKA_ID_COOKIE_KEY = "WC.FilterMarka.Id.Cookie";
        private const string FILTER_GOST_ID_COOKIE_KEY = "WC.FilterGost.Id.Cookie";
        private const string OGT_ID_COOKIE_KEY = "WC.Ogt.Id.Cookie";
        private const string NAIM_SKM_COOKIE_KEY = "WC.Naim.Skm.Cookie";
        private const string MARKA_ID_COOKIE_KEY_AFTER_CHANGE = "WC.Marka.Id.After.Change.Cookie";
        private const string GOST_ID_COOKIE_KEY_AFTER_CHANGE = "WC.Gost.Id.After.Change.Cookie";

        private readonly IRepository<MemorandumBase> _memorandumBaseRepository;
        private readonly IRepository<SprSkm> _SprSkmRepository;
        private readonly IRepository<MemoAddingMaterialCode> _MemoAddingMaterialCodeRepository;
        private readonly IRepository<DocumentStatus> _DocumentStatusRepository;
        private readonly IRepository<DirectoryOfMaterialName> _DirectoryOfMaterialNameRepository;
        private readonly IRepository<MarkMater> _MarkMaterRepository;
        private readonly IRepository<Spr_pvi> _Spr_pviRepository;
        private readonly IRepository<GostMater> _GostMaterRepository;
        private readonly IRepository<SprKgr> _SprKgrRepository;
        private readonly IRepository<Spr_pkp> _SprPkpRepository;
        private readonly IRepository<Customer> _CustomerRepository;
        private readonly IRepository<SprEizm> _SprEizmRepository;
        private readonly IRepository<SprOgt> _SprOgtRepository;
        private readonly IRepository<SprOts> _SprOtsRepository;
        private readonly IRepository<SprPrKm> _SprPrKmRepository;
        private readonly IRepository<SprBalSch> _SprBalSchRepository;
        private readonly IRepository<SprGrMater> _SprGrMaterRepository;
        private readonly IRepository<SortMater> _SortMaterRepository;
        private readonly IRepository<SprSortam> _SprSortamRepository;
        private readonly IWorkContext _workContext;
        private readonly ISkmHelper _SkmHelper;
        private readonly HttpContextBase httpContext;
        private readonly INmMaterService _INmMaterService;
        private readonly IMarkaMaterialService _IMarkaMaterialService;
        private readonly IGostMaterService _IGostMaterService;
        private readonly IOgtService _IOgtService;
        private readonly IMemoAddingMaterialCode _IMemoAddingMaterialCode;
        private readonly IAclService _aclService;

        private readonly INmMaterService _nmMaterService;
        private readonly IMarkaMaterialService _markaMaterialService;
        private readonly IGostMaterService _gostMaterService;
        private readonly IUnitOfMeasurementService _unitOfMeasurementService;
        private readonly IGrMaterService _grMaterService;
        private readonly ISprKgrService _sprKgrService;
        private readonly IOtsService _otsService;
        private readonly IDocumentStatus _DocumentStatusService;
        private readonly IOgtService _OgtService;
        private readonly IDirectoryOfMaterialCodifiersService _directoryOfMaterialCodifiersService;
        private readonly ISprCenMaterService _sprCenMaterService;
        



        public SkmController(IRepository<MemoAddingMaterialCode> MemoAddingMaterialCodeRepository, INmMaterService nmMaterService, IMarkaMaterialService markaMaterialService,
            IGostMaterService gostMaterService, IUnitOfMeasurementService unitOfMeasurementService, IGrMaterService grMaterService, IOtsService otsService, IDocumentStatus DocumentStatusService,
            IRepository<DocumentStatus> DocumentStatusRepository, IOgtService OgtService, IDirectoryOfMaterialCodifiersService directoryOfMaterialCodifiersService, ISprKgrService sprKgrService,
            IRepository<DirectoryOfMaterialName> directoryOfMaterialNameRepository, ISprCenMaterService sprCenMaterService,
            IRepository<MarkMater> markMaterRepository,
            IRepository<Spr_pvi> Spr_pviRepository,
            IRepository<GostMater> GostMaterRepository,
            IRepository<SprKgr> sprKgrRepository,
            IRepository<Spr_pkp> SprPkpRepository,
            IWorkContext workContext,
            IRepository<Customer> CustomerRepository,
            IRepository<SprEizm> SprEizmRepository,
            IRepository<SprOgt> SprOgtRepository,
            IRepository<SprOts> SprOtsRepository,
            IRepository<SprPrKm> SprPrKmRepository,
            IRepository<SprBalSch> SprBalSchRepository,
            IRepository<SprGrMater> SprGrMaterRepository,
            IRepository<SortMater> sortMaterRepository,
            IRepository<SprSortam> sprSortamRepository,
            IRepository<SprSkm> sprSkmRepository,
            ISkmHelper SkmHelper,
            HttpContextBase httpContext,
            INmMaterService INmMaterService,
            IMarkaMaterialService IMarkaMaterialService,
            IGostMaterService IGostMaterService,
            IOgtService IOgtService,
            IMemoAddingMaterialCode IMemoAddingMaterialCode,
            IAclService aclService, IRepository<MemorandumBase> memorandumBaseRepository)

        {
            _MemoAddingMaterialCodeRepository = MemoAddingMaterialCodeRepository;
            _DocumentStatusRepository = DocumentStatusRepository;
            _DirectoryOfMaterialNameRepository = directoryOfMaterialNameRepository;
            _MarkMaterRepository = markMaterRepository;
            _Spr_pviRepository = Spr_pviRepository;
            _GostMaterRepository = GostMaterRepository;
            _SprKgrRepository = sprKgrRepository;
            _SprPkpRepository = SprPkpRepository;
            _workContext = workContext;
            _CustomerRepository = CustomerRepository;
            _SprEizmRepository = SprEizmRepository;
            _SprOgtRepository = SprOgtRepository;
            _SprOtsRepository = SprOtsRepository;
            _SprPrKmRepository = SprPrKmRepository;
            _SprBalSchRepository = SprBalSchRepository;
            _SprGrMaterRepository = SprGrMaterRepository;
            _SortMaterRepository = sortMaterRepository;
            _SprSortamRepository = sprSortamRepository;
            _SprSkmRepository = sprSkmRepository;
            _SkmHelper = SkmHelper;
            this.httpContext = httpContext;
            _INmMaterService = INmMaterService;
            _IMarkaMaterialService = IMarkaMaterialService;
            _IGostMaterService = IGostMaterService;
            _IOgtService = IOgtService;
            _IMemoAddingMaterialCode = IMemoAddingMaterialCode;
            _aclService = aclService;
            _nmMaterService = nmMaterService;
            _markaMaterialService = markaMaterialService;
            _gostMaterService = gostMaterService;
            _unitOfMeasurementService = unitOfMeasurementService;
            _grMaterService = grMaterService;
            _otsService = otsService;
            _DocumentStatusService = DocumentStatusService;
            _OgtService = OgtService;
            _directoryOfMaterialCodifiersService = directoryOfMaterialCodifiersService;
            _sprKgrService = sprKgrService;
            _sprCenMaterService = sprCenMaterService;
            _memorandumBaseRepository = memorandumBaseRepository;
        }
        public ActionResult MemorandumAddingMaterialCode()
        {
            _SkmHelper.ClearSkmCookies();
            return View();
        }
        public ActionResult ReadMemorandumMaterialCode([DataSourceRequest] DataSourceRequest request)
        {

            var query = _MemoAddingMaterialCodeRepository.Table;
            var products = query.ToList();

            products = products.Where(x => _aclService.Authorize(x)).ToList();

            var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                       .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

            var items = _workContext.CurrentCustomer.MemoAddingMaterialCode;
            var IsAdmin = _workContext.CurrentCustomer.IsAdmin();
            var Workshops = _workContext.CurrentCustomer.UsersWorkshop;
            _SkmHelper.ClearSkmCookies();
            //var NmSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_NAIM_ID_COOKIE_KEY);
            //if (NmSkmCookie != null)
            //{
            //    _SkmHelper.ClearSkmCookies();
            //}
            List<MemoAddingMaterialCodeViewModel> materialCodeViewModels = new List<MemoAddingMaterialCodeViewModel>();

            materialCodeViewModels = (from memorandumMaterial in _MemoAddingMaterialCodeRepository.Table
                                      join status in _DocumentStatusRepository.Table
                                      on memorandumMaterial.DocumentStatusId equals status.Id
                                      join nmmater in _DirectoryOfMaterialNameRepository.Table
                                      on memorandumMaterial.NmSkmId equals nmmater.Id
                                      join markmater in _MarkMaterRepository.Table
                                      on memorandumMaterial.MarkaId equals markmater.Id
                                      join customer in _CustomerRepository.Table
                                      on memorandumMaterial.CustomerId equals customer.Id
                                      join memorandumeizmMaterial in _SprEizmRepository.Table
                                      on memorandumMaterial.EizmId equals memorandumeizmMaterial.Id
                                      join memorandumGostMaterial in _GostMaterRepository.Table
                                      on memorandumMaterial.GostId equals memorandumGostMaterial.Id
                                      join ogt in _SprOgtRepository.Table
                                      on memorandumMaterial.OgtId equals ogt.Id
                                      join ots in _SprOtsRepository.Table
                                      on memorandumMaterial.OtsId equals ots.Id
                                      join kgr in _SprKgrRepository.Table
                                      on memorandumMaterial.KgrId equals kgr.Id
                                      join prkm in _SprPrKmRepository.Table
                                      on memorandumMaterial.PrkmId equals prkm.Id
                                      join bal in _SprBalSchRepository.Table
                                      on memorandumMaterial.BalschId equals bal.Id
                                      join grmater in _SprGrMaterRepository.Table
                                      on memorandumMaterial.GRMaterId equals grmater.Id
                                      join prkmOgt in _SprPrKmRepository.Table
                                      on ogt.PrkmId equals prkmOgt.Id
                                      join sotramentOgt in _SprSortamRepository.Table
                                      on ogt.SortamMaterId equals sotramentOgt.Id
                                      select new MemoAddingMaterialCodeViewModel
                                      {
                                          Id = memorandumMaterial.Id,
                                          AtWork = memorandumMaterial.AtWork,
                                          InTheUsersWorkId = memorandumMaterial.InTheUsersWorkId,
                                          DocumentStatusId = memorandumMaterial.DocumentStatusId,
                                          NameMaterial = nmmater.NameMaterial,
                                          Status = status.Status,
                                          MarkaMater = markmater.MarkaMater,
                                          Km = memorandumMaterial.Km,
                                          Dbt = memorandumMaterial.Dbt,
                                          Dsh = memorandumMaterial.Dsh,
                                          PeriodOpenDate = memorandumMaterial.PeriodOpenDate,
                                          PeriodCloseDate = memorandumMaterial.PeriodCloseDate,
                                          Ves = memorandumMaterial.Ves,
                                          FullCustomerName = customer.BillingAddress.LastName + " " + customer.BillingAddress.FirstName.Substring(0, 1) + ". " + customer.BillingAddress.MiddleName.Substring(0, 1) + ".",
                                          KratNaimEizm = memorandumeizmMaterial.KratNaimEizm,
                                          Gost = memorandumGostMaterial.Gost,
                                          NomenklNomer = memorandumMaterial.NomenklNomer,
                                          OperationDate = memorandumMaterial.OperationDate,
                                          OpisanCherteg = memorandumMaterial.OpisanCherteg,
                                          PriznTto = memorandumMaterial.PriznTto,
                                          SortOGT = memorandumMaterial.SortOGT,
                                          DopolnNomProfil = memorandumMaterial.DopolnNomProfil,
                                          OGT = ogt.OGT.ToString(),
                                          NaimOgt = ogt.NaimOgt,
                                          Ots = ots.KodSklad.ToString(),
                                          Kgr = kgr.Kgr.ToString(),
                                          NoMemorandumLine = memorandumMaterial.NoMemorandumLine,
                                          Prkm = prkm.PrKm,
                                          BalSch = bal.BalSchet.ToString(),
                                          GrMater = grmater.NmGrMater,
                                          NomerGrMater = grmater.NomerGrMater.ToString(),
                                          PrkmOgt = prkmOgt.PrKm,
                                          NmPrkmOgt = prkmOgt.NmPrkm,
                                          KsimKm = ogt.KsimKm.ToString(),
                                          Sortament = sotramentOgt.Sortament,
                                          Comment = memorandumMaterial.Comment
                                      }).ToList();

            var no_sz = _MemoAddingMaterialCodeRepository.Table.OrderByDescending(num => num.NoMemorandumLine).Select(x => x.NoMemorandumLine).FirstOrDefault() + 1;

            //return Json(materialCodeViewModels.ToDataSourceResult(request));

            return Json(materialCodeViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetNaimOgt(string naimogt)
        {
            var i = true;

            List<GroupOgtViewModelForGrid> listNaimOgt = new List<GroupOgtViewModelForGrid>();

            if (i)
            {
                listNaimOgt = (from ogtRepo in _SprOgtRepository.Table
                               where ogtRepo.NaimOgt == naimogt
                               join grmater in _SprGrMaterRepository.Table
                               on ogtRepo.GrMaterId equals grmater.Id
                               join prkm in _SprPrKmRepository.Table
                               on ogtRepo.PrkmId equals prkm.Id
                               join sortMater in _SortMaterRepository.Table
                               on ogtRepo.SortMaterId equals sortMater.Id
                               join sortamentMater in _SprSortamRepository.Table
                               on ogtRepo.SortamMaterId equals sortamentMater.Id
                               select new GroupOgtViewModelForGrid
                               {
                                   Id = ogtRepo.Id,
                                   OGT = ogtRepo.OGT.ToString(),
                                   NaimOgt = ogtRepo.NaimOgt,
                                   KsimKm = ogtRepo.KsimKm.ToString(),
                                   NmPrkmOgt = prkm.NmPrkm,
                                   PrkmOgt = prkm.PrKm,
                                   NmGrMater = grmater.NmGrMater,
                                   NomerGrMater = grmater.NomerGrMater.ToString(),
                                   SortUsl = sortMater.SortUsl,
                                   UslRu = sortMater.UslRu,
                                   Sortament = sortamentMater.Sortament
                               }).ToList();

                var OGTID = listNaimOgt.Select(e => e.Id).FirstOrDefault();
                _SkmHelper.SetOgtIdToCookies(OGTID);

                var FilterNaimSkmId = _SprSkmRepository.Table.Where(x => x.OgtId == OGTID).Select(s => s.NmSkmId).ToList().Distinct();
                var resultFilterNaimSkmId = string.Join("|", FilterNaimSkmId);
                _SkmHelper.SetNmSkmIdToCookies(resultFilterNaimSkmId);
            }
            return Json(listNaimOgt, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetOgt(string ogt)
        {
            List<GroupOgtViewModelForGrid> listOgt = new List<GroupOgtViewModelForGrid>();
            //List<SprOgt> listOgt = new List<SprOgt>();

            //int OgtId;
            var i = int.TryParse(ogt, out int OgtId);
            if (i)
            {
                //listOgt = _SprOgtRepository.Table.Where(x => x.OGT == OgtId).ToList();

                listOgt = (from ogtRepo in _SprOgtRepository.Table
                           where ogtRepo.OGT == OgtId
                           join grmater in _SprGrMaterRepository.Table
                           on ogtRepo.GrMaterId equals grmater.Id
                           join prkm in _SprPrKmRepository.Table
                           on ogtRepo.PrkmId equals prkm.Id
                           join sortMater in _SortMaterRepository.Table
                           on ogtRepo.SortMaterId equals sortMater.Id
                           join sortamentMater in _SprSortamRepository.Table
                           on ogtRepo.SortamMaterId equals sortamentMater.Id
                           select new GroupOgtViewModelForGrid
                           {
                               Id = ogtRepo.Id,
                               OGT = ogtRepo.OGT.ToString(),
                               NaimOgt = ogtRepo.NaimOgt,
                               KsimKm = ogtRepo.KsimKm.ToString(),
                               NmPrkmOgt = prkm.NmPrkm,
                               PrkmOgt = prkm.PrKm,
                               NmGrMater = grmater.NmGrMater,
                               NomerGrMater = grmater.NomerGrMater.ToString(),
                               SortUsl = sortMater.SortUsl,
                               UslRu = sortMater.UslRu,
                               Sortament = sortamentMater.Sortament
                           }).ToList();

                var OGTID = listOgt.Select(e => e.Id).FirstOrDefault();
                _SkmHelper.SetOgtIdToCookies(OGTID);

                var FilterNaimSkmId = _SprSkmRepository.Table.Where(x => x.OgtId == OGTID).Select(s => s.NmSkmId).ToList().Distinct();
                var resultFilterNaimSkmId = string.Join("|", FilterNaimSkmId);
                _SkmHelper.SetNmSkmIdToCookies(resultFilterNaimSkmId);
            }
            return Json(listOgt, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AddMemorandumMaterialCode([DataSourceRequest] DataSourceRequest request, MemoAddingMaterialCodeViewModel model)
        {
            MemoAddingMaterialCode materialCodeModel = new MemoAddingMaterialCode();

            materialCodeModel = _IMemoAddingMaterialCode.PrepareMemorandumMaterialCodeModel(model.OGT, model.NaimOgt, model.NomerGrMater, model.GrMater, model.Dbt, model.Dsh, model.Ves, model.Km);

            _MemoAddingMaterialCodeRepository.Insert(materialCodeModel);
            return RedirectToAction("Index");
        }
        public ActionResult ComboBoxMemoAddingPrkmRead()
        {
            List<DirectoryOfSignsOfMaterialCodesViewModel> modelPrkm = new List<DirectoryOfSignsOfMaterialCodesViewModel>();

            modelPrkm = (from prkm in _SprPrKmRepository.Table
                         where prkm.Id != 1
                         select new DirectoryOfSignsOfMaterialCodesViewModel
                         {
                             Id = prkm.Id,
                             PrKm = prkm.PrKm,
                             NmPrKm = prkm.NmPrkm
                         }).ToList();
            return Json(modelPrkm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ToTakeOnTheJob(int szId)
        {
            var model = _IMemoAddingMaterialCode.GetMemoMaterialCodeFromSZId(szId);
            model.AtWork = true;
            model.InTheUsersWorkId = _workContext.CurrentCustomer.Id;
            _MemoAddingMaterialCodeRepository.Update(model);
            return View();
        }
        public void GetSettingsToTakeOnTheJob(int szId)
        {
            var model = _IMemoAddingMaterialCode.GetMemoMaterialCodeFromSZId(szId);
            if (model.AtWork)
            {
                var customer = _CustomerRepository.GetById(model.InTheUsersWorkId);
                var FullCustomerName = customer.BillingAddress.LastName + " " + customer.BillingAddress.FirstName.Substring(0, 1) + ". " + customer.BillingAddress.MiddleName.Substring(0, 1) + ".";
                ViewData["AtWork"] = FullCustomerName;
            }
            else
            {
                ViewData["AtWork"] = null;
            }
        }
        public ActionResult ComboBoxMemoAddingMaterialDshRead(string Dsh)
        {
            var MarkaSkmCookie = this.httpContext.Request.Cookies.Get(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            string MarkaSkm = null;

            var NmSkmCookie = this.httpContext.Request.Cookies.Get(NAIM_SKM_COOKIE_KEY);
            string NmSkm = null;

            var GostSkmCookie = this.httpContext.Request.Cookies.Get(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            string GostSkm = null;

            int GostId = 1;
            if (GostSkmCookie != null)
            {
                GostSkm = GostSkmCookie.Value;
                GostId = _IGostMaterService.GetIdFromGost(GostSkm);
            }
            int nmMaterId = 1;
            if (NmSkmCookie != null)
            {
                NmSkm = NmSkmCookie.Value;
                nmMaterId = _INmMaterService.GetIdFromNameMater(NmSkm);
            }
            int markaMaterId = 1;
            if (MarkaSkmCookie != null)
            {
                MarkaSkm = MarkaSkmCookie.Value;
                markaMaterId = _IMarkaMaterialService.GetIdFromNameMarkMater(MarkaSkm);
            }
            List<SkmViewModel> modelKm = new List<SkmViewModel>();
            var OgtCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);

            var OgtId = int.Parse(OgtCookie.Value);
            if (!String.IsNullOrEmpty(Dsh) || !String.IsNullOrWhiteSpace(Dsh))
            {
                modelKm = (from km in _SprSkmRepository.Table
                           where km.OgtId == OgtId && km.Dbt.Contains(Dsh) && km.NmSkmId == nmMaterId && km.MarkaId == markaMaterId && km.GostId == GostId
                           select new SkmViewModel
                           {
                               Dsh = km.Dsh
                           }).ToList();
            }
            else
            {
                modelKm = (from km in _SprSkmRepository.Table
                           where km.OgtId == OgtId && km.NmSkmId == nmMaterId && km.MarkaId == markaMaterId && km.GostId == GostId
                           select new SkmViewModel
                           {
                               Dsh = km.Dsh
                           }).ToList();
            }
            return Json(modelKm.Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingMaterialDbtRead(string Dbt)
        {
            var MarkaSkmCookie = this.httpContext.Request.Cookies.Get(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            string MarkaSkm = null;

            var NmSkmCookie = this.httpContext.Request.Cookies.Get(NAIM_SKM_COOKIE_KEY);
            string NmSkm = null;

            var GostSkmCookie = this.httpContext.Request.Cookies.Get(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            string GostSkm = null;

            int GostId = 1;
            if (GostSkmCookie != null)
            {
                GostSkm = GostSkmCookie.Value;
                GostId = _IGostMaterService.GetIdFromGost(GostSkm);
            }
            int nmMaterId = 1;
            if (NmSkmCookie != null)
            {
                NmSkm = NmSkmCookie.Value;
                nmMaterId = _INmMaterService.GetIdFromNameMater(NmSkm);
            }
            int markaMaterId = 1;
            if (MarkaSkmCookie != null)
            {
                MarkaSkm = MarkaSkmCookie.Value;
                markaMaterId = _IMarkaMaterialService.GetIdFromNameMarkMater(MarkaSkm);
            }
            List<SkmViewModel> modelKm = new List<SkmViewModel>();
            var OgtCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);

            var OgtId = int.Parse(OgtCookie.Value);
            if (!String.IsNullOrEmpty(Dbt) || !String.IsNullOrWhiteSpace(Dbt))
            {
                modelKm = (from km in _SprSkmRepository.Table
                           where km.OgtId == OgtId && km.Dbt.Contains(Dbt) && km.NmSkmId == nmMaterId && km.MarkaId == markaMaterId && km.GostId == GostId
                           select new SkmViewModel
                           {
                               Dbt = km.Dbt
                           }).ToList();
            }
            else
            {
                modelKm = (from km in _SprSkmRepository.Table
                           where km.OgtId == OgtId && km.NmSkmId == nmMaterId && km.MarkaId == markaMaterId && km.GostId == GostId
                           select new SkmViewModel
                           {
                               Dbt = km.Dbt
                           }).ToList();
            }
            return Json(modelKm.Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingKmRead(string Km)
        {
            var MarkaSkmCookie = this.httpContext.Request.Cookies.Get(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            string MarkaSkm = null;

            var NmSkmCookie = this.httpContext.Request.Cookies.Get(NAIM_SKM_COOKIE_KEY);
            string NmSkm = null;

            var GostSkmCookie = this.httpContext.Request.Cookies.Get(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            string GostSkm = null;

            int GostId = 1;
            if (GostSkmCookie != null)
            {
                GostSkm = GostSkmCookie.Value;
                GostId = _IGostMaterService.GetIdFromGost(GostSkm);
            }
            int nmMaterId = 1;
            if (NmSkmCookie != null)
            {
                NmSkm = NmSkmCookie.Value;
                nmMaterId = _INmMaterService.GetIdFromNameMater(NmSkm);
            }
            int markaMaterId = 1;
            if (MarkaSkmCookie != null)
            {
                MarkaSkm = MarkaSkmCookie.Value;
                markaMaterId = _IMarkaMaterialService.GetIdFromNameMarkMater(MarkaSkm);
            }
            List<SkmViewModel> modelKm = new List<SkmViewModel>();
            var OgtCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);
            if (OgtCookie != null)
            {
                var OgtId = int.Parse(OgtCookie.Value);

                modelKm = (from km in _SprSkmRepository.Table
                           where km.Id != 1 && km.OgtId == OgtId && km.Km.Contains(Km) && km.NmSkmId == nmMaterId && km.MarkaId == markaMaterId && km.GostId == GostId
                           join ogtRepo in _SprOgtRepository.Table
                           on km.OgtId equals ogtRepo.Id
                           join nmSkmRepo in _DirectoryOfMaterialNameRepository.Table
                           on km.NmSkmId equals nmSkmRepo.Id
                           select new SkmViewModel
                           {
                               Km = km.Km.Substring(0, ogtRepo.KsimKm)
                           })
                           .GroupBy(s => s.Km)
                           .Select(gr => gr.FirstOrDefault())
                           .OrderBy(s => s.Km)
                           .ToList();
            }
            else
            {
                modelKm = (from km in _SprSkmRepository.Table
                           select new SkmViewModel
                           {
                               Km = km.Km
                           })
                          .GroupBy(s => s.Km)
                          .Select(gr => gr.FirstOrDefault())
                          .OrderBy(s => s.Km)
                          .ToList();
            }
            return Json(modelKm.Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingSortamentRead()
        {
            List<AssotmentReferenceViewModel> modelSortament = new List<AssotmentReferenceViewModel>();
            modelSortament = (from Sortament in _SprSortamRepository.Table
                              where Sortament.Id != 1
                              select new AssotmentReferenceViewModel
                              {
                                  Id = Sortament.Id,
                                  Sortament = Sortament.Sortament,
                              }).ToList();
            return Json(modelSortament, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingBalSchRead()
        {
            List<DirectoryOfBalanceAccountsViewModel> modelBalsch = new List<DirectoryOfBalanceAccountsViewModel>();

            modelBalsch = (from bal in _SprBalSchRepository.Table
                           where bal.Id != 1
                           select new DirectoryOfBalanceAccountsViewModel
                           {
                               Id = bal.Id,
                               BalSch = bal.BalSchet.ToString()
                           }).ToList();
            return Json(modelBalsch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingGrMaterRead()
        {
            List<DirectoryOfGroupMaterViewModel> modelGrMater = new List<DirectoryOfGroupMaterViewModel>();

            modelGrMater = (from grMater in _SprGrMaterRepository.Table
                            where grMater.Id != 1
                            select new DirectoryOfGroupMaterViewModel
                            {
                                Id = grMater.Id,
                                GrMater = grMater.NmGrMater,
                                NomerGrMater = grMater.NomerGrMater.ToString()
                            }).ToList();
            return Json(modelGrMater, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingOgtRead()
        {
            List<DirectoryOfOgtViewModel> modelOgt = new List<DirectoryOfOgtViewModel>();

            modelOgt = (from ogt in _SprOgtRepository.Table
                        where ogt.Id != 1
                        select new DirectoryOfOgtViewModel
                        {
                            Id = ogt.Id,
                            OGT = ogt.OGT.ToString(),
                            NaimOgt = ogt.NaimOgt.ToString(),
                            KsimKm = ogt.KsimKm.ToString()
                        }).ToList();
            return Json(modelOgt, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingOtsRead()
        {
            List<DirectoryOfOtsViewModel> modelOts = new List<DirectoryOfOtsViewModel>();

            modelOts = (from ots in _SprOtsRepository.Table
                        where ots.Id != 1
                        select new DirectoryOfOtsViewModel
                        {
                            Id = ots.Id,
                            KodSklad = ots.KodSklad.ToString(),
                        }).ToList();
            return Json(modelOts, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingMaterialEizmRead()
        {
            List<DirectoryOfUnitsOfMeasurementViewModel> modelEizmMater = new List<DirectoryOfUnitsOfMeasurementViewModel>();

            modelEizmMater = (from eizm in _SprEizmRepository.Table
                              where eizm.Id != 1
                              select new DirectoryOfUnitsOfMeasurementViewModel
                              {
                                  Id = eizm.Id,
                                  KratNaimEizm = eizm.KratNaimEizm,
                                  PolnNaimEizm = eizm.PolnNaimEizm
                              }).ToList();
            return Json(modelEizmMater, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingMaterialKgrRead()
        {
            var modelkgr = (from kgr in _SprKgrRepository.Table
                            where kgr.Id != 1
                            select new DirectoryOfKgrViewModel
                            {
                                Id = kgr.Id,
                                Kgr = kgr.Kgr.ToString(),
                            }).ToList();
            return Json(modelkgr, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AdaptiveMode_GetProducts([DataSourceRequest] DataSourceRequest request, string name)
        {
            List<DocumentStatus> statusFilter = new List<DocumentStatus>();
            var statusList = _DocumentStatusRepository.Table.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                statusFilter = statusList.Where(p => p.Status.Contains(name)).ToList();
            }

            //var single = statusList.All(s => s.Status == name);
            return Json(statusFilter, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingMaterialCodeRead()
        {
            var statusList = _DocumentStatusRepository.Table;
            return Json(statusList, JsonRequestBehavior.AllowGet);
        }
        [Route("EditorNameMaterial")]
        public ActionResult ComboBoxMemoAddingMaterialMarkaRead()
        {
            List<MarkMaterViewModel> modelMarkMater = new List<MarkMaterViewModel>();
            var markaSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_MARKA_ID_COOKIE_KEY);
            if (markaSkmCookie != null)
            {
                var markaSkmCookieValueParts = markaSkmCookie.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> markaSkmIdList = markaSkmCookieValueParts.Select(s => int.Parse(s)).ToList();

                modelMarkMater = (from mat in _MarkMaterRepository.Table
                                  where markaSkmIdList.Contains(mat.Id)
                                  select new MarkMaterViewModel
                                  {
                                      Id = mat.Id,
                                      MarkaMater = mat.MarkaMater,
                                  }).ToList();
            }
            else
            {
                modelMarkMater = (from mat in _MarkMaterRepository.Table
                                  select new MarkMaterViewModel
                                  {
                                      Id = mat.Id,
                                      MarkaMater = mat.MarkaMater,
                                  }).ToList();
            }
            return Json(modelMarkMater, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingMaterialNameRead()
        {
            List<DirectoryOfMaterialNameViewModel> nmSkmList = new List<DirectoryOfMaterialNameViewModel>();

            var NmSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_NAIM_ID_COOKIE_KEY);
            //var CookieValue = NmSkmCookie.Value;

            if (NmSkmCookie != null)
            {
                var NmSkmCookieValueParts = NmSkmCookie.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> NmSkmIdList = NmSkmCookieValueParts.Select(s => int.Parse(s)).ToList();
                nmSkmList = (from mat in _DirectoryOfMaterialNameRepository.Table
                             where mat.Id != 1 && NmSkmIdList.Contains(mat.Id)
                             select new DirectoryOfMaterialNameViewModel
                             {
                                 Id = mat.Id,
                                 nm_mater1 = mat.NameMaterial
                             }).ToList();

            }
            else
            {
                nmSkmList = (from mat in _DirectoryOfMaterialNameRepository.Table
                             where mat.Id != 1
                             select new DirectoryOfMaterialNameViewModel
                             {
                                 Id = mat.Id,
                                 nm_mater1 = mat.NameMaterial
                             }).ToList();
            }
            return Json(nmSkmList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingPviRead()
        {
            var Pvi2 = _Spr_pviRepository.Table.ToList();

            var Pvi = _Spr_pviRepository.Table;
            return Json(Pvi, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComboBoxMemoAddingMaterialGostRead()
        {
            var gostCookie = this.httpContext.Request.Cookies.Get(FILTER_GOST_ID_COOKIE_KEY);
            var gostCookieValueParts = gostCookie.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> GostIdList = gostCookieValueParts.Select(s => int.Parse(s)).ToList();


            List<DirectoryOfGostMaterialViewModel> modelGostMater = new List<DirectoryOfGostMaterialViewModel>();

            modelGostMater = (from gost in _GostMaterRepository.Table
                              where GostIdList.Contains(gost.Id)
                              select new DirectoryOfGostMaterialViewModel
                              {
                                  Id = gost.Id,
                                  Gost = gost.Gost
                              }).ToList();
            return Json(modelGostMater, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMemoAddingMaterialCodeKendoVersion()
        {
            return View();
        }
        public ActionResult Overview_GetCountries()
        {
            List<SelectListItem> countries = GetData();

            return Json(countries, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MemoAddingMaterialCodeRead([DataSourceRequest] DataSourceRequest request)
        {
            List<GostMaterGridViewModel> gostCodeViewModels = new List<GostMaterGridViewModel>();

            gostCodeViewModels = (from gost in _GostMaterRepository.Table
                                  join status in _DocumentStatusRepository.Table
                                  on gost.StatusDocumentId equals status.Id
                                  select new GostMaterGridViewModel
                                  {
                                      Id = gost.Id,
                                      Gost = gost.Gost,
                                      StatusDocument = status.Status,
                                  }).ToList();
            return Json(gostCodeViewModels.ToDataSourceResult(request));
        }
        public ActionResult AddMemoAddingMaterialCodeGridViewPartial(GostMaterGridViewModel model)
        {
            return View();
        }
        private static List<SelectListItem> GetData()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Value = "1", Text = "Lisboa"},
                new SelectListItem{ Value = "2", Text = "Moscow"},
                new SelectListItem{ Value = "3", Text = "Napoli"},
                new SelectListItem{ Value = "4", Text = "Tokyo"},
                new SelectListItem{ Value = "5", Text = "Oslo"},
                new SelectListItem{ Value = "6", Text = "Pаris"},
                new SelectListItem{ Value = "7", Text = "Porto"},
                new SelectListItem{ Value = "8", Text = "Rome"},
                new SelectListItem{ Value = "9", Text = "Berlin"},
                new SelectListItem{ Value = "10",Text = "Nice"},
                new SelectListItem{ Value = "11",Text = "New York"},
                new SelectListItem{ Value = "12",Text = "Sao Paulo"},
                new SelectListItem{ Value = "13",Text = "Rio De Janeiro"},
                new SelectListItem{ Value = "14",Text = "Venice"},
                new SelectListItem{ Value = "15",Text = "Los Angeles"},
                new SelectListItem{ Value = "16",Text = "Madrid"},
                new SelectListItem{ Value = "17",Text = "Barcelona"},
                new SelectListItem{ Value = "18",Text = "Prague"},
                new SelectListItem{ Value = "19",Text = "Mexico City"},
                new SelectListItem{ Value = "20",Text = "Buenos Aires"}
            };
        }
        [HttpPost]
        public ActionResult NaimSkmAfterChange(string naimskm)
        {
            var OgtCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);
            var OgtId = int.Parse(OgtCookie.Value);
            var FilterMarkaId = _SprSkmRepository.Table.Where(x => x.DirectoryOfMaterialName.NameMaterial == naimskm && x.OgtId == OgtId).Select(s => s.MarkaId).ToList().Distinct();
            var FilterGostId = _SprSkmRepository.Table.Where(x => x.DirectoryOfMaterialName.NameMaterial == naimskm && x.OgtId == OgtId).Select(s => s.GostId).ToList().Distinct();
            var listSkmAfterInputNmSkm = _SprSkmRepository.Table.Where(x => x.DirectoryOfMaterialName.NameMaterial == naimskm && x.OgtId == OgtId).ToList().Distinct();

            var resultFilterMarkaId = string.Join("|", FilterMarkaId);
            _SkmHelper.SetMarkaSkmIdToCookies(resultFilterMarkaId);

            var resultFilterGostId = string.Join("|", FilterGostId);
            _SkmHelper.SetGostSkmIdToCookies(resultFilterGostId);

            _SkmHelper.SetNaimSkmToCookies(naimskm);
            return null;
        }
        [HttpPost]
        public ActionResult MarkaSkmAfterChange(string markaskm)
        {
            var OgtCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);
            var NmSkmCookie = this.httpContext.Request.Cookies.Get(NAIM_SKM_COOKIE_KEY);
            var Ogt = int.Parse(OgtCookie.Value);
            var NmSkm = NmSkmCookie.Value;
            var FilterGostId = _SprSkmRepository.Table.Where(x => x.DirectoryOfMaterialName.NameMaterial == NmSkm && x.OgtId == Ogt && x.MarkMater.MarkaMater == markaskm).Select(s => s.GostId).ToList().Distinct();
            var resultFilterGostId = string.Join("|", FilterGostId);
            _SkmHelper.SetGostSkmIdToCookies(resultFilterGostId);
            _SkmHelper.SetMarkaSkmIdToCookiesAfterChange(markaskm);
            //var listSkmAfterInputNmSkm = _SprSkmRepository.Table.Where(x => x.DirectoryOfMaterialName.NameMaterial == naimskm && x.OgtId == OgtId).ToList().Distinct();
            return null;
        }
        [HttpPost]
        public ActionResult GostAfterChange(string gost)
        {
            _SkmHelper.SetGostSkmIdToCookiesAfterChange(gost);
            return null;
        }
        public ActionResult GetTabStrip(int szId)
        {
            return View();
        }
        [Authorize]
        public ActionResult Index(SprSkm model, string resetPassword)
        {
            if (resetPassword != null)
            {
                Session["skm_parametr"] = null;
                return View();
            }
            if (model.Km != null || model.PeriodOpenDate != null)
            {
                Session["skm_parametr"] = model;
                return View(model);
            }
            else
                return View();
        }
        public ActionResult MainPageSkmGridViewPartial()
        {
            GetTempDataForSkm();
            var parametr = Session["skm_parametr"] as SprSkm;
            var model = _directoryOfMaterialCodifiersService.GetAllKm(parametr);
            return PartialView("_MainPageSkmGridViewPartial", model);
        }
        public ActionResult GetCenMater(int cenaId)
        {
            var parametr = Session["skm_parametr"] as SprSkm;
            var description = _directoryOfMaterialCodifiersService.GetAllKm(parametr).Where(x => x.Id == cenaId).Select(x => x.Km).FirstOrDefault()
                + ";" + _directoryOfMaterialCodifiersService.GetAllKm(parametr).Where(x => x.Id == cenaId).Select(x => x.DirectoryOfMaterialName.NameMaterial).FirstOrDefault().ToString()
                + ";" + _directoryOfMaterialCodifiersService.GetAllKm(parametr).Where(x => x.Id == cenaId).Select(x => x.MarkMater.MarkaMater).FirstOrDefault().ToString()
                + ";" + _directoryOfMaterialCodifiersService.GetAllKm(parametr).Where(x => x.Id == cenaId).Select(x => x.GostMater.Gost).FirstOrDefault().ToString();
            ViewData["DescriptionKm"] = " (" + description + ")";
            return View();
        }
        public ActionResult GridViewPartialCenMater(int cenaId)
        {
            if (cenaId != 0)
            {
                var model = _sprCenMaterService.GetAllCenMaterToList().Where(x => x.Id == cenaId).ToList();
                ViewData["cena_mater"] = _sprCenMaterService.GetAllCenMaterToList().Where(x => x.Id == cenaId).Select(c => c.Id).FirstOrDefault();
                return PartialView("_CenMaterGridViewPartial", model);
            }
            return PartialView("_MainPageSkmGridViewPartial");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SKM_GridViewPartialAddNew(SprSkm model)
        {
            GetTempDataForSkm();
            var parametr = Session["skm_parametr"] as SprSkm;
            var anyKM = _directoryOfMaterialCodifiersService.GetAllKm(parametr).Where(x => x.Km.Trim() == model.Km.Trim()).ToList();
            if (anyKM.Count > 0)
            {
                ViewData["Success_KM"] = "Данная запись уже существует";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    model.CustomerId = _workContext.CurrentCustomer.Id;
                    model.OperationDate = DateTime.Now;
                    model.PeriodOpenDate = DateTime.Now;
                    model.Spr_pviId = (int)PviLevel.Insert;
                    model.DocumentStatusId = _DocumentStatusRepository.Table.Where(p => p.Status.Contains("Действует")).Select(p => p.Id).FirstOrDefault();
                    _SprSkmRepository.Insert(model);
                    ViewData["Success_KM"] = "Запись добавлена";
                }
                return PartialView("_MainPageSkmGridViewPartial", _directoryOfMaterialCodifiersService.GetAllKm(parametr));
            }
            return PartialView("_MainPageSkmGridViewPartial", _directoryOfMaterialCodifiersService.GetAllKm(parametr));
        }
        public ActionResult SKM_GridViewPartialDelete(int Id)
        {
            GetTempDataForSkm();
            var parametr = Session["skm_parametr"] as SprSkm;
            var model = _SprSkmRepository.Table;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                    {
                        _SprSkmRepository.Delete(item);
                        ViewData["Success_KM"] = "Запись успешно удалена";
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
                return PartialView("_MainPageSkmGridViewPartial", _directoryOfMaterialCodifiersService.GetAllKm(parametr));
            }
            return PartialView("_MainPageSkmGridViewPartial", _directoryOfMaterialCodifiersService.GetAllKm(parametr));
        }
        public void GetTempDataForSkm()
        {
            ViewBag.OGT = _OgtService.GetAllNaimOgt();
            ViewBag.ListStatus = _DocumentStatusService.GetAllStatusList();
            ViewBag.PrKm = _directoryOfMaterialCodifiersService.GetAllPrKms();
            ViewBag.NmSkm = _nmMaterService.GetAllNameMaterList();
            ViewBag.NmMarka = _markaMaterialService.GetAllMarkMaterList();
            ViewBag.GostMater = _gostMaterService.GetAllGostMaterList();
            ViewBag.NmEizm = _unitOfMeasurementService.GetAllUnitOfMeasurementList();
            ViewBag.NmGrMater = _grMaterService.GetAllGrMaterList();
            ViewBag.NoKgr = _sprKgrService.GetAllKgrToList();
            ViewBag.Ots = _otsService.GetAllNaimOts();
        }
        public ActionResult GetMemorandumBase()
        {
            return View();
        }
        public ActionResult GetMemorandumBaseJs([DataSourceRequest] DataSourceRequest request)
        {
            var memorandumBase = _memorandumBaseRepository.Table.ToList();
            return Json(memorandumBase, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTabStripDetail(int id)
        {
            return View();
        }
    }
}






