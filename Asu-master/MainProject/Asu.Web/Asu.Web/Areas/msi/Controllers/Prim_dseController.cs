
using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Prim_dseController : Controller
    {
        //private string ControllerName = "SprSpecifController";
        private const string EditResultKey = "EditResultKey";
        private int Izd_Id;
        private int Obozn_Id;
        //private string obozn_prim;


        public ActionResult Index(string obozn, string ControllerName)
        {
            //int Id_Obozn;
            //int.TryParse(obozn, out Id_Obozn);
            //try
            //{                
            //        obozn_prim = db.Spr_obozn.Where(x => x.Id == Id_Obozn).Select(j => j.obozn_p).FirstOrDefault();
            //    if (ControllerName == "SprSpecifController")
            //    {
            //        obozn_prim = db.Spr_specif.Where(x => x.Id == Id_Obozn).Select(j => j.Spr_obozn1.obozn_p).FirstOrDefault();
            //    }
            //}
            //catch (Exception)
            //{
            //    obozn_prim = db.Spr_specif.Where(x => x.Id == Id_Obozn).Select(j => j.Spr_obozn1.obozn_p).FirstOrDefault();
            //    if (ControllerName == "SprSpecifController")
            //    {
            //        obozn_prim = db.Spr_specif.Where(x => x.Id == Id_Obozn).Select(j => j.Spr_obozn1.obozn_p).FirstOrDefault();
            //    }                
            //}

            //ViewData["obozn"] = obozn_prim;
            return View();
        }
        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();
        [ValidateInput(false)]
        public ActionResult GridView1Partial(string obozn, string ControllerName)
        {
            if (ControllerName == "SprSpecifController")
            {
                int.TryParse(obozn, out Izd_Id);
                var model = db.Spr_prim_dse.Where(x => x.link_specif == Izd_Id).ToList();// Массив изделий для применяемости(спецификация)
                ViewData["Id"] = Izd_Id;
                Session["Dse_ID"] = Izd_Id;
                ViewData["ConName"] = ControllerName;
                
                return PartialView("_GridView1Partial", model.ToList());
            }
            else
            {
                int.TryParse(obozn, out Izd_Id);
                var model = db.Spr_prim_dse.Where(x => x.link_obozn == Izd_Id).ToList(); // Массив изделий для применяемости(номенклатура)
                ViewData["Id"] = Izd_Id;
                Session["Dse_ID"] = Izd_Id;
                ViewData["ConName"] = ControllerName;
                return PartialView("_GridView1Partial", model.ToList());
            }
        }


        public string Add_Potrebitel(Spr_prim_dse param)
        {
            var rascexSmall = param.link_rascizd != null ? (db.Spr_rascex.Where(x => x.Id == param.link_rascizd).Select(j => j.rascex_small).ToList())[0] : "";
            var cp_1 = param.CP1 != null ? (db.Spr_cex.Where(x => x.Id == param.CP1).Select(j => j.cex).ToList())[0] : "";
            var cp_2 = param.CP2 != null ? " / " + (db.Spr_cex.Where(x => x.Id == param.CP2).Select(j => j.cex).ToList())[0] : "";
            var cp_3 = param.CP3 != null ? " / " + (db.Spr_cex.Where(x => x.Id == param.CP3).Select(j => j.cex).ToList())[0] : "";
            var rascexItog = rascexSmall.Replace("cp1", cp_1).Replace(" / cp2", cp_2).Replace(" / cp3", cp_3);
            return rascexItog;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(Asu.Web.Models.Spr_prim_dse item, string ID, string ControllerName)
        {
            var model = db.Spr_prim_dse;
            if (ModelState.IsValid)
            {
                try
                {
                    string rascexItog = Add_Potrebitel(item);
                    item.rascex = rascexItog;
                    int.TryParse(ID, out Obozn_Id);
                    if (ControllerName == "SprSpecifController")
                    {
                        var chosenForInsertPrimSpecificanion = db.Spr_prim_dse.Where(x => x.link_obozn == Obozn_Id).ToList();
                        item.link_specif = Obozn_Id;
                        var obozn = db.Spr_specif.Where(x => x.Id == Obozn_Id).Select(j => j.link_obozn).ToList()[0];
                        item.link_obozn = obozn;

                        var kts_Id = db.Spr_specif.Where(x => x.Id == item.Id).Select(j => j.link_kts).ToList()[0];
                        item.link_obozn = obozn;
                    }                    
                    else
                    {
                        item.link_obozn = Obozn_Id;
                    }

                    model.Add(item);
                    db.SaveChanges();
                    ViewData["Id"] = Obozn_Id;
                    ViewData["EditResultKey"] = EditResultKey;

                    if (ControllerName == "SprSpecifController")
                    {
                        var model_prim_dse = db.Spr_prim_dse.Where(x => x.link_specif == Obozn_Id).ToList();
                        return PartialView("_GridView1Partial", model_prim_dse.ToList());
                    }
                    else
                    {
                        var model_prim_dse = db.Spr_prim_dse.Where(x => x.link_obozn == Obozn_Id).ToList();
                        return PartialView("_GridViewAddPartial", model_prim_dse.ToList());
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
        public ActionResult GridView1PartialUpdate(Asu.Web.Models.Spr_prim_dse item, string ID, string ControllerName)
        {
            int.TryParse(ID, out Izd_Id);
            var model = db.Spr_prim_dse;
            if (ModelState.IsValid)
            {
                string rascexItog = Add_Potrebitel(item);

                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        modelItem.rascex = rascexItog;
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
            if (ControllerName == "SprSpecifController")
            {
                ViewData["Id"] = Session["Dse_ID"];
                int obozn = (int)ViewData["Id"];
                var model_prim_dse = db.Spr_prim_dse.Where(x => x.link_specif == obozn).ToList();
                return PartialView("_GridView1Partial", model_prim_dse.ToList());
            }
            else
            {
                ViewData["Id"] = Session["Dse_ID"];
                int obozn = (int)ViewData["Id"];
                var model_prim_dse = db.Spr_prim_dse.Where(x => x.link_obozn == obozn).ToList();
                return PartialView("_GridView1Partial", model_prim_dse.ToList());
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int64 Id, string ControllerName)
        {
            var model = db.Spr_prim_dse;
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
            return PartialView("_GridView1Partial", model.ToList());
        }
    }
}