using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Customers;
using Asu.Mapping.Msi;
using Asu.Web.Models;
using Asu.Web.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Data.Common;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Asu.Core.Domain.Tasks;
using Asu.Core.Domain.Msi;
using DevExpress.XtraPrinting;
//using Kendo.Mvc.UI;
//using Kendo.Mvc.Extensions;

namespace Asu.Web.Controllers
{
    public class GetNodalizedArrayController : Controller
    {
        private readonly ISprTemService _sprTemService;
        private readonly ISprPerizdService _sprPerizdService;
        private readonly ISprMashService _sprMashService;
        private readonly IDerIzdService _derIzdService;
        private readonly ITreeProductService _treeProductService;
        private readonly Isrez_sostoyanieService _srez_sostoyanieService;
        private readonly IRepository<UsersTasks> _UsersTasksRepository;
        private readonly IRepository<TreeProduct> _TreeProductRepository;



        public GetNodalizedArrayController(ISprTemService sprTemService, ISprPerizdService sprPerizdService, ISprMashService sprMashService, IDerIzdService derIzdService,
            ITreeProductService treeProductService, Isrez_sostoyanieService srez_sostoyanieService, IRepository<UsersTasks> UsersTasksRepository, IRepository<TreeProduct> TreeProductRepository)
        {
            _sprTemService = sprTemService;
            _sprPerizdService = sprPerizdService;
            _sprMashService = sprMashService;
            _derIzdService = derIzdService;
            _treeProductService = treeProductService;
            _srez_sostoyanieService = srez_sostoyanieService;
            _UsersTasksRepository = UsersTasksRepository;
            _TreeProductRepository = TreeProductRepository;
        }

        public ActionResult Index()
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
        public ActionResult GetProductTree(SubjectAndProductsViewModel model)
        {
            SubjectAndProductsViewModel i = new SubjectAndProductsViewModel();
            i.Series = model.Series;
            i.ProductId = model.ProductId;
            i.SubjecId = model.SubjecId;

            var series = model.Series;
            var product = _sprPerizdService.GetAllistIzd().Where(x => x.Id == model.ProductId).Select(z => z.Izdelie).FirstOrDefault();

            ViewData["ProductSeries"] = product + " (" + series + ")";

            Session["modelSubjectAndProducts"] = model;

            return View();
        }
        [ValidateInput(false)]
        public ActionResult TreeListProductPartial([DataSourceRequest] DataSourceRequest request)
        {
            var materialCodeViewModels = (from memorandumMaterial in _TreeProductRepository.Table
                                          select new TreeProductViewModel
                                          {
                                              Id = memorandumMaterial.Id,
                                              Vhodim_str = memorandumMaterial.Vhodim_str,
                                              Vhodim_rod = memorandumMaterial.Vhodim_rod,
                                              Vhodimost = memorandumMaterial.Vhodimost,
                                              Obozn = memorandumMaterial.Obozn,
                                              pkpObozn = memorandumMaterial.pkpObozn,
                                              Kizd = memorandumMaterial.Kizd,
                                              Ss = memorandumMaterial.Ss,
                                              Spo = memorandumMaterial.Spo,
                                              Rascex_poln = memorandumMaterial.Rascex_poln,
                                              Mas1sh = memorandumMaterial.Mas1sh,
                                              Masizd = memorandumMaterial.Masizd,
                                              tk1 = memorandumMaterial.tk1,
                                              tk2 = memorandumMaterial.tk2,
                                              tk3 = memorandumMaterial.tk3,
                                              Operation = memorandumMaterial.Operation,
                                          });





            var modelSubjectAndProducts = Session["modelSubjectAndProducts"] as SubjectAndProductsViewModel;

            var ser_s = int.Parse(_sprMashService.GetAllListMash().Where(x => x.NomMash == modelSubjectAndProducts.Series).Select(z => z.Ser_s).FirstOrDefault());
            var ser_spo = int.Parse(_sprMashService.GetAllListMash().Where(x => x.NomMash == modelSubjectAndProducts.Series).Select(z => z.Ser_po).FirstOrDefault());

            List<TreeProductViewModel> model = new List<TreeProductViewModel>();

            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();

            using (var connection = new SqlConnection(settings.DataConnectionString))
            {
                using (var cmd = new SqlCommand("GetTreeProduct", connection))
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
                    SqlParameter IzdIdParameter = new SqlParameter("@paramIzdId", modelSubjectAndProducts.ProductId);
                    SsParameter.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(IzdIdParameter);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter InventIdParameter = new SqlParameter("@paramInventId", modelSubjectAndProducts.srez_sostoyanieId);
                    SsParameter.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(InventIdParameter);
                    connection.Open();
                    cmd.CommandTimeout = 60;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }
                    

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            model.Add(new TreeProductViewModel()
                            {
                                Vhodim_str = sdr.IsDBNull(0) ? null : sdr.GetString(0),
                                Vhodim_rod = sdr.IsDBNull(1) ? null : sdr.GetString(1),
                                Vhodimost = sdr.IsDBNull(2) ? null : sdr.GetString(2),
                                Obozn = sdr.IsDBNull(3) ? null : sdr.GetString(3),
                                pkpObozn = sdr.IsDBNull(4) ? null : sdr.GetString(4),
                                Kizd = sdr.IsDBNull(5) ? 0 : sdr.GetInt32(5),
                                Ss = sdr.IsDBNull(6) ? null : sdr.GetString(6),
                                Spo = sdr.IsDBNull(7) ? null : sdr.GetString(7),
                                Rascex_poln = sdr.IsDBNull(8) ? null : sdr.GetString(8),
                                Mas1sh = sdr.IsDBNull(9) ? 0 : sdr.GetDecimal(9),
                                Masizd = sdr.IsDBNull(10) ? 0 : sdr.GetDecimal(10),
                                tk1 = sdr.IsDBNull(11) ? null : sdr.GetString(11),
                                tk2 = sdr.IsDBNull(12) ? null : sdr.GetString(12),
                                tk3 = sdr.IsDBNull(13) ? null : sdr.GetString(13),
                                Operation = sdr.IsDBNull(14) ? null : sdr.GetString(14),
                                Id = sdr.IsDBNull(15) ? 0 : sdr.GetInt32(15),
                            });
                        }
                    }
                    connection.Close();
                }
            }
            var model2 = model.AsEnumerable();
            var result = model2.ToTreeDataSourceResult(request,
                e => e.Vhodim_str,
                e => e.Vhodim_rod,
                e => e
            );
            //return Json(result, JsonRequestBehavior.AllowGet);
            return PartialView("~/Views/GetNodalizedArray/_TreeListProductPartial.cshtml", model);

        }
        
        [ValidateInput(false)]
        public ActionResult TreeListUserTaskPartial([DataSourceRequest] DataSourceRequest request)
        {

            var model = _UsersTasksRepository.Table.AsEnumerable();
            var result = model.ToTreeDataSourceResult(request,
                e => e.NaimTask,
                e => e.Title                
            );
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public IList<SelectListItem> GetCondition()
        {
            var listParamInvent = _srez_sostoyanieService.GetAllListParameterInvent();
            var conditions = listParamInvent.Select(x => new SelectListItem { Text = x.sostoyanie, Value = x.Id.ToString() }).ToList();
            return conditions;
        }
    }
}