using Asu.Core;
using Asu.Data;
using System.Web.Mvc;
using Asu.Core.Domain.Msi;
using Asu.Services.Logging;
using Asu.Services.Customers;
using Asu.Services.SprPkp;
using Asu.Core.Domain.Pvi;
using Asu.Services.Security;
using Asu.Services.Authentication;
using Asu.Core.Data;
using System.Data;
using Asu.Core.Domain.Customers;
using Asu.Services;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Mapping.DocumentStatusService;
using Asu.Services.UsersTasks;
using Asu.Mapping.TTO;
using Asu.Mapping.Skm;
using DocumentFormat.OpenXml;
using Asu.Services.Localization;
using System;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Logging;

namespace Asu.Web.Controllers
{
    public class SprCexServiceController : Controller
    {
        #region Fields

        private readonly IDbContext _context;
        private readonly IRepository<Spr_pkp> _pkpRepository;
        private readonly IRepository<Log> _logRepository;
        private readonly IRepository<Customer> _CustomerRepository;
        private readonly ILogger _logger;
        private readonly ISprPkpService _pkpService;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IStoreContext _storeContext;
        private readonly IDataProvider dataProvider;
        private readonly IRepository<SprSkm> _SprSkmRepository;
        private readonly IDocumentStatus _DocumentStatusService;
        private readonly IUserTaskService _UserTaskServiceService;
        private readonly IGostMaterService _GostMaterService;
        private readonly ITtoService _TtoService;
        private readonly IUslSkmService _UslSkmService;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;



        #endregion

        #region Ctor
        public SprCexServiceController(
               IDbContext context,
               IRepository<Spr_pkp> pkpRepository,
               ILogger logger,
               ISprPkpService pkpService,
               ICustomerService customerService,
               IWorkContext workContext,
               IPermissionService permissionService,
               IAuthenticationService authenticationService,
               IStoreContext storeContext, IDataProvider dataProvider,
               IRepository<SprSkm> SprSkmRepository,
               IDocumentStatus DocumentStatusService, IUserTaskService UserTaskServiceService, ITtoService TtoService, IUslSkmService UslSkmService,
               IGostMaterService GostMaterService, IWebHelper webHelper, ILocalizationService localizationService, IRepository<Log> logRepository, IRepository<Customer> customerRepository)
        {
            _context = context;
            _pkpRepository = pkpRepository;
            _logger = logger;
            _pkpService = pkpService;
            _customerService = customerService;
            _workContext = workContext;
            _permissionService = permissionService;
            _authenticationService = authenticationService;
            _storeContext = storeContext;
            this.dataProvider = dataProvider;
            _SprSkmRepository = SprSkmRepository;
            _DocumentStatusService = DocumentStatusService;
            _UserTaskServiceService = UserTaskServiceService;
            _GostMaterService = GostMaterService;
            _TtoService = TtoService;
            _UslSkmService = UslSkmService;
            _webHelper = webHelper;
            _localizationService = localizationService;
            _logRepository = logRepository;
            _CustomerRepository = customerRepository;
        }
        #endregion

        #region CRUD methods
        [ValidateInput(false)]
        public ActionResult Index()     
        {            
            var language = _workContext.WorkingLanguage.Id;
            var success =  _webHelper.IsCurrentConnectionSecured();

            foreach (ProductSortingEnum enumValue in Enum.GetValues(typeof(ProductSortingEnum)))
            {
                var sortValue = enumValue.GetLocalizedEnum(_localizationService, _workContext);

            }

            var url = _webHelper.GetThisPageUrl(false);

            var gostMater = _GostMaterService.GetAllGostMater();

            var uslskm = _UslSkmService.GetAllUsl();

            var tto = _TtoService.GetAllTto();
            
            
            var f = _pkpRepository.Table;
            var skm = _SprSkmRepository.Table;
            var i = _DocumentStatusService.GetAllStatus();
            var parametr = Session["skm_parametr"] as SprSkm;


            var customerIsAdmin = _workContext.CurrentCustomer.IsAdmin();
            var customerRole = _workContext.CurrentCustomer.CustomerRoles;
            var customerTask = _workContext.CurrentCustomer.UsersTask;
            var customer = _workContext.CurrentCustomer.ShippingAddress;


            var pParentCategoryId = this.dataProvider.GetParameter();
            pParentCategoryId.ParameterName = "ParentId";
            //pParentCategoryId.Value = parentCategoryId;
            pParentCategoryId.DbType = DbType.Int32;


            //if (!_permissionService.Authorize(StandardPermissionProvider.EnableShoppingCart))
            //    return RedirectToRoute("HomePage");
            var store = _storeContext.CurrentStore.Id;
            //var customer = _workContext.CurrentCustomer;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartialSprCexService()
        {
            return PartialView("_GridViewPartialSprCexService", _pkpService.GetAllPkp());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNewSprCexService(Spr_pkp item)
        {
            if (ModelState.IsValid)
            {
                item.DocumentStatusId = 1;
                item.CustomerId = _workContext.CurrentCustomer.Id;
                item.PviId = (int)PviLevel.Insert;
                 _pkpRepository.Insert(item);
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialSprCexService", _pkpService.GetAllPkp());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdateSprCexService(Spr_pkp item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pkp = _pkpService.GetPkpById(item.Id);
                    pkp.Pkp = item.Pkp;
                    pkp.NmPkp = item.NmPkp;
                    _pkpRepository.Update(pkp);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialSprCexService", _pkpService.GetAllPkp());
        }
        #endregion
    }
}