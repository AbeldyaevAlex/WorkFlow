using Asu.Web.Models;
using Asu.Web.Models.ContextDb;
using Asu.Web.Partial_Update_Delete;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{

    public class Spr_CexController : Controller
    {
        private int cex_stal_Id;
        AsuAviaDbContext db = new AsuAviaDbContext();

        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridView21Partial()
        {
            var model = db.Spr_cex.ToList();
            return PartialView("_GridView21Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView21PartialAddNew(Spr_cex item)
        {
            var model = db.Spr_cex;
            if (ModelState.IsValid)
            {
                try
                {
                    var any_Cex = db.Spr_cex.Any(p => string.Compare(p.cex, item.cex) == 0) || db.Spr_cex.Any(p => string.Compare(p.naim_cex, item.naim_cex) == 0);
                    if (any_Cex)
                    {
                        ViewData["EditError"] = $"Запись ЦЕХ - {item.cex} и НАИМЕНОВАНИЕМ - {item.naim_cex} уже существует.";
                    }
                    else
                    {
                        Models.Msi.Spr_cex _cex = new Models.Msi.Spr_cex
                        {
                            operation_date = DateTime.Now,
                            period_open_date = DateTime.Now,
                            cex = item.cex,
                            link_cex_real = item.link_cex_real,
                            nm_cex_krat = item.nm_cex_krat,
                            link_status = item.link_status,
                            naim_cex = item.naim_cex,
                        };
                        model.Add(_cex);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }

            return PartialView("_GridView21Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView21PartialUpdate(Spr_cex item)
        {
            var model = db.Spr_cex;
            //var model = db.Spr_cex.Where(x => x.Status_dok.status != "Аннулирован").ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    var any_Cex = db.Spr_cex.Any(p => string.Compare(p.cex, item.cex) == 0);
                    var any_Naim_Cex = db.Spr_cex.Any(p => string.Compare(p.naim_cex, item.naim_cex) == 0);
                    //if (true)
                    //{
                    //    ViewData["EditError"] = "Данная запись уже существует.";
                    //}
                    
                    
                        Models.Msi.Spr_cex modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                        if (modelItem != null)
                        {
                            //Partial_Update<Models.Msi.Spr_cex> up = new Partial_Update<Models.Msi.Spr_cex>();
                            //up.Partial_Update_Directory(modelItem, "");

                            this.UpdateModel(model);
                            db.SaveChanges();

                            Models.Msi.Spr_cex __cex = new Models.Msi.Spr_cex
                            {
                                operation_date = (DateTime?)DateTime.Now,
                                operation = "insert",
                                period_open_date = DateTime.Now,
                                cex = item.cex,
                                link_cex_real = item.link_cex_real,
                                nm_cex_krat = item.nm_cex_krat,
                                link_status = item.link_status,
                                naim_cex = item.naim_cex
                            };
                            model.Add(__cex);
                            db.SaveChanges();
                            cex_stal_Id = __cex.Id;
                        }
                    
                    string connetionString = null;
                    SqlConnection connection;
                    SqlParameter cex_bilo;
                    SqlParameter cex_stalo;
                    SqlParameter naim_sprav;
                    var bilo = item.Id;
                    var stalo = cex_stal_Id;
                    string sprav = "CEX";
                    connetionString = "data source = i7-860; initial catalog = ASU_AVIA; user id = k6; password = jnltk35";
                    connection = new SqlConnection(connetionString);
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("VALUE_AFTER_UPDATE", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cex_bilo = new SqlParameter("@paramIdOld", bilo);
                        cex_bilo.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(cex_bilo);

                        cex_stalo = new SqlParameter("@paramIdNew", stalo);
                        cex_stalo.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(cex_stalo);

                        naim_sprav = new SqlParameter("@param", sprav);
                        naim_sprav.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(naim_sprav);

                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception)
                    {
                        TempData["msg"] = "<script>alert('Нет подключения к серверу Баз Данных!!!!!');</script>";
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return PartialView("_GridView21Partial", db.Spr_cex.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView21PartialDelete(System.Int32 Id)
        {
            //var model = db.Spr_cex;
            var model = db.Spr_cex.ToList();
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                    {
                        item.link_status = 46;
                        item.operation = "cancelled";
                        item.operation_date = DateTime.Now;
                        item.period_close_date = DateTime.Now;
                        this.UpdateModel(item);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView21Partial", model.ToList());
        }
        public ActionResult ShowDeletetedRow()
        {
            var model = db.Spr_cex.ToList();
            return View("_GridView21Partial", model.ToList());
        }
    }
}