using System.Web.Mvc;
using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Services.Common;
using Asu.Services;
using Asu.Services.SprPkp;
using Asu.Mapping.Skm;
using Asu.Mapping.DocumentStatusService;
using Asu.Core.Domain.Blogs;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Localization;
using Asu.Core.Domain.Media;
using Asu.Services.Blogs;
using Asu.Services.Helpers;
using Asu.Services.Localization;
using Asu.Services.Logging;
using Asu.Services.Media;
using Asu.Services.Messages;
using Asu.Services.Stores;
using Asu.Core.Caching;
using Asu.Framework.UI.Captcha;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_skmController : Controller
    {
        const string CurrentTask = "Справочник Кодификатор Материалов";

        private readonly IWorkContext _workContext;
        private readonly IDocumentStatus _DocumentStatusService;
        private readonly IOgtService _OgtService;
        private readonly IDirectoryOfMaterialCodifiersService _directoryOfMaterialCodifiersService;
        private readonly IBlogService _blogService;
        private readonly IStoreContext _storeContext;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IWebHelper _webHelper;
        private readonly ICacheManager _cacheManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly MediaSettings _mediaSettings;
        private readonly BlogSettings _blogSettings;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly INmMaterService _nmMaterService;
        private readonly IMarkaMaterialService _markaMaterialService;
        private readonly IGostMaterService _gostMaterService;
        private readonly IUnitOfMeasurementService _unitOfMeasurementService;
        private readonly IGrMaterService _grMaterService;
        private readonly ISprKgrService _sprKgrService;
        private readonly IOtsService _otsService;
        

        public Spr_skmController(IWorkContext workContext, IDirectoryOfMaterialCodifiersService directoryOfMaterialCodifiersService, IDocumentStatus DocumentStatusService,
            IBlogService blogService, INmMaterService nmMaterService, IMarkaMaterialService markaMaterialService, IWebHelper webHelper, IGostMaterService gostMaterService,
            IStoreContext storeContext, IPictureService pictureService, IWorkflowMessageService workflowMessageService, ICacheManager cacheManager, IUnitOfMeasurementService unitOfMeasurementService,
            ILocalizationService localizationService, IDateTimeHelper dateTimeHelper, ISprKgrService sprKgrService, IOtsService otsService,
            ICustomerActivityService customerActivityService,
            IStoreMappingService storeMappingService,
            MediaSettings mediaSettings,
            BlogSettings blogSettings,
            LocalizationSettings localizationSettings,
            CustomerSettings customerSettings,
            CaptchaSettings captchaSettings, IOgtService ogtService, IGrMaterService grMaterService)
        {
            _workContext = workContext;
            _directoryOfMaterialCodifiersService = directoryOfMaterialCodifiersService;
            _DocumentStatusService = DocumentStatusService;
            this._blogService = blogService;
            this._storeContext = storeContext;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._dateTimeHelper = dateTimeHelper;
            this._workflowMessageService = workflowMessageService;
            this._webHelper = webHelper;
            this._cacheManager = cacheManager;
            this._customerActivityService = customerActivityService;
            this._storeMappingService = storeMappingService;
            this._mediaSettings = mediaSettings;
            this._blogSettings = blogSettings;
            this._localizationSettings = localizationSettings;
            this._customerSettings = customerSettings;
            this._captchaSettings = captchaSettings;
            _OgtService = ogtService;
            _nmMaterService = nmMaterService;
            _markaMaterialService = markaMaterialService;
            _gostMaterService = gostMaterService;
            _unitOfMeasurementService = unitOfMeasurementService;
            _grMaterService = grMaterService;
            _sprKgrService = sprKgrService;
            _otsService = otsService;
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
        public ActionResult SKM_GridViewPartial()
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
            var parametr = Session["skm_parametr"] as SprSkm;
            var model = _directoryOfMaterialCodifiersService.GetAllKm(parametr);
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost]
        public ActionResult SKM_GridViewPartialAddNew(SprSkm model)
        {
            return View();
        }

    }
}






