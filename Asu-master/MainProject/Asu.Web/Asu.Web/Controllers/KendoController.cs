using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core;
using Asu.Mapping.Skm;
using Asu.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Core.Data;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.Msi;
using Asu.Web.ViewModel;

namespace Asu.Web.Controllers
{
    public class KendoController : Controller
    {
        private readonly IRepository<SprSkm> _SprSkmRepository;
        private readonly IRepository<MemoAddingMaterialCode> _MemoAddingMaterialCodeRepository;
        private readonly IRepository<DocumentStatus> _DocumentStatusRepository;
        private readonly IRepository<DirectoryOfMaterialName> _DirectoryOfMaterialNameRepository;
        private readonly IRepository<MarkMater> _MarkMaterRepository;
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


        public KendoController(IRepository<MemoAddingMaterialCode> MemoAddingMaterialCodeRepository,
            IRepository<DocumentStatus> DocumentStatusRepository,
            IRepository<DirectoryOfMaterialName> directoryOfMaterialNameRepository,
            IRepository<MarkMater> markMaterRepository,
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
            IGostMaterService IGostMaterService)

        {
            _MemoAddingMaterialCodeRepository = MemoAddingMaterialCodeRepository;
            _DocumentStatusRepository = DocumentStatusRepository;
            _DirectoryOfMaterialNameRepository = directoryOfMaterialNameRepository;
            _MarkMaterRepository = markMaterRepository;
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
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 10).Select(i => new OrderViewModel2
            {
                //OrderID = i,
                //Freight = i * 10,
                //OrderDate = DateTime.Now.AddDays(i),
                ShipName = "ShipName " + i,
            });

            return Json(result.ToDataSourceResult(request));
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
    }
}