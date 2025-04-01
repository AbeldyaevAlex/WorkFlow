using Asu.Web.Models;
using Asu.Web.Partial_Update_Delete;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.TypicalTechnologicalOperations.Controllers
{
    public class Spr_ttoController : Controller
    {
        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
        //private long cex_stal_Id;
        
        public ActionResult Index()
        {            
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridView13Partial(string tto)
        {
            var tto_ = db.Spr_tto;

            var tto_id = int.Parse(tto);
            var komp_kod = db.Spr_tto.Where(i => i.Id == tto_id).Select(k => k.link_kod_komp).ToList()[0];
            var tto_kod = db.Spr_tto.Where(i => i.Id == komp_kod).Select(k => k.link_kod_TTO).ToList()[0];

            //List<TTO_View_Model> list = new List<TTO_View_Model>();
            //foreach (var item in tto_)
            //{
            //    TTO_View_Model v_m_tto = new TTO_View_Model();
            //    v_m_tto.Id = item.Id;
            //    v_m_tto.krat = item.krat;
            //    v_m_tto.link_cizg = item.link_cizg;
            //    v_m_tto.link_kod_TTO = item.link_kod_TTO;
            //    v_m_tto.link_kod_komp = item.link_kod_komp;
            //    v_m_tto.link_prkm = item.link_prkm;
            //    v_m_tto.link_prpokr = item.link_prpokr;
            //    v_m_tto.link_status = item.link_status;
            //    v_m_tto.link_user = item.link_user;
            //    v_m_tto.nrm = item.nrm;
            //    v_m_tto.nrvp = item.nrvp;
            //    v_m_tto.operation = item.operation;
            //    v_m_tto.operation_date = item.operation_date;
            //    v_m_tto.period_close_date = item.period_close_date;
            //    v_m_tto.period_open_date = item.period_open_date;
            //    v_m_tto.sort_kod_komp = item.sort_kod_komp;
            //    v_m_tto.vpost = item.vpost;
            //    v_m_tto.vpost_sh = item.vpost_sh;
            //    //v_m_tto.km_tto = item.Spr_skm.km;
            //    //v_m_tto.km_komp = item.Spr_skm1.km;
            //    list.Add(v_m_tto);
            //}
            

            if (tto_kod != null)
            {
                //var tto_id = db.Spr_tto.Where(i => i.link_kod_TTO.ToString() == tto).Select(u => u.Id).ToList()[0];
                ////var komp_id = db.Spr_tto.Where(i => i.link_kod_TTO.ToString() == tto).Select(u => u.link_kod_komp).ToList()[0];
                //var tto_id = db.Spr_tto.Where(i => i.link_kod_TTO.ToString() == tto);


                ViewData["link_tto"] = tto_kod;

                ViewData["link_kkomp"] = komp_kod;

                //komp_id = 0;
                return PartialView("_GridView1Partial", db.Spr_tto.Where(i => i.link_kod_TTO.ToString() == tto_kod.ToString()));
            }
            else
            {               
                return PartialView("_GridView13Partial", tto_);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult GridView13PartialAddNew(Asu.Web.Models.Spr_tto item)
        //{
        //    var model = db.Spr_tto;
        //    if (ModelState.IsValid)
        //    {
        //        var any_TTO = db.Spr_tto.Any(p => string.Compare(p.link_km.ToString(), item.link_km.ToString()) == 0) && db.Spr_tto.Any(p => string.Compare(p.link_kkomp.ToString(), item.link_kkomp.ToString()) == 0);
        //        bool km_kkomp = item.link_km == item.link_kkomp;
        //        try
        //        {
        //            if (any_TTO == true)
        //            {
        //                ViewData["EditError"] = $"Запись Код материала и Код компонента  уже существует.";
        //            }
        //            else if (km_kkomp == true)
        //            {
        //                ViewData["EditError"] = $"Нельзя ввести : Код материала  == Код компонента.";
        //            }
        //            else
        //            {
        //                Spr_tto _tto = new Spr_tto
        //                {
        //                    operation_date = DateTime.Now,
        //                    operation = "insert",
        //                    period_open_date = DateTime.Now,                                                       
        //                    link_user = (Int32)Session["UserId"],
        //                    link_status = item.link_status,
        //                    //link_kkomp = item.link_kkomp,
        //                    //link_km = item.link_km,
        //                    vpost = item.vpost,
        //                    vpost_sh = item.vpost_sh,
        //                    //sort_kkomp = item.sort_kkomp,
        //                    //sort_km = item.sort_km,
        //                    krat = item.krat,
        //                    link_cizg  =item.link_cizg,
        //                    link_prkm = item.link_prkm,
        //                    link_prpokr = item.link_prpokr,
        //                    nrm = item.nrm,
        //                    nrvp = item.nrvp,                           
        //                };
        //                model.Add(_tto);
        //                db.SaveChanges();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //    {
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    }               
        //    return PartialView("_GridView13Partial", model.ToList());
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult GridView13PartialUpdate(Spr_tto item)
        //{
        //    var model = db.Spr_tto;
        //    //var model = db.Spr_cex.Where(x => x.Status_dok.status != "Аннулирован").ToList();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var any_TTO = db.Spr_tto.Any(p => string.Compare(p.link_km.ToString(), item.link_km.ToString()) == 0) && db.Spr_tto.Any(p => string.Compare(p.link_kkomp.ToString(), item.link_kkomp.ToString()) == 0);
        //            bool km_kkomp = item.link_km == item.link_kkomp;

        //            if (any_TTO)
        //            {
        //                ViewData["EditError"] = "Данная запись уже существует.";
        //            }
        //            else if (km_kkomp)
        //            {
        //                ViewData["EditError"] = $"Нельзя ввести : Код материала  == Код компонента.";
        //            }
        //            else
        //            {
        //                Spr_tto modelItem = model.FirstOrDefault(it => it.Id == item.Id);
        //                if (modelItem != null)
        //                {
        //                    Partial_Update up = new Partial_Update();
        //                    up.Partial_Update_Spr_tto(modelItem, (Int32)Session["UserId"]);

        //                    this.UpdateModel(model);
        //                    db.SaveChanges();

        //                    Spr_tto __tto = new Spr_tto
        //                    {
        //                        operation_date = (DateTime?)DateTime.Now,
        //                        operation = "insert",
        //                        period_open_date = DateTime.Now,
        //                        link_user = (Int32)Session["UserId"],
        //                        link_status = item.link_status,
        //                        link_kkomp = item.link_kkomp,
        //                        link_km = item.link_km,
        //                        vpost = item.vpost,
        //                        vpost_sh = item.vpost_sh,
        //                        sort_kkomp = item.sort_kkomp,
        //                        sort_km = item.sort_km,
        //                        krat = item.krat,
        //                        link_cizg = item.link_cizg,
        //                        link_prkm = item.link_prkm,
        //                        link_prpokr = item.link_prpokr,
        //                        nrm = item.nrm,
        //                        nrvp = item.nrvp
        //                    };
        //                    model.Add(__tto);
        //                    db.SaveChanges();
        //                    cex_stal_Id = __tto.Id;
        //                }
        //            }
        //            string connetionString = null;
        //            SqlConnection connection;
        //            SqlParameter cex_bilo;
        //            SqlParameter cex_stalo;
        //            SqlParameter naim_sprav;
        //            var bilo = item.Id;
        //            var stalo = cex_stal_Id;
        //            string sprav = "TTO";
        //            connetionString = "data source = i7-860; initial catalog = ASU_AVIA; user id = k6; password = jnltk35";
        //            connection = new SqlConnection(connetionString);
        //            try
        //            {
        //                connection.Open();
        //                SqlCommand cmd = new SqlCommand("VALUE_AFTER_UPDATE", connection);
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cex_bilo = new SqlParameter("@cex_bilo", bilo);
        //                cex_bilo.Direction = ParameterDirection.Input;
        //                cmd.Parameters.Add(cex_bilo);

        //                cex_stalo = new SqlParameter("@cex_stalo", stalo);
        //                cex_stalo.Direction = ParameterDirection.Input;
        //                cmd.Parameters.Add(cex_stalo);

        //                naim_sprav = new SqlParameter("@param", sprav);
        //                cex_stalo.Direction = ParameterDirection.Input;
        //                cmd.Parameters.Add(naim_sprav);

        //                cmd.ExecuteNonQuery();
        //                connection.Close();
        //            }
        //            catch (Exception)
        //            {
        //                TempData["msg"] = "<script>alert('Нет подключения к серверу Баз Данных!!!!!');</script>";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //    {
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    }
        //    return PartialView("_GridView13Partial", db.Spr_tto.Where(x => x.Status_dok.status != "Аннулирован").ToList());
        //}

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView13PartialDelete(System.Int64 Id)
        {
            var model = db.Spr_tto;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                    {
                        item.link_status = db.Status_dok.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
                        item.operation = "cancelled";
                        item.link_user = (Int32)Session["UserId"];
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
            return PartialView("_GridView13Partial", model.ToList());
        }

        Asu.Web.Models.ASU_AVIAEntities12 db1 = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult GridView1Partial(string tto)
        {
            //ViewBag.link_tto = new SelectList(db.Spr_tto, "Id", "Spr_skm.km");
            //ViewBag.link_kkomp = new SelectList(db.Spr_tto, "Id", "Spr_skm1.km");
            if (tto != null)
            {
                ViewData["yes_tto"] = "Входящие ТТО";
                //var tto_ = db.Spr_tto;
                //List<TTO_View_Model> list = new List<TTO_View_Model>();

                //foreach (var item in tto_)
                //{
                //    TTO_View_Model v_m_tto = new TTO_View_Model();
                //    v_m_tto.Id = item.Id;
                //    v_m_tto.krat = item.krat;
                //    v_m_tto.link_cizg = item.link_cizg;
                //    v_m_tto.link_kod_TTO = item.link_kod_TTO;
                //    v_m_tto.link_kod_komp = item.link_kod_komp;
                //    v_m_tto.link_prkm = item.link_prkm;
                //    v_m_tto.link_prpokr = item.link_prpokr;
                //    v_m_tto.link_status = item.link_status;
                //    v_m_tto.link_user = item.link_user;
                //    v_m_tto.nrm = item.nrm;
                //    v_m_tto.nrvp = item.nrvp;
                //    v_m_tto.operation = item.operation;
                //    v_m_tto.operation_date = item.operation_date;
                //    v_m_tto.period_close_date = item.period_close_date;
                //    v_m_tto.period_open_date = item.period_open_date;
                //    v_m_tto.sort_kod_komp = item.sort_kod_komp;
                //    v_m_tto.vpost = item.vpost;
                //    v_m_tto.vpost_sh = item.vpost_sh;
                //    list.Add(v_m_tto);
                //}  
                Random random = new Random();
                var tick = random.Next(10000);
                var tto_id = int.Parse(tto);
                ViewData["link_tto"] = tto_id;

                var tto_kod = db.Spr_tto.Where(i => i.Id == tto_id).Select(k => k.link_kod_komp).ToList()[0];
                var ss = db.Spr_tto.Where(i => i.link_kod_TTO == tto_kod).ToList();
                if (ss.Count != 0)
                {
                    ViewData["yes_tto"] = "TTO";
                }
                //return PartialView("Dop_tto", db.Spr_tto);
                return PartialView("Dop_tto", db.Spr_tto.Where(i => i.link_kod_TTO == tto_kod).ToList());
            }
            else
            {
                return PartialView("_GridView1Partial", db.Spr_tto);
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(Asu.Web.Models.Spr_tto item)
        {
            var model = db1.Spr_tto;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(Asu.Web.Models.Spr_tto item)
        {
            var model = db1.Spr_tto;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db1.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int64 Id)
        {
            var model = db1.Spr_tto;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView1Partial", model.ToList());
        }
    }
}