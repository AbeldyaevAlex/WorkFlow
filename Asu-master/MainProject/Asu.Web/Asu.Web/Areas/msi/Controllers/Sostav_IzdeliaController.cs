using Asu.Web.Models.ContextDb;
using Asu.Web.Partial_Update_Delete;
using Asu.Web.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Web.Services.Description;
using System.Configuration;
using Asu.Web.Areas.TypicalTechnologicalOperations.Controllers;
using Asu.Core.Data;
using Asu.Core.Domain.Msi;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Sostav_IzdeliaController : Controller, IController
    {

        private readonly IRepository<Spr_tem> _sprThemRepository;
        private readonly IRepository<Spr_Perizd> _sprPerIzdRepository;

        public const string EditResultKey = "EditResult";
        public const string EditErrorKey = "EditError";
        public const string yes_pkp = "'Ж','В','SL','ТРН','ЖК','ВК','СХК','SLK','ТНК','КЗЧ','ЗЧК'";
        public const string no_pkp = "'СНГ','СГ','СКН','СГК','ТС','ТСК','КТС','ДСН'";

        private AsuAviaDbContext db;
        public Sostav_IzdeliaController(IRepository<Spr_tem> sprThemRepository, IRepository<Spr_Perizd> sprPerIzdRepository)
        {
            _sprThemRepository = sprThemRepository;
            _sprPerIzdRepository = sprPerIzdRepository;
            db = new AsuAviaDbContext();
        }
        public ActionResult GetThemesAndProduct(string productComposition)
        {
            Session["productComposition"] = productComposition;
            return View();
        }
        public ActionResult DropDownListGetThemesAndProduct()
        {
            List<Spr_tem> listTheme = new List<Spr_tem>();

            listTheme = (from theme in _sprThemRepository.Table
                         select theme).ToList();
            return Json(listTheme, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetThemesAndProduct(FormCollection formCollection)
        {
            string selectedProduct = formCollection["Id"];
            return RedirectToAction("Get_Sostav", "Sostav_Izdelia", new { Model = selectedProduct });
        }
        public JsonResult GetProducts(string id)
        {
            var result = int.TryParse(id, out int ThemeId);
            if (!result)
            {
                throw new ArgumentException("not parse productid");
            }
            else
            {
                List<Spr_Perizd> listProduct = new List<Spr_Perizd>();

                listProduct = (from product in _sprPerIzdRepository.Table
                               where product.TemaId == ThemeId
                               select product).ToList();
                return Json(listProduct, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, Authorize]
        public ActionResult Get_Sostav(string Model)
        {
            int[] listOfProductId = Model.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Session["listOfProductId"] = Model;
            return View(listOfProductId);
        }
        public ActionResult GridView1Partial()
        {
            var composition = Session["productComposition"].ToString();

            var listOfProductId = Session["listOfProductId"].ToString();

            IQueryable<OboznViewModel> list_obozn = null;

            if (composition.Contains("Специф"))
            {
                list_obozn = Get_ob(listOfProductId, composition);
            }
            if (composition.Contains("Номенкл"))
            {
                list_obozn = Get_ob(listOfProductId, composition);
            }

            return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_GridView1Partial.cshtml", list_obozn);
        }
        public IQueryable<OboznViewModel> Get_ob(string arr_izd, string composition)
        {
            IQueryable<OboznViewModel> statesList = null;
            SqlCommand cmd = new SqlCommand();

            if (composition.Contains("Специф"))
            {
                string query = "GetProduct";
                cmd.Parameters.AddWithValue("@ProductsIds", arr_izd);
                cmd.Parameters.AddWithValue("@Y_PKP", yes_pkp);
                cmd.Parameters.AddWithValue("@NO_PKP", no_pkp);
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                statesList = GetItemsProducts(cmd);
            }
            if (composition.Contains("Номенкл"))
            {
                string query = "GetItems";
                cmd.Parameters.AddWithValue("@ProductsIds", arr_izd);
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                statesList = GetItemsProducts(cmd);
            }

            return statesList;
        }
        private IQueryable<OboznViewModel> GetItemsProducts(SqlCommand cmd)
        {
            List<OboznViewModel> listItems = new List<OboznViewModel>();
            string connetionString = ConfigurationManager.ConnectionStrings["AsuAviaContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        listItems.Add(new OboznViewModel()
                        {
                            Id = sdr.GetInt64(0),
                            obozn = sdr.GetString(1),
                            link_naim = sdr.GetInt64(2),
                            link_pkp = sdr.GetInt64(3),
                            link_status = sdr.GetInt32(4),
                            link_user = sdr.GetString(5),
                            obozn_p = sdr.GetString(6),
                            obozn_dos = sdr.GetString(7),
                            period_open_date = sdr.IsDBNull(9) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(9),
                            period_close_date = sdr.IsDBNull(8) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(8),

                            operation_date = sdr.GetDateTime(12),
                            stsort_kt = sdr.GetString(13),
                            stsort_tip = sdr.GetString(14),
                            stsort_tr_1 = sdr.GetString(15),
                            stsort_tr_2 = sdr.GetString(16),
                            stsort_tr_3 = sdr.GetString(17),
                            stsort_tr_4 = sdr.GetString(18),
                            stsort_tr_5 = sdr.GetString(19),
                            stsort_tr_6 = sdr.GetString(20),
                            stsort_tr_7 = sdr.GetString(21),
                            var = sdr.GetString(22),
                            link_pvi = sdr.GetInt32(23)
                        });
                    }
                }
                conn.Close();
            }
            return listItems.AsQueryable();
        }
        private List<SelectListItem> GetItems(SqlCommand cmd)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            string connetionString = ConfigurationManager.ConnectionStrings["AsuAviaContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        listItems.Add(new SelectListItem { Text = sdr[1].ToString(), Value = sdr[0].ToString() });
                    }
                }
                conn.Close();
            }
            return listItems;
        }
        ////[HttpGet, Authorize]
        ////public ActionResult Index()
        ////{
        ////    Tema tema = new Tema();
        ////    return View(tema);
        ////}
        ////[HttpPost]
        ////public ActionResult Index(Tema tem)
        ////{
        ////    var tema = tem.Spr_tem.Where(x => x.Id == int.Parse(tem.SelectedTem)).Select(i => i.Id).ToArray()[0];
        ////    var name_tema = tem.Spr_tem.Where(x => x.Id == int.Parse(tem.SelectedTem)).Select(i => i.nm_tem_p).ToArray()[0];
        ////    Session["Name_tema"] = name_tema;
        ////    Index2(tema);
        ////    return View("Index2");
        ////}

        ////[HttpGet, Authorize]
        ////public ActionResult Index2(long? temIzdIds)
        ////{
        ////    Session["Tema"] = temIzdIds;
        ////    var perIzd = db.Spr_Perizd.Where(x => x.link_tema == temIzdIds).ToList();
        ////    list_Izd lstIzd = new list_Izd();
        ////    lstIzd.listIzd = perIzd;
        ////    return View(lstIzd);
        ////}
        ////[HttpPost]
        ////public ActionResult Index2(list_Izd izdelie)
        ////{
        ////    List<Models.Msi.Spr_Perizd> list_product = new List<Models.Msi.Spr_Perizd>();
        ////    list_product = izdelie.listIzd.Where(x => x.IsActive == true).ToList();
        ////    long tema = (long)Session["Tema"];
        ////    List<string> get_all_perizd = izdelie.listIzd.Where(x => x.IsActive == true).Select(u => u.nm_izd).ToList();
        ////    var result_all_perizd = (String.Join(",", get_all_perizd));
        ////    Session["result_all_perizd"] = result_all_perizd;
        ////    if (result_all_perizd.Contains("Картотека детально-сборочных единиц по теме ") && result_all_perizd.Contains("Картотека стандартных изделий по теме "))
        ////    {
        ////        list_product = (from per_izd in db.Spr_Perizd
        ////                        where per_izd.link_tema == tema
        ////                        select per_izd).ToList();
        ////    }
        ////    var result = string.Join(",", list_product.Select(x => x.Id));
        ////    int[] listOfId = result.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        ////    Session["listOfId"] = listOfId;
        ////    this.ViewBag.Array_Izd = get_all_perizd;
        ////    return View("Get_Sostav");
        ////}
        ////[ValidateInput(false)]
        ////public ActionResult GridView1Partial()
        ////{
        ////    int[] array_izd = (int[])Session["listOfId"];
        ////    string result_all_perizd = (string)Session["result_all_perizd"];
        ////    var list_obozn = Get_ob(array_izd, result_all_perizd);
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_GridView1Partial.cshtml", list_obozn);
        ////}
        public static List<Models.Msi.Spr_obozn> GetRowValuesByKeyValue(int key)
        {
            AsuAviaDbContext db = new AsuAviaDbContext();
            var array_obozn = db.Spr_obozn.Where(x => x.Id == key).ToList();
            return array_obozn;
        }
        //public ActionResult CustomGridViewEditingPartial(int key)
        //{
        //    var model = db.Spr_obozn;
        //    ViewData["key"] = key;
        //    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_GridView1Partial.cshtml", model);
        //}
        ////public IQueryable<Models.Msi.Spr_obozn> Get_ob(int[] arr_izd, string all_list)
        ////{
        ////    IQueryable<Models.Msi.Spr_obozn> result = null;
        ////    if (all_list.Contains("Картотека детально-сборочных единиц по теме") && !all_list.Contains("Картотека стандартных изделий по теме "))
        ////    {
        ////        result = (from obozn in db.Spr_obozn
        ////                  join pkp in db.Spr_PKP
        ////                  on obozn.link_pkp equals pkp.Id
        ////                  //join prim_dse in db.Spr_prim_dse
        ////                  //on obozn.Id equals prim_dse.link_obozn                              
        ////                  join r_dse in db.Spr_Razd_DSE
        ////                  on pkp.link_razd_dse equals r_dse.Id
        ////                  where r_dse.Id == 2
        ////                  select obozn).Distinct();
        ////        return result;
        ////    }
        ////    //if (all_list.Contains("Картотека детально-сборочных единиц по теме") && all_list.Contains("Картотека стандартных изделий по теме "))
        ////    //{
        ////    //    result = (from obozn in db.Spr_obozn
        ////    //              join pkp in db.Spr_PKP
        ////    //              on obozn.link_pkp equals pkp.Id
        ////    //              join prim_dse in db.Spr_prim_dse
        ////    //              on obozn.Id equals prim_dse.link_obozn
        ////    //              select obozn).Distinct();
        ////    //    return result;
        ////    //}
        ////    //if (!all_list.Contains("Картотека детально-сборочных единиц по теме") && !all_list.Contains("Картотека стандартных изделий по теме "))
        ////    //{
        ////    //    result = (from obozn in db.Spr_obozn
        ////    //              join prim_dse in db.Spr_prim_dse
        ////    //              on obozn.Id equals prim_dse.link_obozn
        ////    //              where arr_izd.Any(s => s == prim_dse.link_izd)
        ////    //              select obozn).Distinct();
        ////    //    return result;
        ////    //}
        ////    if (!all_list.Contains("Картотека детально-сборочных единиц по теме") && all_list.Contains("Картотека стандартных изделий по теме "))
        ////    {
        ////        result = (from obozn in db.Spr_obozn
        ////                      //join prim_dse in db.Spr_prim_dse
        ////                      //on obozn.Id equals prim_dse.link_obozn
        ////                  join pkp in db.Spr_PKP
        ////                  on obozn.link_pkp equals pkp.Id
        ////                  join r_dse in db.Spr_Razd_DSE
        ////                  on pkp.link_razd_dse equals r_dse.Id
        ////                  where r_dse.Id == 3
        ////                  select obozn).Distinct();
        ////        return result;
        ////    }
        ////    return result;
        ////}
        //public ActionResult PageControlPartial(int key)
        //{
        //    ViewData["key"] = key;
        //    var model = db.Spr_obozn.Where(item => item.Id == key).FirstOrDefault();
        //    return PartialView("DetailPageControl", model);
        //}

        ////public ActionResult SpecificationPrimOboznPartial(int key)
        ////{
        ////    ViewData["key"] = key;
        ////    Session["keySpecifId"] = key;
        ////    var model = db.Spr_specif.Where(item => item.Id == key).FirstOrDefault();///////////Поменяй здесь
        ////    return PartialView("SpecificationPrimOboznPageControl", model);
        ////}
        //[ValidateInput(false)]
        //public ActionResult SpecificationPrimDseGridView(int key)
        //{
        //    var prim_specif = db.Spr_obozn.Where(x => x.Id == key).Select(j => j.obozn_p).FirstOrDefault();
        //    ViewData["prim_obozn"] = prim_specif;
        //    Session["primspecificationKey"] = key;
        //    return View();
        //}
        ////[ValidateInput(false)]
        ////public ActionResult SpecifPrimGridViewPartial()
        ////{
        ////    var keySpecifId = (int)Session["keySpecifId"];
        ////    var key = (int)Session["primspecificationKey"];
        ////    if ((db.Spr_specif.Where(x => x.Id == keySpecifId).Select(j => j.link_pkp_T_TV).ToList()[0]) == 2 || (db.Spr_specif.Where(x => x.Id == keySpecifId).Select(j => j.link_pkp_T_TV).ToList()[0]) == 3)
        ////    {
        ////        var a = db.Spr_specif.Where(x => x.Id == keySpecifId).Select(j => j.link_kts).FirstOrDefault();
        ////        var model = db.Spr_prim_dse.Where(e => e.link_kts == a && e.link_obozn == 1).ToList();
        ////        return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifPrimGridViewPartial.cshtml", model.ToList());
        ////    }
        ////    else
        ////    {
        ////        var link_kts = db.Spr_specif.Where(x => x.Id == keySpecifId).Select(j => j.link_kts).FirstOrDefault();
        ////        var link_obozn = db.Spr_specif.Where(x => x.Id == keySpecifId).Select(j => j.link_obozn).FirstOrDefault();
        ////        var model = db.Spr_prim_dse.Where(e => e.link_kts == link_kts && e.link_obozn == link_obozn).ToList();
        ////        return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifPrimGridViewPartial.cshtml", model.ToList());
        ////    }
        ////    //var model = db.Spr_prim_dse.Where(item => item.link_obozn == (int)key);           
        ////}

        //[ValidateInput(false)]
        //public ActionResult SpecificationGridView(int key)
        //{
        //    var kts = db.Spr_obozn.Where(x => x.Id == key).Select(j => j.obozn).FirstOrDefault();
        //    ViewData["KTS_NAIM"] = kts;
        //    Session["specificationKey"] = key;
        //    return View();
        //}
        //[ValidateInput(false)]
        //public ActionResult SpecifGridViewPartial()
        //{
        //    var key = Session["specificationKey"];

        //    var link_specification = GetLinkSpecification((int)key);

        //    var model = db.Spr_specif.Where(item => item.link_spec == link_specification);

        //    var model2 = db.Spr_specif.Where(item => item.link_spec == link_specification).ToList();

        //    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifGridViewPartial.cshtml", model);
        //}
        //public static long GetLinkSpecification(int key)
        //{
        //    var db = new AsuAviaDbContext();
        //    var ob_ = db.Spr_obozn.Where(x => x.Id == key).Select(i => i.obozn).FirstOrDefault();


        //    var Id_Obozn__ = db.Spr_obozn.Where(x => x.obozn == ob_).ToList();


        //    long Id_Obozn = db.Spr_obozn.Where(x => x.obozn == ob_ && (String.IsNullOrEmpty(x.var) || x.var == "")).Select(i => i.Id).FirstOrDefault();
        //    return Id_Obozn;
        //}
        ////public static IQueryable<Models.Msi.Spr_specif> GetObozn_test(object key)
        ////{
        ////    var db = new AsuAviaDbContext();
        ////    IQueryable<Models.Msi.Spr_specif> source = from specif in db.Spr_specif
        ////                                    where specif.link_kts.Equals(key)
        ////                                    select specif;
        ////    return source;
        ////}

        //[ValidateInput(false)]
        //public ActionResult PrimDseGridView(int key)
        //{
        //    Session["PrimDseKey"] = key;
        //    var PrimDseKey = db.Spr_obozn.Where(x => x.Id == key).Select(o => o.obozn_p).ToList()[0];
        //    ViewData["PrimDseKey"] = PrimDseKey;
        //    return View();
        //}
        //[ValidateInput(false)]
        //public ActionResult PrimDseGridViewPartial()
        //{
        //    var key = Session["PrimDseKey"];
        //    var model = db.Spr_prim_dse.Where(item => item.link_obozn == (int)key);
        //    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_PrimDseGridViewPartial.cshtml", model);
        //}
        ////[ValidateInput(false)]
        ////public ActionResult UserInformationGridView(int key)
        ////{
        ////    int? user_Id = db.Spr_obozn.Where(item => item.Id == key).Select(j => j.link_user).ToArray()[0];
        ////    return View();
        ////}
        ////[ValidateInput(false)]
        ////public ActionResult UserInformationGridViewPartial()
        ////{
        ////    var model = db.User;
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_UserInformationGridViewPartial.cshtml", model.ToList());
        ////}

        //[HttpPost, ValidateInput(false)]
        //public ActionResult GridView1PartialAddNew(Models.Msi.Spr_obozn item)
        //{
        //    var per_izd = GetTextByValues();
        //    int[] listOfId = per_izd.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        //    item.per_izd = null;
        //    var model_obozn = db.Spr_obozn;
        //    var model_prin_dse = db.Spr_prim_dse;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var any_Obozn = db.Spr_obozn.Where(x => x.obozn == item.obozn && x.var == item.var).ToList();
        //            if (any_Obozn.Count > 0)
        //            {
        //                ViewData[EditResultKey] = string.Format($"Обозначение - {item.obozn} {item.var} уже существует.");
        //            }
        //            else
        //            {
        //                if (item.var != null)
        //                {
        //                    var OboznNotVar = db.Spr_obozn.Where(i => i.obozn == item.obozn && item.obozn != null).Select(l => l.obozn).FirstOrDefault();
        //                    if (String.IsNullOrEmpty(OboznNotVar))
        //                    {
        //                        Models.Msi.Spr_obozn _obozn = new Models.Msi.Spr_obozn
        //                        {
        //                            operation_date = DateTime.Now,
        //                            period_open_date = DateTime.Now,
        //                            obozn = item.obozn,
        //                            link_pkp = db.Spr_PKP.Where(x => x.pkp == "С").Select(p => p.Id).ToList()[0],
        //                            link_naim = item.link_naim,
        //                            //  link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0]
        //                        };
        //                        model_obozn.Add(_obozn);
        //                        db.SaveChanges();
        //                        ViewData[EditResultKey] = string.Format("Добавлено обозначение: '{0}{1}'", item.obozn, item.var);
        //                    }
        //                }
        //                Models.Msi.Spr_obozn __obozn = new Models.Msi.Spr_obozn
        //                {
        //                    operation_date = DateTime.Now,
        //                    period_open_date = DateTime.Now,
        //                    obozn = item.obozn,
        //                    var = item.var,
        //                    link_pkp = item.link_pkp,
        //                    link_naim = item.link_naim,
        //                    //link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).FirstOrDefault()
        //                };
        //                model_obozn.Add(__obozn);
        //                db.SaveChanges();
        //                ViewData[EditResultKey] = string.Format("Добавлено обозначение: '{0}{1}'", item.obozn, item.var);

        //                //foreach (var Id in listOfId)
        //                //{
        //                //    if (item.var != null)
        //                //    {
        //                //        Models.Msi.Spr_prim_dse _pr_dse = new Models.Msi.Spr_prim_dse
        //                //        {
        //                //            link_izd = Id,
        //                //            operation_date = DateTime.Now,
        //                //            operation = "insert",
        //                //            period_open_date = DateTime.Now,
        //                //            link_obozn = db.Spr_obozn.Where(x => x.obozn == item.obozn && x.var == item.var).Select(p => p.Id).ToList()[0],

        //                //            link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0]
        //                //        };
        //                //        model_prin_dse.Add(_pr_dse);
        //                //        db.SaveChanges();
        //                //    }
        //                //    else
        //                //    {
        //                //        Models.Msi.Spr_prim_dse _pr_dse = new Models.Msi.Spr_prim_dse
        //                //        {
        //                //            link_izd = Id,
        //                //            operation_date = DateTime.Now,
        //                //            operation = "insert",
        //                //            period_open_date = DateTime.Now,
        //                //            //link_obozn = db.Spr_obozn.Where(x => x.obozn == item.obozn).Select(p => p.Id).ToList()[0],
        //                //            link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0]
        //                //        };
        //                //        model_prin_dse.Add(_pr_dse);
        //                //        db.SaveChanges();
        //                //    }
        //                //}

        //                ViewData["newKey"] = db.Spr_obozn.Where(x => x.obozn == item.obozn).Select(p => p.Id).ToList()[0];
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData[EditResultKey] = e.Message;
        //        }
        //    }
        //    else
        //    {
        //        ViewData[EditResultKey] = string.Format("Please, correct all errors.");
        //    }
        //    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_GridView1Partial.cshtml", model_obozn);
        //}
        //public string GetTextByValues()
        //{
        //    string[] values = CheckBoxListExtension.GetSelectedValues<string>("per_izd");
        //    return String.Join(",", values);
        //}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult GridView1PartialUpdate(Spr_obozn item)
        ////{
        ////    var model = db.Spr_obozn;
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            Spr_obozn modelItem = model.FirstOrDefault(it => it.Id == item.Id);
        ////            if (modelItem != null)
        ////            {
        ////                Partial_Update up = new Partial_Update();
        ////                up.Partial_Update_Spr_obozn(modelItem, (Int32)Session["UserId"]);

        ////                this.UpdateModel(model);
        ////                db.SaveChanges();

        ////                Spr_obozn notation_reference = new Spr_obozn
        ////                {
        ////                    obozn = item.obozn,
        ////                    var = item.var,
        ////                    obozn_dos = item.obozn_dos,
        ////                    obozn_p = item.obozn_p,
        ////                    link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0],
        ////                    link_naim = item.link_naim,
        ////                    link_user = (Int32)Session["UserId"],
        ////                    link_pkp = item.link_pkp,
        ////                    operation_date = (DateTime?)DateTime.Now,
        ////                    operation = "insert",
        ////                    period_open_date = DateTime.Now
        ////                };
        ////                model.Add(notation_reference);
        ////                db.SaveChanges();
        ////                _obozn_Id_new = notation_reference.Id;
        ////            }

        ////            SqlConnection connection;
        ////            SqlParameter cex_bilo;
        ////            SqlParameter cex_stalo;
        ////            SqlParameter naim_sprav;
        ////            var ItWas = item.Id;
        ////            var HasBecome = _obozn_Id_new;
        ////            string sprav = "Spr_obozn";
        ////            string connetionString = "data source = i7-860; initial catalog = ASU_AVIA; user id = k6; password = jnltk35";
        ////            connection = new SqlConnection(connetionString);
        ////            try
        ////            {
        ////                connection.Open();
        ////                SqlCommand cmd = new SqlCommand("VALUE_AFTER_UPDATE", connection);
        ////                cmd.CommandType = CommandType.StoredProcedure;

        ////                cex_bilo = new SqlParameter("@paramIdOld", ItWas);
        ////                cex_bilo.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(cex_bilo);

        ////                cex_stalo = new SqlParameter("@paramIdNew", HasBecome);
        ////                cex_stalo.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(cex_stalo);

        ////                naim_sprav = new SqlParameter("@param", sprav);
        ////                naim_sprav.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(naim_sprav);

        ////                cmd.ExecuteNonQuery();
        ////                connection.Close();
        ////            }
        ////            catch (Exception)
        ////            {

        ////                ViewData[EditResultKey] = string.Format("Нет подключения к серверу Баз Данных!!!!!");
        ////            }
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    else
        ////        ViewData["EditError"] = "Please, correct all errors.";
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_GridView1Partial.cshtml", model);
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult GridView1PartialDelete(long Id)
        ////{
        ////    var model = db.Spr_obozn;
        ////    if (Id >= 0)
        ////    {
        ////        try
        ////        {
        ////            var item = model.FirstOrDefault(it => it.Id == Id);
        ////            if (item != null)
        ////            {
        ////                item.link_status = db.Status_dok.Where(x => x.status.StartsWith("Анну")).Select(p => p.Id).ToList()[0];
        ////                item.operation = "cancelled";
        ////                item.link_user = Session["UserId"];
        ////                item.operation_date = DateTime.Now;
        ////                item.period_close_date = DateTime.Now;
        ////                this.UpdateModel(item);
        ////            }
        ////            db.SaveChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_GridView1Partial.cshtml", model);
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult PrimDseGridViewPartialAddNew(Spr_prim_dse item, int key)
        ////{
        ////    var model = db.Spr_prim_dse.Where(i => i.link_kts == key).ToList();
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {

        ////            item.operation_date = DateTime.Now;
        ////            item.operation = "insert";
        ////            item.period_open_date = DateTime.Now;
        ////            item.link_user = (int)Session["UserId"];
        ////            item.link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0];
        ////            model.Add(item);
        ////            db.SaveChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        ViewData["EditError"] = "Please, correct all errors.";
        ////    }
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_PrimDseGridViewPartial.cshtml", model.ToList());
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult PrimDseGridViewPartialUpdate(Spr_prim_dse item)
        ////{
        ////    var model = db.Spr_prim_dse;
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
        ////            if (modelItem != null)
        ////            {
        ////                Partial_Update up = new Partial_Update();
        ////                up.Partial_Update_Spr_prim_dse(modelItem, (int)Session["UserId"]);
        ////                this.UpdateModel(modelItem);
        ////                db.SaveChanges();

        ////                Spr_prim_dse notation_reference = new Spr_prim_dse
        ////                {
        ////                    link_izd = item.link_izd,
        ////                    link_kts = item.link_kts,
        ////                    link_grrazdizd = item.link_grrazdizd,
        ////                    link_obozn = item.link_obozn,
        ////                    link_specif = item.link_specif,
        ////                    link_rascizd = item.link_rascizd,
        ////                    kizd = item.kizd,
        ////                    masizd = item.masizd,
        ////                    rascex = item.rascex,
        ////                    n_list = item.n_list,
        ////                    n_poz = item.n_poz,
        ////                    kp1 = item.kp1,
        ////                    kp2 = item.kp2,
        ////                    kp3 = item.kp3,
        ////                    CP1 = item.CP1,
        ////                    CP2 = item.CP2,
        ////                    CP3 = item.CP3,
        ////                    tk1 = item.tk1,
        ////                    tk2 = item.tk2,
        ////                    tk3 = item.tk3,
        ////                    ss = item.ss,
        ////                    spo = item.spo,
        ////                    link_user = (int)Session["UserId"],
        ////                    link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0],
        ////                    operation_date = (DateTime?)DateTime.Now,
        ////                    operation = "insert",
        ////                    period_open_date = DateTime.Now
        ////                };
        ////                model.Add(notation_reference);
        ////                db.SaveChanges();
        ////            }
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    else
        ////        ViewData["EditError"] = "Please, correct all errors.";
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_PrimDseGridViewPartial.cshtml", model.ToList());
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult PrimDseGridViewPartialDelete(System.Int64 Id)
        ////{
        ////    var model = db.Spr_prim_dse;
        ////    if (Id >= 0)
        ////    {
        ////        try
        ////        {
        ////            var item = model.FirstOrDefault(it => it.Id == Id);
        ////            if (item != null)
        ////            {
        ////                item.link_status = db.Status_dok.Where(x => x.status.StartsWith("Анну")).Select(p => p.Id).ToList()[0];
        ////                item.operation = "cancelled";
        ////                item.link_user = (int)Session["UserId"];
        ////                item.operation_date = DateTime.Now;
        ////                item.period_close_date = DateTime.Now;
        ////                this.UpdateModel(item);
        ////            }
        ////            db.SaveChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_PrimDseGridViewPartial.cshtml", model.ToList());
        ////}

        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult SpecifGridViewPartialAddNew(Spr_specif item, int key)
        ////{
        ////    var model = db.Spr_specif;
        ////    item.link_kts = key;
        ////    item.link_spec = key;
        ////    item.operation = "insert";
        ////    item.link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0];
        ////    item.operation_date = (DateTime?)DateTime.Now;
        ////    if (ModelState.IsValid)
        ////    {
        ////        //var any_Specification = db.Spr_obozn.Where(x => x.obozn == item.obozn && x.var == item.var).ToList();
        ////        var any_Specification = 1;//Потом убери эту затычку она временна тут должна быть проверка на дубли
        ////        if (any_Specification != 1)
        ////        {
        ////            //ViewData[EditResultKey] = string.Format($"Обозначение - {item.obozn} {item.var} уже существует.");
        ////        }
        ////        else
        ////        {
        ////            try
        ////            {
        ////                model.Add(item);
        ////                db.SaveChanges();
        ////                ViewData[EditResultKey] = string.Format($"Спецификация - {item.link_kts} с обозначением {item.link_obozn} сохранены на сервер.");
        ////            }
        ////            catch (Exception e)
        ////            {
        ////                ViewData[EditResultKey] = e.Message;
        ////            }
        ////        }
        ////    }
        ////    else
        ////    {
        ////        ViewData[EditResultKey] = string.Format("Please, correct all errors.");
        ////    }
        ////    var modelSpecif = db.Spr_specif.Where(i => i.link_spec == key);
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifGridViewPartial.cshtml", modelSpecif);
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult SpecifGridViewPartialUpdate(Spr_specif item)
        ////{
        ////    var model = db.Spr_specif;
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
        ////            if (modelItem != null)
        ////            {
        ////                Partial_Update up = new Partial_Update();
        ////                up.Partial_Update_Spr_specif(modelItem, (Int32)Session["UserId"]);
        ////                this.UpdateModel(model);
        ////                db.SaveChanges();

        ////                Spr_specif notation_reference = new Spr_specif
        ////                {
        ////                    link_obozn = item.link_obozn,
        ////                    link_kts = item.link_kts,
        ////                    link_pkp_T_TV = item.link_pkp_T_TV,
        ////                    link_kdan = item.link_kdan,
        ////                    link_komplekt = item.link_komplekt,
        ////                    link_razd_det = item.link_razd_det,
        ////                    link_spec = item.link_spec,
        ////                    ksb = item.ksb,
        ////                    prim_konstrukt = item.prim_konstrukt,
        ////                    prim_kts = item.prim_kts,
        ////                    prim_texn = item.prim_texn,
        ////                    link_status = db.Status_dok.Where(x => x.status.StartsWith("Действ")).Select(p => p.Id).ToList()[0],
        ////                    link_user = (Int32)Session["UserId"],
        ////                    operation_date = (DateTime?)DateTime.Now,
        ////                    operation = "insert",
        ////                    period_open_date = DateTime.Now
        ////                };
        ////                model.Add(notation_reference);
        ////                db.SaveChanges();
        ////            }
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData[EditResultKey] = e.Message;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        ViewData[EditResultKey] = "Please, correct all errors.";
        ////    }
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifGridViewPartial.cshtml", model.ToList());
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult SpecifGridViewPartialDelete(System.Int64 Id)
        ////{
        ////    var model = db.Spr_specif;
        ////    if (Id >= 0)
        ////    {
        ////        try
        ////        {
        ////            var item = model.FirstOrDefault(it => it.Id == Id);
        ////            if (item != null)
        ////            {
        ////                item.link_status = db.Status_dok.Where(x => x.status.StartsWith("Анну")).Select(p => p.Id).ToList()[0];
        ////                item.operation = "cancelled";
        ////                item.link_user = (int)Session["UserId"];
        ////                item.operation_date = DateTime.Now;
        ////                item.period_close_date = DateTime.Now;
        ////                this.UpdateModel(item);
        ////            }                     
        ////            db.SaveChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifGridViewPartial.cshtml", model.ToList());
        ////}

        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult SpecifPrimGridViewPartialAddNew(Spr_prim_dse item)
        ////{
        ////    var model = db.Spr_prim_dse;
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            model.Add(item);
        ////            db.SaveChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    else
        ////        ViewData["EditError"] = "Please, correct all errors.";
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifPrimGridViewPartial.cshtml", model.ToList());
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult SpecifPrimGridViewPartialUpdate(Spr_prim_dse item)
        ////{
        ////    var model = db.Spr_prim_dse;
        ////    if (ModelState.IsValid)
        ////    {
        ////        try
        ////        {
        ////            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
        ////            if (modelItem != null)
        ////            {
        ////                this.UpdateModel(modelItem);
        ////                db.SaveChanges();
        ////            }
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    else
        ////        ViewData["EditError"] = "Please, correct all errors.";
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifPrimGridViewPartial.cshtml", model.ToList());
        ////}
        ////[HttpPost, ValidateInput(false)]
        ////public ActionResult SpecifPrimGridViewPartialDelete(System.Int64 Id)
        ////{
        ////    var model = db.Spr_prim_dse;
        ////    if (Id >= 0)
        ////    {
        ////        try
        ////        {
        ////            var item = model.FirstOrDefault(it => it.Id == Id);
        ////            if (item != null)
        ////                model.Remove(item);
        ////            db.SaveChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            ViewData["EditError"] = e.Message;
        ////        }
        ////    }
        ////    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifPrimGridViewPartial.cshtml", model.ToList());
        ////}
        ////public ActionResult DistributeDetails(int id)
        ////{
        ////    var Obozn__Id = db.Spr_prim_dse.Where(x => x.Id == id).Select(i => i.link_obozn).ToList()[0];

        ////    var ListPrimDse = db.Spr_prim_dse.Where(x => x.link_obozn == Obozn__Id).ToList();

        ////    var Idcollection = db.Spr_prim_dse.Where(x => x.Id == id).ToList();
        ////    foreach (var item in Idcollection)
        ////    {
        ////        foreach (var i in ListPrimDse)
        ////        {
        ////            string connetionString = null;
        ////            SqlConnection connection;
        ////            SqlParameter id_param, link_specif_param, link_grrazdizd_param, kizd_param, kp1_param, kp2_param, kp3_param, tk1_param, tk2_param, tk3_param, masizd_param,
        ////            linkrascizd_param, ss_param, spo_param, n_list_param, n_poz_param, link_obozn_param, cp1_param, cp2_param, cp3_param;
        ////            connetionString = "data source = i7-860; initial catalog = ASU_AVIA; user id = k6; password = jnltk35";
        ////            connection = new SqlConnection(connetionString);
        ////            try
        ////            {
        ////                connection.Open();
        ////                SqlCommand cmd = new SqlCommand("DistributeDetails", connection);
        ////                cmd.CommandType = CommandType.StoredProcedure;

        ////                link_obozn_param = new SqlParameter("@link_obozn", Obozn__Id);
        ////                link_obozn_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(link_obozn_param);

        ////                id_param = new SqlParameter("@id", i.Id);
        ////                id_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(id_param);

        ////                link_specif_param = new SqlParameter("@link_specif", item.link_specif);
        ////                link_specif_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(link_specif_param);

        ////                link_grrazdizd_param = new SqlParameter("@link_grrazdizd", item.link_grrazdizd);
        ////                link_grrazdizd_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(link_grrazdizd_param);

        ////                kizd_param = new SqlParameter("@kizd", item.kizd);
        ////                kizd_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(kizd_param);

        ////                cp1_param = new SqlParameter("@cp1", item.CP1);
        ////                cp1_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(cp1_param);


        ////                cp2_param = new SqlParameter("@cp2", item.CP2 == null ? 1 : item.CP2);
        ////                cp2_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(cp2_param);


        ////                cp3_param = new SqlParameter("@cp3", item.CP3 == null ? 1 : item.CP3);
        ////                cp3_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(cp3_param);


        ////                kp1_param = new SqlParameter("@kp1", item.kp1);
        ////                kp1_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(kp1_param);

        ////                kp2_param = new SqlParameter("@kp2", item.kp2);
        ////                kp2_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(kp2_param);

        ////                kp3_param = new SqlParameter("@kp3", item.kp3);
        ////                kp3_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(kp3_param);

        ////                tk1_param = new SqlParameter("@tk1", item.tk1);
        ////                tk1_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(tk1_param);

        ////                tk2_param = new SqlParameter("@tk2", item.tk2);
        ////                tk2_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(tk2_param);

        ////                tk3_param = new SqlParameter("@tk3", item.tk3);
        ////                tk3_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(tk3_param);

        ////                masizd_param = new SqlParameter("@masizd", item.masizd);
        ////                masizd_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(masizd_param);

        ////                linkrascizd_param = new SqlParameter("@linkrascizd", item.link_rascizd);
        ////                linkrascizd_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(linkrascizd_param);

        ////                ss_param = new SqlParameter("@ss", item.ss);
        ////                ss_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(ss_param);

        ////                spo_param = new SqlParameter("@spo", item.spo);
        ////                spo_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(spo_param);

        ////                n_list_param = new SqlParameter("@n_list", item.n_list);
        ////                n_list_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(n_list_param);

        ////                n_poz_param = new SqlParameter("@n_poz", item.n_poz);
        ////                n_poz_param.Direction = ParameterDirection.Input;
        ////                cmd.Parameters.Add(n_poz_param);

        ////                cmd.ExecuteNonQuery();
        ////                connection.Close();
        ////            }
        ////            catch (Exception)
        ////            {
        ////                TempData["msg"] = "<script>alert('Нет подключения к серверу Баз Данных!);</script>";
        ////            }
        ////        }
        ////    }
        ////    return View("Get_Sostav");
        ////}




        //[ValidateInput(false)]
        //public ActionResult SpecifGridView_new()
        //{
        //    var key = Session["specificationKey"];

        //    var link_specification = GetLinkSpecification((int)key);

        //    var model = db.Spr_specif.Where(item => item.link_spec == link_specification);

        //    var model2 = db.Spr_specif.Where(item => item.link_spec == link_specification).ToList();

        //    return PartialView("~/Areas/msi/Views/Sostav_Izdelia/_SpecifGridView_new.cshtml", model);
        //}
    }
}