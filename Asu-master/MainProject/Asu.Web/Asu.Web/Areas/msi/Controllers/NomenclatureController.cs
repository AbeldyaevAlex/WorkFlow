using Asu.Core.Data;
using Asu.Core.Domain.Tasks;
using Asu.Mapping.Msi;
using Asu.Web.Models;
using Asu.Web.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class NomenclatureController : Controller
    {
        private readonly ISprTemService _sprTemService;
        private readonly ISprPerizdService _sprPerizdService;
        private readonly ISprMashService _sprMashService;
        private readonly IDerIzdService _derIzdService;
        private readonly ITreeProductService _treeProductService;
        private readonly Isrez_sostoyanieService _srez_sostoyanieService;
        private readonly IRepository<UsersTasks> _UsersTasksRepository;
        private readonly IWorkShopService _workShopService;

        public NomenclatureController(ISprTemService sprTemService, ISprPerizdService sprPerizdService, ISprMashService sprMashService, IDerIzdService derIzdService,
            ITreeProductService treeProductService, Isrez_sostoyanieService srez_sostoyanieService, IRepository<UsersTasks> UsersTasksRepository, IWorkShopService workShopService)
        {
            _sprTemService = sprTemService;
            _sprPerizdService = sprPerizdService;
            _sprMashService = sprMashService;
            _derIzdService = derIzdService;
            _treeProductService = treeProductService;
            _srez_sostoyanieService = srez_sostoyanieService;
            _UsersTasksRepository = UsersTasksRepository;
            _workShopService = workShopService;
        }
        public ActionResult GetParameterNomenclature()
        {
            var Conditions = GetCondition();
            var model = new SubjectAndProductsViewModel { AvaliableConditions = Conditions };
            var listofTheme = _sprTemService.GetAllListTem().Where(x => x.Id > 1);
            ViewBag.ListTem = new SelectList(listofTheme, "Id", "Nm_tem_k");
            return View(model);
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
        [HttpPost]
        public ActionResult GetNomenclature(SubjectAndProductsViewModel model)
        {
            var product = _sprPerizdService.GetAllistIzd().Where(x => x.Id == model.ProductId).Select(z => z.Izdelie).FirstOrDefault();           
            var ser_s = int.Parse(_sprMashService.GetAllListMash().Where(x => x.NomMash == model.Series).Select(z => z.Ser_s).FirstOrDefault());
            var ser_spo = int.Parse(_sprMashService.GetAllListMash().Where(x => x.NomMash == model.Series).Select(z => z.Ser_po).FirstOrDefault());

            List<NomenclatureViewModel> nomenclature = new List<NomenclatureViewModel>();
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            using (var connection = new SqlConnection(settings.DataConnectionString))
            {
                using (var cmd = new SqlCommand("GettingNomenclatureFromTreeProductTreeProduct", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter SsParameter = new SqlParameter("@paramSs", ser_s);
                    SsParameter.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(SsParameter);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter SpoParameter = new SqlParameter("@paramSpo", ser_spo);
                    SsParameter.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(SpoParameter);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter IzdIdParameter = new SqlParameter("@paramIzdId", model.ProductId);
                    SsParameter.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(IzdIdParameter);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter InventIdParameter = new SqlParameter("@paramInventId", model.srez_sostoyanieId);
                    SsParameter.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(InventIdParameter);
                    connection.Open();

                    cmd.CommandTimeout = 120;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            nomenclature.Add(new NomenclatureViewModel()
                            {
                                NomenclatureObozn = sdr.IsDBNull(0) ? "" : sdr.GetString(0),
                                Pkp = sdr.IsDBNull(1) ? "" : sdr.GetString(1),
                                Ss = sdr.IsDBNull(2) ? "" : sdr.GetString(2),
                                Spo = sdr.IsDBNull(3) ? "" : sdr.GetString(3),
                                Ksb = sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4),
                                Kizd = sdr.IsDBNull(5) ? 0 : sdr.GetInt32(5),
                                NameDet = sdr.IsDBNull(6) ? "" : sdr.GetString(6),
                                OboznNaim = sdr.IsDBNull(7) ? "" : sdr.GetString(7),
                                Tk1 = sdr.IsDBNull(8) ? "" : sdr.GetString(8),
                                Tk2 = sdr.IsDBNull(9) ? "" : sdr.GetString(9),
                                Tk3 = sdr.IsDBNull(10) ? "" : sdr.GetString(10),
                                Mas1sh = sdr.IsDBNull(11) ? 0 : sdr.GetDecimal(11),
                                Masizd = sdr.IsDBNull(12) ? 0 : sdr.GetDecimal(12),
                                Kp1 = sdr.IsDBNull(13) ? 0 : sdr.GetInt32(13),
                                Kp2 = sdr.IsDBNull(14) ? 0 : sdr.GetInt32(14),
                                Kp3 = sdr.IsDBNull(15) ? 0 : sdr.GetInt32(15),
                                RascexPoln = sdr.IsDBNull(16) ? "" : sdr.GetString(16),
                                PviId = sdr.IsDBNull(17) ? 0 : sdr.GetInt32(17),
                                NaimIzd = sdr.IsDBNull(18) ? "" : sdr.GetString(18),
                                PrimKonstructor = sdr.IsDBNull(19) ? "" : sdr.GetString(19),
                                PrimTehnolog = sdr.IsDBNull(20) ? "" : sdr.GetString(20),
                                PrimPrinadlegn = sdr.IsDBNull(21) ? "" : sdr.GetString(21),
                                PrimIzmenChast = sdr.IsDBNull(22) ? "" : sdr.GetString(22),
                                NmRazdIzd = sdr.IsDBNull(23) ? "" : sdr.GetString(23),
                                NmGroup = sdr.IsDBNull(24) ? "" : sdr.GetString(24),
                                AgregateObozn = sdr.IsDBNull(25) ? "" : sdr.GetString(25),
                                GroupAgregate = sdr.IsDBNull(26) ? "" : sdr.GetString(26),
                                Komplekt = sdr.IsDBNull(27) ? "" : sdr.GetString(27),
                                Status = sdr.IsDBNull(28) ? "" : sdr.GetString(28),
                                PeriodOpenDate = sdr.IsDBNull(29) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(29),
                                PeriodCloseDate = sdr.IsDBNull(30) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(30),
                                Condition = sdr.IsDBNull(31) ? "" : sdr.GetString(31)
                            });
                        }
                    }
                    connection.Close();
                }
            }

            ViewData["ProductSeries"] = product + " (" + model.Series + ")";
            Session["Nomenclature"] = nomenclature;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GetNomenclatureFromTreeProductPartialView([DataSourceRequest] DataSourceRequest request)
        {
            var nomenclature = Session["Nomenclature"] as List<NomenclatureViewModel>;

            return PartialView("~/Areas/msi/Views/Nomenclature/_GetNomenclatureFromTreeProductPartialView.cshtml", nomenclature);
        }
        public IList<SelectListItem> GetCondition()
        {
            var listParamInvent = _srez_sostoyanieService.GetAllListParameterInvent();
            var conditions = listParamInvent.Select(x => new SelectListItem { Text = x.sostoyanie, Value = x.Id.ToString() }).ToList();
            return conditions;
        }
    }
}