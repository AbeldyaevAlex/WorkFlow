using DevExpress.Web.Mvc;
using Asu.Core;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Vehicles;
using Asu.Services.UsersTasks;
using Asu.Web.Areas.TypicalTechnologicalOperations.Controllers;
using Asu.Web.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2013.Word;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DevExpress.Xpo;

namespace Asu.Web.Areas.Malahit.Controllers
{
    public class MainMalahitController : Controller
    {
        private PGDbContext db = new PGDbContext();
        const string NaimTask = "Малахит (МСЗ)";
        private readonly IWorkContext _workContext;
        private readonly IUserTaskService _userTaskService;

        public MainMalahitController(IWorkContext workContext, IUserTaskService userTaskService)
        {
            _workContext = workContext;
            _userTaskService = userTaskService;
        }
        public ActionResult Index()
        {
            var qwery = _workContext.CurrentCustomer.UsersTask.Where(x => x.IdRoditel == _userTaskService.GetSubTaskId(NaimTask));
            return View(qwery);
        }
        public ActionResult GetMSZ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetMSZ(WorkShopMemorandumBase item)
        {
            if (String.IsNullOrEmpty(item.start_date.ToString()) || String.IsNullOrWhiteSpace(item.start_date.ToString()) || item.start_date == DateTime.Parse("1/1/0001"))
            {
                item.start_date = DateTime.Parse("2015-01-01");
            }
            if (String.IsNullOrEmpty(item.end_date.ToString()) || String.IsNullOrWhiteSpace(item.end_date.ToString()) || item.end_date == DateTime.Parse("1/1/0001"))
            {
                item.end_date = DateTime.Now;
            }
            else
            {
                item.end_date = item.end_date.AddDays(1);
            }
            Session["malahit_zakas"] = item.zakaz;
            Session["malahit_start_date"] = item.start_date;
            Session["malahit_end_date"] = item.end_date;
            return View("~/Areas/Malahit/Views/MainMalahit/KendoGridMSZ.cshtml");
        }

        public ActionResult Accumulativememorandum_Read([DataSourceRequest] DataSourceRequest request)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["PgMalaxitContextProd"].ConnectionString;

            List<AccumulativememorandumVM> accumulativememorandum = new List<AccumulativememorandumVM>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connetionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("public.selectaccumulative", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_accumulative_no", NpgsqlDbType.Text, Session["malahit_accumulativeno"].ToString());
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            accumulativememorandum.Add(new AccumulativememorandumVM()
                            {
                                no_sz = dataReader.IsDBNull(0) ? null : dataReader.GetString(0),
                                accumulativeno = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                                status = dataReader.IsDBNull(2) ? null : dataReader.GetString(2)                               
                            });
                        }
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return Json(accumulativememorandum.ToDataSourceResult(request));
        }
        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["PgMalaxitContextProd"].ConnectionString;

            List<WorkShopMemorandumBase> memorandumBase = new List<WorkShopMemorandumBase>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connetionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("public.selectmsztest4", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_prod_order", NpgsqlDbType.Text, Session["malahit_zakas"].ToString());
                    command.Parameters.AddWithValue("_docum_state", NpgsqlDbType.Text, "Аннулирован");
                    command.Parameters.AddWithValue("_start_date", Session["malahit_start_date"]);
                    command.Parameters.AddWithValue("_end_date", Session["malahit_end_date"]);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            memorandumBase.Add(new WorkShopMemorandumBase()
                            {
                                Id = dataReader.GetInt64(0),
                                tema = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                                zakaz = dataReader.IsDBNull(2) ? null : dataReader.GetString(2),
                                no_sz = dataReader.IsDBNull(3) ? null : dataReader.GetString(3),
                                date = dataReader.IsDBNull(4) ? (DateTime?)null : (DateTime?)dataReader.GetDateTime(4),
                                version = dataReader.GetInt16(5),
                                refcshortname = dataReader.IsDBNull(6) ? null : dataReader.GetString(6),
                                sostavitel = dataReader.IsDBNull(7) ? null : dataReader.GetString(7),
                                osnovanie = dataReader.IsDBNull(8) ? null : dataReader.GetString(8),
                                soderjanie = dataReader.IsDBNull(9) ? null : dataReader.GetString(9),
                                status = dataReader.IsDBNull(10) ? null : dataReader.GetString(10),
                                status_version = dataReader.IsDBNull(11) ? null : dataReader.GetString(11),
                                cex_otprav = dataReader.IsDBNull(12) ? null : dataReader.GetString(12),
                                unxcode = dataReader.IsDBNull(13) ? 0 : dataReader.GetInt64(13),
                                sborka_montaj = dataReader.IsDBNull(14) ? null : dataReader.GetString(14),
                                divisshortname = dataReader.IsDBNull(15) ? null : dataReader.GetString(15),
                                ogmet = dataReader.IsDBNull(16) ? false : dataReader.GetBoolean(16),
                                ogt = dataReader.IsDBNull(17) ? false : dataReader.GetBoolean(17),
                                unitcode = dataReader.IsDBNull(18) ? 0 : dataReader.GetInt64(18),
                                worklineno = dataReader.IsDBNull(19) ? 0 : dataReader.GetInt32(19),
                                level = dataReader.IsDBNull(20) ? null : dataReader.GetString(20),
                                pkp = dataReader.IsDBNull(21) ? null : dataReader.GetString(21),
                                oboznach = dataReader.IsDBNull(22) ? null : dataReader.GetString(22),
                                naim = dataReader.IsDBNull(23) ? null : dataReader.GetString(23),
                                weight = dataReader.IsDBNull(24) ? 0 : dataReader.GetDecimal(24),
                                eizm = dataReader.IsDBNull(25) ? null : dataReader.GetString(25),
                                name = dataReader.IsDBNull(26) ? null : dataReader.GetString(26),
                                samplequantity = dataReader.IsDBNull(27) ? 0 : dataReader.GetInt32(27),
                                quantity = dataReader.IsDBNull(28) ? 0 : dataReader.GetInt32(28),
                                productdesignationparent = dataReader.IsDBNull(29) ? null : dataReader.GetString(29),
                                productnameparent = dataReader.IsDBNull(30) ? null : dataReader.GetString(30),
                                rascehov = dataReader.IsDBNull(31) ? null : dataReader.GetString(31),
                                kodmat = dataReader.IsDBNull(32) ? 0 : dataReader.GetInt64(32),
                                isstandard = dataReader.IsDBNull(33) ? false : dataReader.GetBoolean(33),
                                producttype = dataReader.IsDBNull(34) ? null : dataReader.GetString(34),
                                tex_usl = dataReader.IsDBNull(35) ? null : dataReader.GetString(35),
                                materialquantity = dataReader.IsDBNull(36) ? 0 : dataReader.GetDecimal(36),
                                operationname = dataReader.IsDBNull(37) ? null : dataReader.GetString(37),
                                cex_izgotov = dataReader.IsDBNull(38) ? null : dataReader.GetString(38),
                                cex_usl = dataReader.IsDBNull(39) ? null : dataReader.GetString(39),
                                prodgrname = dataReader.IsDBNull(40) ? null : dataReader.GetString(40),
                                opergrname = dataReader.IsDBNull(41) ? null : dataReader.GetString(41),
                                no = dataReader.IsDBNull(42) ? null : dataReader.GetString(42),
                                materialsize = dataReader.IsDBNull(43) ? null : dataReader.GetString(43),
                                materialmark = dataReader.IsDBNull(44) ? null : dataReader.GetString(44),
                                standard = dataReader.IsDBNull(45) ? null : dataReader.GetString(45),
                                diameter = dataReader.IsDBNull(46) ? null : dataReader.GetString(46),
                                dimension = dataReader.IsDBNull(47) ? null : dataReader.GetString(47),
                                nomenclatureno = dataReader.IsDBNull(48) ? null : dataReader.GetString(48),
                                namemater = dataReader.IsDBNull(49) ? null : dataReader.GetString(49),
                            });
                        }
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return Json(memorandumBase.ToDataSourceResult(request));
        }
        public ActionResult MalahitMszGridViewPartial()
        {
            string connetionString = ConfigurationManager.ConnectionStrings["PgMalaxitContextProd"].ConnectionString;
            string TestConnetionString = ConfigurationManager.ConnectionStrings["PgMalaxitContext"].ConnectionString;

            //string query = "SELECT * FROM public.\"SelectMSZ\" where  membase.documentdate between  '2024-08-01' and '2024-09-13' and prod_order.no = '51885' and docum_state.name != 'Аннулирован'";


            List<WorkShopMemorandumBase> memorandumBase = new List<WorkShopMemorandumBase>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connetionString))
            {
                connection.Open();
                var prod_order = "51885";
                var docum_state = "Аннулирован";
                var start_date = "2024-08-01";
                var end_date = "2024-09-13";
                using (NpgsqlCommand command = new NpgsqlCommand("public.selectmsztest4", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_prod_order", NpgsqlDbType.Text, prod_order);
                    command.Parameters.AddWithValue("_docum_state", NpgsqlDbType.Text, docum_state);
                    command.Parameters.AddWithValue("_start_date", start_date);
                    command.Parameters.AddWithValue("_end_date", end_date);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            //memorandumBase.Add(new WorkShopMemorandumBase()
                            //{                              
                            //    cex_izgotov = dataReader.IsDBNull(0) ? null : dataReader.GetString(0),
                            //    Id = dataReader.GetInt32(1),
                            //});
                            memorandumBase.Add(new WorkShopMemorandumBase()
                            {
                                Id = dataReader.GetInt64(0),
                                tema = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                                zakaz = dataReader.IsDBNull(2) ? null : dataReader.GetString(2),
                                no_sz = dataReader.IsDBNull(3) ? null : dataReader.GetString(3),
                                date = dataReader.IsDBNull(4) ? (DateTime?)null : (DateTime?)dataReader.GetDateTime(4),
                                version = dataReader.GetInt16(5),
                                refcshortname = dataReader.IsDBNull(6) ? null : dataReader.GetString(6),
                                sostavitel = dataReader.IsDBNull(7) ? null : dataReader.GetString(7),
                                osnovanie = dataReader.IsDBNull(8) ? null : dataReader.GetString(8),
                                soderjanie = dataReader.IsDBNull(9) ? null : dataReader.GetString(9),
                                status = dataReader.IsDBNull(10) ? null : dataReader.GetString(10),
                                status_version = dataReader.IsDBNull(11) ? null : dataReader.GetString(11),
                                cex_otprav = dataReader.IsDBNull(12) ? null : dataReader.GetString(12),
                                unxcode = dataReader.IsDBNull(13) ? 0 : dataReader.GetInt64(13),
                                sborka_montaj = dataReader.IsDBNull(14) ? null : dataReader.GetString(14),
                                divisshortname = dataReader.IsDBNull(15) ? null : dataReader.GetString(15),
                                ogmet = dataReader.IsDBNull(16) ? false : dataReader.GetBoolean(16),
                                ogt = dataReader.IsDBNull(17) ? false : dataReader.GetBoolean(17),
                                unitcode = dataReader.IsDBNull(18) ? 0 : dataReader.GetInt64(18),
                                worklineno = dataReader.IsDBNull(19) ? 0 : dataReader.GetInt32(19),
                                level = dataReader.IsDBNull(20) ? null : dataReader.GetString(20),
                                pkp = dataReader.IsDBNull(21) ? null : dataReader.GetString(21),
                                oboznach = dataReader.IsDBNull(22) ? null : dataReader.GetString(22),
                                naim = dataReader.IsDBNull(23) ? null : dataReader.GetString(23),
                                weight = dataReader.IsDBNull(24) ? 0 : dataReader.GetDecimal(24),
                                eizm = dataReader.IsDBNull(25) ? null : dataReader.GetString(25),
                                name = dataReader.IsDBNull(26) ? null : dataReader.GetString(26),
                                samplequantity = dataReader.IsDBNull(27) ? 0 : dataReader.GetInt32(27),
                                quantity = dataReader.IsDBNull(28) ? 0 : dataReader.GetInt32(28),
                                productdesignationparent = dataReader.IsDBNull(29) ? null : dataReader.GetString(29),
                                productnameparent = dataReader.IsDBNull(30) ? null : dataReader.GetString(30),
                                rascehov = dataReader.IsDBNull(31) ? null : dataReader.GetString(31),
                                kodmat = dataReader.IsDBNull(32) ? 0 : dataReader.GetInt64(32),
                                isstandard = dataReader.IsDBNull(33) ? false : dataReader.GetBoolean(33),
                                producttype = dataReader.IsDBNull(34) ? null : dataReader.GetString(34),
                                tex_usl = dataReader.IsDBNull(35) ? null : dataReader.GetString(35),
                                materialquantity = dataReader.IsDBNull(36) ? 0 : dataReader.GetDecimal(36),
                                operationname = dataReader.IsDBNull(37) ? null : dataReader.GetString(37),
                                cex_izgotov = dataReader.IsDBNull(38) ? null : dataReader.GetString(38),
                                cex_usl = dataReader.IsDBNull(39) ? null : dataReader.GetString(39),
                                prodgrname = dataReader.IsDBNull(40) ? null : dataReader.GetString(40),
                                opergrname = dataReader.IsDBNull(41) ? null : dataReader.GetString(41),
                                no = dataReader.IsDBNull(42) ? null : dataReader.GetString(42),
                                materialsize = dataReader.IsDBNull(43) ? null : dataReader.GetString(43),
                                materialmark = dataReader.IsDBNull(44) ? null : dataReader.GetString(44),
                                standard = dataReader.IsDBNull(45) ? null : dataReader.GetString(45),
                                diameter = dataReader.IsDBNull(46) ? null : dataReader.GetString(46),
                                dimension = dataReader.IsDBNull(47) ? null : dataReader.GetString(47),
                                nomenclatureno = dataReader.IsDBNull(48) ? null : dataReader.GetString(48),
                                namemater = dataReader.IsDBNull(49) ? null : dataReader.GetString(49),
                            });
                        }
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return PartialView("~/Areas/Malahit/Views/MainMalahit/_MalahitMszGridView.cshtml", memorandumBase);
        }
        public ActionResult GetAccumulativeNoMSZ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAccumulativeNoMSZ(AccumulativememorandumVM item)
        {
            Session["malahit_accumulativeno"] = item.accumulativeno;
            return View("~/Areas/Malahit/Views/MainMalahit/KendoGridAccumulativeNoMSZ.cshtml");
        }


        public ActionResult NomenklMsz()
        {
            return View();
        }
        public ActionResult Nomenkl_Read([DataSourceRequest] DataSourceRequest request)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["PgMalaxitContextProd"].ConnectionString;

            List<WorkShopMemorandumBase> memorandumBase = new List<WorkShopMemorandumBase>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connetionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("public.selectuchetmsz", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_prod_order", NpgsqlDbType.Text, Session["malahit_zakas"].ToString());
                    command.Parameters.AddWithValue("_docum_state", NpgsqlDbType.Text, "Аннулирован");
                    command.Parameters.AddWithValue("_start_date", Session["malahit_start_date"]);
                    command.Parameters.AddWithValue("_end_date", Session["malahit_end_date"]);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            memorandumBase.Add(new WorkShopMemorandumBase()
                            {
                                Id = dataReader.GetInt64(0),
                                tema = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                                zakaz = dataReader.IsDBNull(2) ? null : dataReader.GetString(2),
                                pkp = dataReader.IsDBNull(3) ? null : dataReader.GetString(3),
                                oboznach = dataReader.IsDBNull(4) ? null : dataReader.GetString(4),
                                naim = dataReader.IsDBNull(5) ? null : dataReader.GetString(5),
                                samplequantity = dataReader.IsDBNull(6) ? 0 : dataReader.GetInt32(6),
                                quantity = dataReader.IsDBNull(7) ? 0 : dataReader.GetInt32(7),
                                eizm = dataReader.IsDBNull(8) ? null : dataReader.GetString(8),
                                productdesignationparent = dataReader.IsDBNull(9) ? null : dataReader.GetString(9),
                                productnameparent = dataReader.IsDBNull(10) ? null : dataReader.GetString(10),
                                rascehov = dataReader.IsDBNull(11) ? null : dataReader.GetString(11),
                                no_sz = dataReader.IsDBNull(12) ? null : dataReader.GetString(12),
                                date = dataReader.IsDBNull(13) ? (DateTime?)null : (DateTime?)dataReader.GetDateTime(13),
                                version = dataReader.GetInt16(14),
                            });
                        }
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return Json(memorandumBase.ToDataSourceResult(request));
        }
    }
}