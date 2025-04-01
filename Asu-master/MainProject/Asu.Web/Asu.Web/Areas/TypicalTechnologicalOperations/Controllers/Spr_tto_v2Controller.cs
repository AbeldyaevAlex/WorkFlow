using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using Asu.Web.ViewModel;
using System.Data.Entity;
using Asu.Web.Models.ContextDb;
using System.Configuration;
using System.Web.Services.Description;

namespace Asu.Web.Areas.TypicalTechnologicalOperations.Controllers
{
    //public class ViewModel
    //{
    //    public List<Group_TTO> Uniq_link_komp { get; set; }
    //    public IQueryable<Models.TypicalTechnologicalOperations.Spr_tto> Spr_tto { get; set; }
    //}
    //public class Group<K, T>
    //{
    //    public K Key;
    //    public IEnumerable<T> Values;
    //}
    //public class Group_TTO
    //{
    //    public long link_kod_tto { get; set; }
    //    public string km { get; set; }
    //    public string dbt { get; set; }
    //    public string dsh { get; set; }       
    //    public decimal? ves { get; set; }
    //    public string nm_mater { get; set; }
    //    public string marka_mater { get; set; }
    //    public string gost { get; set; }
    //    public string krat_naim_eizm { get; set; }
    //    public int kgr { get; set; }
    //    public int kod_sklad { get; set; }
    //    public long link_ogt { get; set; }
    //    public string pr_km { get; set; }
    //    public long link_prpokr { get; set; }
    //}
    //public class FullSkmInfo
    //{
    //    public long Id { get; set; }
    //    public string FullInfo { get; set; }
    //}
    //public class Spr_tto_v2Controller : Controller
    //{
    //    private string connetionString = ConfigurationManager.ConnectionStrings["AsuAviaContext"].ConnectionString;
    //    private AsuAviaDbContext _context;
    //    private ApplicationDbContext _usercontext;

    //    public Spr_tto_v2Controller()
    //    {
    //        _context = new AsuAviaDbContext();
    //        _usercontext = new ApplicationDbContext();            
    //    }      
    //    public List<Group_TTO> Get_UniQ_TTO()
    //    {
    //        Models.TypicalTechnologicalOperations.Spr_tto list = new Models.TypicalTechnologicalOperations.Spr_tto();
    //        SqlConnection connection = new SqlConnection(connetionString);
    //        connection.Open();
    //        SqlCommand command = new SqlCommand(@"SELECT * FROM Grouping_tto", connection);
    //        List<Group_TTO> _res = new List<Group_TTO>();
    //        SqlDataReader dr = command.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            _res.Add(new Group_TTO()
    //            {
    //                link_kod_tto = dr.GetInt64(0),
    //                km = dr.GetString(1),
    //                dbt = dr.GetString(2),
    //                dsh = dr.GetString(3),
    //                ves = dr.GetDecimal(4),
    //                nm_mater = dr.GetString(5),
    //                marka_mater = dr.GetString(6),
    //                gost = dr.GetString(7),
    //                krat_naim_eizm = dr.GetString(8),
    //                kgr = dr.GetInt32(9),
    //                kod_sklad = dr.GetInt32(10),
    //                link_ogt = dr.GetInt64(11),
    //                pr_km = dr.GetString(12),
    //                link_prpokr = dr.GetInt64(13)
    //            });
    //        }
    //        connection.Close();
    //        return _res.OrderBy(i => i.km).ToList();
    //    }
    //    public List<FullSkmInfo> Get_Full_Skm_Info()
    //    {
    //        Models.TypicalTechnologicalOperations.Spr_tto list = new Models.TypicalTechnologicalOperations.Spr_tto();
    //        SqlConnection connection = new SqlConnection(connetionString);
    //        connection.Open();
    //        SqlCommand command = new SqlCommand(@"SELECT DISTINCT * FROM FULL_SKM_INFO ORDER BY Id", connection);
    //        List<FullSkmInfo> _fullListSkmInfo = new List<FullSkmInfo>();

    //        SqlDataReader dr = command.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            _fullListSkmInfo.Add(new FullSkmInfo()
    //            {
    //                Id = dr.GetInt64(0),
    //                FullInfo = dr.GetString(3),
    //            });
    //        }
    //        connection.Close();
    //        return _fullListSkmInfo.ToList();
    //    }
    //    public IEnumerable<FullSkmInfo> GetListInfo(IList<FullSkmInfo> item)
    //    {
    //        var skm_info = (from skm in item
    //                        select skm);
    //        Session["Skm_Info"] = skm_info;
    //        return skm_info;
    //    }
    //    public IQueryable<Models.TypicalTechnologicalOperations.Spr_tto> Get_TTO(object masterRowKey)

    //    {
    //        var skm_2 = (from skm in _context.Spr_skm
    //                     join mark in _context.Mark_mater
    //                     on skm.link_marka equals mark.Id
    //                     join nm_mater in _context.Nm_mater
    //                     on skm.link_nm_skm equals nm_mater.Id
    //                     join gost in _context.GOST_mater
    //                     on skm.link_gost equals gost.Id
    //                     join eizm in _context.Spr_eizm
    //                     on skm.link_eizm equals eizm.Id
    //                     select new
    //                     {
    //                         skm.Id,
    //                         skm.km,
    //                         skm.ves,
    //                         mark.marka_mater,
    //                         skm.dbt,
    //                         skm.dsh,
    //                         nm_mater.nm_mater1,
    //                         gost.gost,
    //                         eizm.krat_naim_eizm
    //                     });
    //        var list_ = skm_2.ToList();
    //        ViewBag.ListSkm = list_;
    //        //if (masterRowKey == null)
    //        //{
    //        //    //var model = AutoMapper.Mapper.Map<IEnumerable<TtoViewModel>>(db.Spr_tto);
    //        //    var model = db.Spr_tto;
    //        //    //IQueryable<TtoViewModel> selmodel = (IQueryable<TtoViewModel>)model;
    //        //    return model;
    //        //}
    //        //else
    //        //{
    //        string Id_link_kod_tto = null;
    //        if (masterRowKey != null)
    //        {
    //            Id_link_kod_tto = masterRowKey.ToString();
    //        }
    //        else
    //        {
    //            Id_link_kod_tto = null;
    //        }

    //        int Id;
    //        int.TryParse(Id_link_kod_tto, out Id);
    //        Session["Row"] = Id;
    //        //var model = db.Spr_tto.Where(i => i.link_kod_TTO == Id).ToList();
    //        //var model = AutoMapper.Mapper.Map<IEnumerable<TtoViewModel>>(db.Spr_tto);
    //        //model.Where(i => i.link_kod_TTO == Id).ToList();
    //        //IQueryable<TtoViewModel> selmodel = (IQueryable<TtoViewModel>)model;
    //        var model = _context.Spr_tto.Where(i => i.link_kod_TTO == Id);
    //        var modeltest = _context.Spr_tto.Where(i => i.link_kod_TTO == Id).ToList();
    //        return model;
    //        //}
    //    }
    //    public static IQueryable GetGroup(IQueryable<Models.TypicalTechnologicalOperations.Spr_tto> tto)
    //    {
    //        using (var context = new AsuAviaDbContext())
    //        {
    //            var result = context.Spr_tto.GroupBy(p => p.link_kod_TTO);
    //            return result;
    //        }
    //    }
    //    public ActionResult Index()
    //    {
    //        var FullListSkmInfo = Get_Full_Skm_Info();
    //        var InfoSkm = GetListInfo(FullListSkmInfo);
    //        ViewBag.full_km_tto = new SelectList(InfoSkm, "Id", "FullInfo");
    //        ViewBag.full_kod_komp = new SelectList(_context.Spr_tto, "Id", "link_kod_komp");
    //        return View(new ViewModel { Uniq_link_komp = Get_UniQ_TTO(), Spr_tto = Get_TTO(null) });
    //    }
    //    public ActionResult GridViewPartialUniq()
    //    {
    //        return PartialView("_GridViewPartialUniq", Get_UniQ_TTO());
    //    }
    //    public ActionResult GridViewPartialTTO(string kod_komp)
    //    {
    //        if (kod_komp != null)
    //        {
    //            var kod_komp_id = int.Parse(kod_komp);
    //            ViewData["_kod_komp"] = kod_komp_id;

    //            var komp_TTO = (from tto in _context.Spr_tto
    //                            where tto.link_kod_TTO == kod_komp_id
    //                            select tto);
    //            //return PartialView("_GridViewPartialTTO", komp_TTO);
    //            return PartialView("_GridViewPartialTTO", Get_TTO(kod_komp_id));
    //        }
    //        else
    //        {
    //            var tto_Id = long.Parse(Request.Params["MasterRowKey"]);


    //            var TipicalTehnOper = _context.Spr_skm.Where(i => i.Id == tto_Id).Select(c => c.km).FirstOrDefault();
    //            Session["TipicalTehnOper"]  = TipicalTehnOper;

    //            return PartialView("_GridViewPartialTTO", Get_TTO(Request.Params["MasterRowKey"]));               
    //        }           
    //    }
    //    [ValidateInput(false)]
    //    public ActionResult GridViewPartial_TTO()
    //    {
    //        Spr_tto list = new Spr_tto();
    //        SqlConnection connection = new SqlConnection(connetionString);
    //        connection.Open();
    //        SqlCommand command = new SqlCommand(@"SELECT * FROM Grouping_tto", connection);
    //        List<Group_TTO> _res = new List<Group_TTO>();
    //        SqlDataReader dr = command.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            _res.Add(new Group_TTO()
    //            {
    //                link_kod_tto = dr.GetInt64(0),
    //                km = dr.GetString(1),
    //                dbt = dr.GetString(2),
    //                dsh = dr.GetString(3),
    //                ves = dr.GetDecimal(4),
    //                nm_mater = dr.GetString(5),
    //                marka_mater = dr.GetString(6),
    //                gost = dr.GetString(7),
    //                krat_naim_eizm = dr.GetString(8),
    //                kgr = dr.GetInt32(9),
    //                kod_sklad = dr.GetInt32(10),
    //                link_ogt = dr.GetInt64(11),
    //                pr_km = dr.GetString(12),
    //                link_prpokr = dr.GetInt64(13)
    //            });
    //        }
    //        connection.Close();
    //        return PartialView("_GridViewPartial_TTO", _res.ToList());
    //    }

    //    /// <summary>
    //    ///  CRUD операции для кода ТТО.
    //    /// </summary>
    //    /// 
    //    [HttpPost, ValidateInput(false)]
    //    public ActionResult GridViewPartial_TTOAddNew(Models.TypicalTechnologicalOperations.Spr_tto item)
    //    {
    //        var viewModel = AutoMapper.Mapper.Map<IEnumerable<TtoViewModel>>(_context.Spr_tto);
    //        var model = _context.Spr_tto;
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                Models.TypicalTechnologicalOperations.Spr_tto _tto = new Models.TypicalTechnologicalOperations.Spr_tto()
    //                {
    //                    operation_date = DateTime.Now,
    //                    operation = "insert",
    //                    period_open_date = DateTime.Now,
    //                    link_user = _usercontext.Users.Where(x => x.Email == User.Identity.Name).Select(x => x.Id).FirstOrDefault(),
    //                    link_status = item.link_status,
    //                    link_kod_TTO = item.link_kod_TTO,
    //                    link_kod_komp = item.link_kod_komp,
    //                    link_cizg = item.link_cizg,
    //                    link_prkm = item.link_prkm,
    //                    link_prpokr = item.link_prpokr,
    //                    krat = item.krat,
    //                    nrm = item.nrm,
    //                    nrvp = item.nrvp,
    //                    sort_kod_komp = item.sort_kod_komp,
    //                    sort_kod_TTO = item.sort_kod_TTO
    //                };
    //                model.Add(_tto);
    //                _context.SaveChanges();
    //            }
    //            catch (Exception e)
    //            {
    //                ViewData["EditError"] = e.Message;
    //            }
    //        }
    //        else
    //        {
    //            ViewData["EditError"] = "Please, correct all errors.";
    //        }
    //        return RedirectToAction("Index");
    //    }
    //    [HttpPost, ValidateInput(false)]
    //    public ActionResult GridViewPartial_TTOUpdate(Group_TTO item)
    //    {
    //        var model = _context.Spr_tto;
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                var modelItem = model.FirstOrDefault(it => it.link_kod_TTO == item.link_kod_tto);
    //                if (modelItem != null)
    //                {
    //                    this.UpdateModel(modelItem);
    //                    _context.SaveChanges();
    //                }
    //            }
    //            catch (Exception e)
    //            {
    //                ViewData["EditError"] = e.Message;
    //            }
    //        }
    //        else
    //        {
    //            ViewData["EditError"] = "Please, correct all errors.";
    //        }
    //        return RedirectToAction("Index");
    //    }
    //    [HttpPost, ValidateInput(false)]
    //    public ActionResult GridViewPartial_TTODelete(Group_TTO Id)
    //    {
    //        var model = _context.Spr_tto.Where(i => i.link_kod_TTO == Id.link_kod_tto).ToList();
    //        var status_Id = _context.DocumentStatus.Where(p => p.status == "Аннулирован").Select(i => i.Id).FirstOrDefault();
    //        if (model != null)
    //        {
    //            foreach (var item in model)
    //            {
    //                item.link_status = status_Id;
    //                item.operation = "Cancelled";
    //                item.link_user = _usercontext.Users.Where(x => x.Email == User.Identity.Name).Select(x => x.Id).FirstOrDefault();
    //                item.operation_date = DateTime.Now;
    //                item.period_close_date = DateTime.Now;
    //                this.UpdateModel(item);
    //            }
    //        }
    //        _context.SaveChanges();
    //        return RedirectToAction("Index");
    //    }
    //    /// <summary>
    //    ///  CRUD операции для кода компонента.
    //    /// </summary>
    //    [ValidateInput(false)]
    //    public ActionResult GridViewKodKomp(string link_kod_tto)
    //    {
    //        if (link_kod_tto == null)
    //        {
    //            var model = _context.Spr_tto.ToList();
    //            return View("_GridView_Kod_Komp", model);
    //        }
    //        else
    //        {
    //            int Id_link_kod_tto;
    //            int.TryParse(link_kod_tto, out Id_link_kod_tto);
    //            var model = _context.Spr_tto.Where(i => i.link_kod_TTO == Id_link_kod_tto).ToList();
    //            //var model = db.Spr_tto.ToList();
    //            return View("_GridView_Kod_Komp", model);
    //        }

    //    }
    //    [HttpPost, ValidateInput(false)]
    //    public ActionResult GridView1PartialAddNew(Models.TypicalTechnologicalOperations.Spr_tto item, int masterRowKey)
    //    {
    //        var model = _context.Spr_tto;
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                Models.TypicalTechnologicalOperations.Spr_tto _tto = new Models.TypicalTechnologicalOperations.Spr_tto()
    //                {
    //                    operation_date = DateTime.Now,
    //                    operation = "insert",
    //                    period_open_date = DateTime.Now,
    //                    link_user = _usercontext.Users.Where(x => x.Email == User.Identity.Name).Select(x => x.Id).FirstOrDefault(),
    //                    link_status = item.link_status,
    //                };
    //                model.Add(item);
    //                _context.SaveChanges();
    //            }
    //            catch (Exception e)
    //            {
    //                ViewData["EditError"] = e.Message;
    //            }
    //        }
    //        else
    //            ViewData["EditError"] = "Please, correct all errors.";
    //        return PartialView("_GridView_Kod_Komp", model);
    //    }
    //    //[HttpPost, ValidateInput(false)]
    //    //public ActionResult GridView1PartialUpdate(Spr_tto item)
    //    //{
    //    //    var model = AutoMapper.Mapper.Map<IEnumerable<TtoViewModel>>(_context.Spr_tto);
    //    //    //var model = _context.Spr_tto;
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        try
    //    //        {
    //    //            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
    //    //            if (modelItem != null)
    //    //            {
    //    //                this.UpdateModel(modelItem);
    //    //                _context.SaveChanges();
    //    //            }
    //    //        }
    //    //        catch (Exception e)
    //    //        {
    //    //            ViewData["EditError"] = e.Message;
    //    //        }
    //    //    }
    //    //    else
    //    //        ViewData["EditError"] = "Please, correct all errors.";
    //    //    return PartialView("_GridView_Kod_Komp", model);
    //    //}
    //    [HttpPost, ValidateInput(false)]
    //    public ActionResult GridView1PartialDelete(long Id)
    //    {
    //        var model = _context.Spr_tto;
    //        if (Id >= 0)
    //        {
    //            try
    //            {
    //                var item = model.FirstOrDefault(it => it.Id == Id);
    //                if (item != null)
    //                    model.Remove(item);
    //                _context.SaveChanges();
    //            }
    //            catch (Exception e)
    //            {
    //                ViewData["EditError"] = e.Message;
    //            }
    //        }
    //        return PartialView("_GridView_Kod_Komp", model);
    //    }
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            _context.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}