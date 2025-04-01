using Asu.Core.Data;
using Asu.Core.Domain.Tasks;
using Asu.Mapping.Msi;
using Asu.Web.Models;
using Asu.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Core;
using Asu.Core.Domain.Customers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Asu.Core.Domain.Work;
using DevExpress.Utils.Extensions;
using System.Threading.Tasks;
using System.Net.Http;

namespace Asu.Web.Areas.msi.Controllers
{
    public class WorkshopPortfolioController : Controller
    {
        private readonly ISprTemService _sprTemService;
        private readonly ISprPerizdService _sprPerizdService;
        private readonly ISprMashService _sprMashService;
        private readonly IDerIzdService _derIzdService;
        private readonly ITreeProductService _treeProductService;
        private readonly Isrez_sostoyanieService _srez_sostoyanieService;
        private readonly IRepository<UsersTasks> _UsersTasksRepository;
        private readonly IWorkShopService _workShopService;
        private readonly IRepository<ExceptionForWork> _ExceptionForWorkRepository;
        private readonly IWorkContext workContext;
        private readonly IRepository<DirectiveWork> _directiveWorkRepository;
        public WorkshopPortfolioController(ISprTemService sprTemService, ISprPerizdService sprPerizdService, ISprMashService sprMashService, IDerIzdService derIzdService,
         ITreeProductService treeProductService, Isrez_sostoyanieService srez_sostoyanieService, IRepository<UsersTasks> UsersTasksRepository, IWorkShopService workShopService, IWorkContext workContext, IRepository<ExceptionForWork> ExceptionForWorkRepository,
         IRepository<DirectiveWork> directiveWorkRepository)
        {
            _sprTemService = sprTemService;
            _sprPerizdService = sprPerizdService;
            _sprMashService = sprMashService;
            _derIzdService = derIzdService;
            _treeProductService = treeProductService;
            _srez_sostoyanieService = srez_sostoyanieService;
            _UsersTasksRepository = UsersTasksRepository;
            _workShopService = workShopService;
            this.workContext = workContext;
            _ExceptionForWorkRepository = ExceptionForWorkRepository;
            _directiveWorkRepository = directiveWorkRepository;
        }

        public JsonResult GetProductList(int SubjecId)
        {
            var ProductList = _sprPerizdService.GetAllistIzd().Where(x => x.TemaId == SubjecId && x.Id > 1);
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSeriaList(int ProductId, bool exception = true)
        {
            var seriaList = _sprMashService.GetAllListMash().Where(x => x.IzdId == ProductId && x.exception != exception);
            return Json(seriaList, JsonRequestBehavior.AllowGet);
        }
        public IList<SelectListItem> GetWorkShopCondition()
        {
            var listWorkSop = _workShopService.GetWorkShopList();
            var workshop = listWorkSop.Select(x => new SelectListItem { Text = x.NaimCex, Value = x.Id.ToString() }).ToList();
            return workshop;
        }
        public IList<SelectListItem> GetCondition()
        {
            var listParamInvent = _srez_sostoyanieService.GetAllListParameterInvent();
            var conditions = listParamInvent.Select(x => new SelectListItem { Text = x.sostoyanie, Value = x.Id.ToString() }).ToList();
            return conditions;
        }
        public IList<SelectListItem> GetTheme()
        {
            var listOfTheme = _sprTemService.GetAllListTem().Where(x => x.Id > 1).ToList();
            var conditions = listOfTheme.Select(x => new SelectListItem { Text = x.Nm_tem_p, Value = x.Id.ToString() }).ToList();
            return conditions;
        }
        [HttpGet]
        public ActionResult GetWorkshopPortfolioFromStoredProcedure()
        {
            var Conditions = GetCondition();
            var WorkShopConditions = GetWorkShopCondition();
            var ThemeConditions = GetTheme();
            var modelForDropDown = new SubjectAndProductsViewModel { AvaliableConditions = Conditions, AvaliableWorkShop = WorkShopConditions, ThemeList = ThemeConditions };
            return View(modelForDropDown);
        }
        public ActionResult GetWorkshopPortfolioFromTreeProduct()
        {
            return PartialView("~/Areas/msi/Views/WorkshopPortfolio/_GetWorkshopPortfolioFromTreeProductPartialKendoView.cshtml");
        }
        public ActionResult GetWorkshopPortfolioFromTreeProductPartialView([DataSourceRequest] DataSourceRequest request)
        {
            var workshopportfolio = Session["WorkShopPortfolio"] as List<WorkshopPortfolioViewModel>;
            return Json(workshopportfolio.ToDataSourceResult(request));
        }
        public ActionResult GetExeptionOfWork()
        {
            List<ExceptionForWorkVewModel> modelOgt = new List<ExceptionForWorkVewModel>();

            modelOgt = (from exp in _ExceptionForWorkRepository.Table
                        select new ExceptionForWorkVewModel
                        {
                            Id = exp.Id,
                            FullName = exp.FullName.ToString()
                        }).ToList();
            return Json(modelOgt, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateDirectiveWork(WorkshopPortfolioViewModel model, [DataSourceRequest] DataSourceRequest request)
        {
            var workshopportfolio = Session["WorkShopPortfolio"] as List<WorkshopPortfolioViewModel>;

            if (ModelState.IsValid)
            {
                var item = _directiveWorkRepository.Table.FirstOrDefault(o => o.Id == model.DirectiveWorkId);
                if (item != null)
                {
                    item.Directive_work_povr_izg = model.DirectiveWorkPovrIzgOnUnit;
                    item.Directive_work_povr_usl = model.DirectiveWorkPovrUslOnUnit;
                    item.Directive_work_sdeln_izg = model.DirectiveWorkSdelnIzgOnUnit;
                    item.Directive_work_sdeln_usl = model.DirectiveWorkSdelnUslOnUnit;
                    //item.ExceptionForWorkId = _ExceptionForWorkRepository.Table.Where(x => x.FullName == model.ExeptionOfWork).Select(k => k.Id).FirstOrDefault();
                    _directiveWorkRepository.Update(item);
                }
                var test = workshopportfolio.Where(x => x.DirectiveWorkId == model.DirectiveWorkId).FirstOrDefault();
                test.DirectiveWorkPovrIzgOnUnit = model.DirectiveWorkPovrIzgOnUnit;
                test.DirectiveWorkPovrUslOnUnit = model.DirectiveWorkPovrUslOnUnit;
                test.DirectiveWorkSdelnIzgOnUnit = model.DirectiveWorkSdelnIzgOnUnit;
                test.DirectiveWorkSdelnUslOnUnit = model.DirectiveWorkSdelnUslOnUnit;
            }
            return PartialView("~/Areas/msi/Views/WorkshopPortfolio/_GetWorkshopPortfolioFromTreeProductPartialKendoView.cshtml", workshopportfolio);
        }





        //public ActionResult GetWorkshopPortfolio()
        //{
        //    var Conditions = GetCondition();
        //    var WorkShopConditions = GetWorkShopCondition();
        //    var ThemeConditions = GetTheme();
        //    var model = new SubjectAndProductsViewModel { AvaliableConditions = Conditions, AvaliableWorkShop = WorkShopConditions, ThemeList = ThemeConditions };
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult GetWorkshopPortfolioFromStoredProcedure(SubjectAndProductsViewModel model)
        //{
        //    var worksopId = workContext.CurrentCustomer.IsAdmin() ? model.WorkShopId : workContext.CurrentCustomer.UsersWorkshop.Select(x => x.Id).FirstOrDefault();
        //    var product = _sprPerizdService.GetAllistIzd().Where(x => x.Id == model.ProductId).Select(z => z.Izdelie).FirstOrDefault();
        //    var ser_s = int.Parse(_sprMashService.GetAllListMash().Where(x => x.NomMash == model.Series).Select(z => z.Ser_s).FirstOrDefault());
        //    var ser_spo = int.Parse(_sprMashService.GetAllListMash().Where(x => x.NomMash == model.Series).Select(z => z.Ser_po).FirstOrDefault());

        //    List<WorkshopPortfolioViewModel> workshopPortfolio = new List<WorkshopPortfolioViewModel>();
        //    var manager = new DataSettingsManager();
        //    var settings = manager.LoadSettings();
        //    using (var connection = new SqlConnection(settings.DataConnectionString))
        //    {
        //        using (var cmd = new SqlCommand("GettingWorkshopPortfolioFromTreeProduct", connection))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlParameter SsParameter = new SqlParameter("@paramSs", ser_s);
        //            SsParameter.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(SsParameter);

        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlParameter SpoParameter = new SqlParameter("@paramSpo", ser_spo);
        //            SsParameter.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(SpoParameter);

        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlParameter IzdIdParameter = new SqlParameter("@paramIzdId", model.ProductId);
        //            SsParameter.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(IzdIdParameter);

        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlParameter InventIdParameter = new SqlParameter("@paramInventId", model.srez_sostoyanieId);
        //            SsParameter.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(InventIdParameter);


        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            SqlParameter WorkShopIdParameter = new SqlParameter("@WorkShopId", worksopId);
        //            SsParameter.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(WorkShopIdParameter);
        //            connection.Open();

        //            cmd.ExecuteNonQuery();

        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    workshopPortfolio.Add(new WorkshopPortfolioViewModel()
        //                    {
        //                        PotrtfolioObozn = sdr.IsDBNull(0) ? "" : sdr.GetString(0),
        //                        Pkp = sdr.IsDBNull(1) ? "" : sdr.GetString(1),
        //                        Ss = sdr.IsDBNull(2) ? "" : sdr.GetString(2),
        //                        Spo = sdr.IsDBNull(3) ? "" : sdr.GetString(3),
        //                        Kizd = sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4),
        //                        Name = sdr.IsDBNull(5) ? "" : sdr.GetString(5),
        //                        Mas1sh = sdr.IsDBNull(7) ? 0 : sdr.GetDecimal(7),
        //                        MasIzd = sdr.IsDBNull(8) ? 0 : sdr.GetDecimal(8),
        //                        //Kp1 = sdr.IsDBNull(9) ? 0 : sdr.GetInt32(9),
        //                        //Kp2 = sdr.IsDBNull(10) ? 0 : sdr.GetInt32(10),
        //                        //Kp3 = sdr.IsDBNull(11) ? 0 : sdr.GetInt32(11),
        //                        RascexPoln = sdr.IsDBNull(12) ? "" : sdr.GetString(12),
        //                        NameIzdel = sdr.IsDBNull(14) ? "" : sdr.GetString(14),
        //                        NameRazdIzd = sdr.IsDBNull(15) ? "" : sdr.GetString(15),
        //                        NameGroup = sdr.IsDBNull(16) ? "" : sdr.GetString(16),
        //                        Komplekt = sdr.IsDBNull(17) ? "" : sdr.GetString(17),
        //                        Status = sdr.IsDBNull(18) ? "" : sdr.GetString(18),
        //                        Condition = sdr.IsDBNull(19) ? "" : sdr.GetString(19),
        //                        Workshop = sdr.IsDBNull(24) ? "" : sdr.GetString(24),
        //                        ExeptionOfWork = sdr.IsDBNull(25) ? "" : sdr.GetString(25),
        //                        DirectiveWorkSdelnIzgOnUnit = sdr.IsDBNull(22) ? 0 : sdr.GetDecimal(22),
        //                        DirectiveWorkSdelnUslOnUnit = sdr.IsDBNull(23) ? 0 : sdr.GetDecimal(23),
        //                        DirectiveWorkPovrIzgOnUnit = sdr.IsDBNull(20) ? 0 : sdr.GetDecimal(20),
        //                        DirectiveWorkPovrUslOnUnit = sdr.IsDBNull(21) ? 0 : sdr.GetDecimal(21),
        //                        DirectiveWorkSdelnIzgOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(22) ? 0 : sdr.GetDecimal(22)),
        //                        DirectiveWorkSdelnUslOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(23) ? 0 : sdr.GetDecimal(23)),
        //                        DirectiveWorkPovrIzgOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(20) ? 0 : sdr.GetDecimal(20)),
        //                        DirectiveWorkPovrUslOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(21) ? 0 : sdr.GetDecimal(21)),
        //                        DirectiveWorkId = sdr.IsDBNull(26) ? 0 : sdr.GetInt32(26),
        //                    });
        //                }
        //            }
        //            connection.Close();
        //        }
        //    }
        //    Session["WorkShopPortfolio"] = workshopPortfolio;
        //    return View();
        //}
    }
}