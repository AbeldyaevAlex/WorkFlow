using Asu.Web.Models;
using DevExpress.Web.Mvc;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    [Authorize]
    public class Spr_rascexController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();
        //private string list;
        public Spr_rascexController()
        {

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.Spr_rascex;
            return PartialView("_GridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Asu.Web.Models.Spr_rascex item)
        {
            var model = db.Spr_rascex;

            var ci_11 = item.CI11 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI11).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_12 = item.CI12 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI12).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_13 = item.CI13 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI13).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_2 = item.CI2 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI2).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_3 = item.CI3 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI3).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_4 = item.CI4 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI4).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_5 = item.CI5 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI5).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_6 = item.CI6 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI6).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_7 = item.CI7 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI7).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_8 = item.CI8 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI8).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_9 = item.CI9 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI9).Select(j => j.cex).ToList())[0] + " " : "";
            var ci_10 = item.CI10 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CI10).Select(j => j.cex).ToList())[0] + " " : "";

            var cto = item.CTO != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CTO).Select(j => j.cex).ToList())[0] + " " : "";

            var cpk1 = item.CPK1 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CPK1).Select(j => j.cex).ToList())[0] + " " : "";
            var cpk2 = item.CPK2 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CPK2).Select(j => j.cex).ToList())[0] + " " : "";
            var cpk3 = item.CPK3 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CPK3).Select(j => j.cex).ToList())[0] + " " : "";
            var cpk4 = item.CPK4 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CPK4).Select(j => j.cex).ToList())[0] + " " : "";

            var cus1 = item.CUS1 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS1).Select(j => j.cex).ToList())[0] + " " : "";
            var cus2 = item.CUS2 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS2).Select(j => j.cex).ToList())[0] + " " : "";
            var cus3 = item.CUS3 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS3).Select(j => j.cex).ToList())[0] + " " : "";
            var cus4 = item.CUS4 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS4).Select(j => j.cex).ToList())[0] + " " : "";
            var cus5 = item.CUS5 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS5).Select(j => j.cex).ToList())[0] + " " : "";
            var cus6 = item.CUS5 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS6).Select(j => j.cex).ToList())[0] + " " : "";
            var cus7 = item.CUS7 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS7).Select(j => j.cex).ToList())[0] + " " : "";
            var cus8 = item.CUS8 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS8).Select(j => j.cex).ToList())[0] + " " : "";
            var cus9 = item.CUS9 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS9).Select(j => j.cex).ToList())[0] + " " : "";
            var cus10 = item.CUS10 != null ? " " + (db.Spr_cex.Where(x => x.Id == item.CUS10).Select(j => j.cex).ToList())[0] + " " : "";
            string rascex_cii = "";
            if ((String.IsNullOrEmpty(ci_12)) && (String.IsNullOrEmpty(ci_13)))
            {
                rascex_cii = $"{ci_11}";
            }
            if ((String.IsNullOrEmpty(ci_13) && !(String.IsNullOrEmpty(ci_12))))
            {
                rascex_cii = $"{ci_11} / {ci_12}";
            }
            if (!(String.IsNullOrEmpty(ci_11) && !(String.IsNullOrEmpty(ci_12))) && !(String.IsNullOrEmpty(ci_13)))
            {
                rascex_cii = $"{ci_11} / {ci_12} / {ci_13}";
            }

            Dictionary<string, string> dict_izg = new Dictionary<string, string>();
            dict_izg.Add("ci_2", ci_2);
            dict_izg.Add("ci_3", ci_3);
            dict_izg.Add("ci_4", ci_4);
            dict_izg.Add("ci_5", ci_5);
            dict_izg.Add("ci_6", ci_6);
            dict_izg.Add("ci_7", ci_7);
            dict_izg.Add("ci_8", ci_8);
            dict_izg.Add("ci_9", ci_9);
            dict_izg.Add("ci_10", ci_10);
            Dictionary<string, string> dict_cpk = new Dictionary<string, string>();
            dict_cpk.Add("cpk1", cpk1);
            dict_cpk.Add("cpk2", cpk2);
            dict_cpk.Add("cpk3", cpk3);
            dict_cpk.Add("cpk4", cpk4);
            Dictionary<string, string> dict_cusl = new Dictionary<string, string>();
            dict_cusl.Add("cus1", cus1);
            dict_cusl.Add("cus2", cus2);
            dict_cusl.Add("cus3", cus3);
            dict_cusl.Add("cus4", cus4);
            dict_cusl.Add("cus5", cus5);
            dict_cusl.Add("cus6", cus6);
            dict_cusl.Add("cus7", cus7);
            dict_cusl.Add("cus8", cus8);
            dict_cusl.Add("cus9", cus9);
            dict_cusl.Add("cus10", cus10);

            string cexa_izg = "";
            foreach (var it in dict_izg)
            {
                if (!(String.IsNullOrEmpty(it.Value)))
                {
                    cexa_izg = cexa_izg + ("-" + it.Value);
                }
            }
            string cexa_cpk = "";
            foreach (var it in dict_cpk)
            {
                if (!(String.IsNullOrEmpty(it.Value)))
                {
                    cexa_cpk = cexa_cpk + (it.Key == "cpk1" ? it.Value : "," + it.Value);
                }
            }
            string cexa_cus = "";
            foreach (var it in dict_cusl)
            {
                if (!(String.IsNullOrEmpty(it.Value)))
                {
                    cexa_cus = cexa_cus + (it.Key == "cus1" ? it.Value : "," + it.Value);
                }
            }
            string fikt_cp = "";
            if (!(String.IsNullOrEmpty(ci_11)) && (String.IsNullOrEmpty(ci_12)) && (String.IsNullOrEmpty(ci_13)))
            {
                fikt_cp = "- цп1 ";
            }
            if (!(String.IsNullOrEmpty(ci_11)) && !(String.IsNullOrEmpty(ci_12)) && (String.IsNullOrEmpty(ci_13)))
            {
                fikt_cp = " - цп1 / цп2";
            }
            if (!(String.IsNullOrEmpty(ci_11)) && !(String.IsNullOrEmpty(ci_12)) && !(String.IsNullOrEmpty(ci_13)))
            {
                fikt_cp = " - цп1 / цп2 / цп3 ";
            }

            //StringBuilder sb = new StringBuilder();

            var rascex_itog = $"{rascex_cii}{cexa_izg}{fikt_cp}";
            if (!String.IsNullOrEmpty(cto))
            {
                rascex_itog += $";{cto}";
            }

            if (!String.IsNullOrEmpty(cexa_cpk))
            {
                if (!String.IsNullOrEmpty(cto))
                {
                    rascex_itog += $";{cexa_cpk}";
                }
                else
                {
                    rascex_itog += $"; ;{cexa_cpk}";
                }
            }

            if (!String.IsNullOrEmpty(cexa_cus) && !String.IsNullOrEmpty(cexa_cpk))
            {
                rascex_itog += $";{cexa_cus}";
            }
            else
            {
                if (!String.IsNullOrEmpty(cexa_cus) && String.IsNullOrEmpty(cexa_cpk) && !String.IsNullOrEmpty(cto))
                {
                    rascex_itog += $"; ;{cexa_cus}";
                }
                else
                {
                    if (!String.IsNullOrEmpty(cexa_cus) && String.IsNullOrEmpty(cexa_cpk) && String.IsNullOrEmpty(cto))
                    {
                        rascex_itog += $"; ; ;{cexa_cus}";
                    }

                }
            }


            item.rascex_small = rascex_itog;
            if (ModelState.IsValid)
            {
                //try
                //{
                    //ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
                    //list = item.rascex_small;
                    //foreach (var bd in db.Spr_rascex)
                    //{
                    //    if (bd.rascex_small == list)
                    //    {
                    //        ViewData["EditError"] = string.Format("Запись уже существует.");
                    //    }
                    //    else
                    //    {
                    //        model.Add(item);
                    //        db.SaveChanges();
                    //        ViewData["EditError"] = string.Format("Запись сохранена на сервер");
                    //    }
                    //};
                    model.Add(item);
                    db.SaveChanges();
                //}
                //catch (Exception e)
                //{
                //    ViewData["EditError"] = e.Message;
                //}
            }
            else
                ViewData["EditError"] = "Есть обязательные поля для заполнения.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Asu.Web.Models.Spr_rascex item)
        {
            var model = db.Spr_rascex;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int64 Id)
        {
            var model = db.Spr_rascex;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model.ToList());
        }
        public ActionResult Create()
        {
            SelectList cex = new SelectList(db.Spr_cex, "Id", "cex");
            ViewBag.Cex = cex;
            SelectList status = new SelectList(db.Status_dok, "Id", "status");
            ViewBag.Status = status;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Spr_rascex item)
        {
            var model = db.Spr_rascex;
            model.Add(item);
            db.SaveChanges();
            return PartialView("Index", model.ToList());
        }
    }
}



//string rascex_izg = "";
//var cex_dic = new Dictionary<string, string>();
//for (int i = 2; i < 10; i++)
//{
//    cex_dic["cex" + $"{i.ToString()}"] =  "item.ci_" + $"{i.ToString()}";
//    foreach (var it in cex_dic)
//    {
//        if ((String.IsNullOrEmpty(it.Value)))
//        {

//        }
//        else
//        {

//        }
//    }
//}


//if (!(String.IsNullOrEmpty(ci_2) && (String.IsNullOrEmpty(ci_3))) && (String.IsNullOrEmpty(ci_4)) && (String.IsNullOrEmpty(ci_5)) && (String.IsNullOrEmpty(ci_6)) && (String.IsNullOrEmpty(ci_7)) && (String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && (String.IsNullOrEmpty(ci_4)) && (String.IsNullOrEmpty(ci_5)) && (String.IsNullOrEmpty(ci_6)) && (String.IsNullOrEmpty(ci_7)) && (String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && (String.IsNullOrEmpty(ci_5)) && (String.IsNullOrEmpty(ci_6)) && (String.IsNullOrEmpty(ci_7)) && (String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && !(String.IsNullOrEmpty(ci_5)) && (String.IsNullOrEmpty(ci_6)) && (String.IsNullOrEmpty(ci_7)) && (String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}-{ci_5}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && !(String.IsNullOrEmpty(ci_5)) && !(String.IsNullOrEmpty(ci_6)) && (String.IsNullOrEmpty(ci_7)) && (String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}-{ci_5}-{ci_6}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && !(String.IsNullOrEmpty(ci_5)) && !(String.IsNullOrEmpty(ci_6)) && !(String.IsNullOrEmpty(ci_7)) && (String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}-{ci_5}-{ci_6}-{ci_7}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && !(String.IsNullOrEmpty(ci_5)) && !(String.IsNullOrEmpty(ci_6)) && !(String.IsNullOrEmpty(ci_7)) && !(String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}-{ci_5}-{ci_6}-{ci_7}-{ci_8}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && !(String.IsNullOrEmpty(ci_5)) && !(String.IsNullOrEmpty(ci_6)) && !(String.IsNullOrEmpty(ci_7)) && !(String.IsNullOrEmpty(ci_8)) && (String.IsNullOrEmpty(ci_9)) && (String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}-{ci_5}-{ci_6}-{ci_7}-{ci_8}-{ci_9}";
//}
//if (!(String.IsNullOrEmpty(ci_2) && !(String.IsNullOrEmpty(ci_3))) && !(String.IsNullOrEmpty(ci_4)) && !(String.IsNullOrEmpty(ci_5)) && !(String.IsNullOrEmpty(ci_6)) && !(String.IsNullOrEmpty(ci_7)) && !(String.IsNullOrEmpty(ci_8)) && !(String.IsNullOrEmpty(ci_9)) && !(String.IsNullOrEmpty(ci_10)))
//{
//    var rascex_ii = $"-{ci_2}-{ci_3}-{ci_4}-{ci_5}-{ci_6}-{ci_7}-{ci_8}-{ci_9}-{ci_10}";
//}