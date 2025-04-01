using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models.ContextDb;
using Asu.Web.Models.Msi;
using Asu.Web.Areas.msi.Controllers;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Asu.Web.Controllers
{
    public class Test3Controller : Controller
    {
        AsuAviaDbContext db = new AsuAviaDbContext();
        public ActionResult TestDb()
        {
            SqlCommand cmd = new SqlCommand();
            string query = "GetTem";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.StoredProcedure;
            List<SelectListItem> CountryList = GetItems(cmd);
            ViewBag.CountryList = CountryList;
            return View();
        }
        [HttpPost]
        public ActionResult TestDb(FormCollection formCollection)
        {
            string selectedCountries = formCollection["Country"];
            string selectedStates = formCollection["State"];
            string selectedCities = formCollection["City"];
            // Code to insert selected values into database.
            return new EmptyResult();
        }
        public JsonResult GetStates(string id)
        {
            SqlCommand cmd = new SqlCommand();
            string query = "GetIzd";
            cmd.Parameters.AddWithValue("@TemIds", id);
            cmd.CommandText = query;
            cmd.CommandType = CommandType.StoredProcedure;
            List<SelectListItem> statesList = GetItems(cmd);

            return Json(statesList, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> GetItems(SqlCommand cmd)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            string connetionString = "data source = i7-860; initial catalog = TestDB_AsuAvia; user id = k6; password = jnltk35";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
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

        public ActionResult GetTask()
        {
            return View();
        }

        Asu.Web.Models.ContextDb.AsuAviaDbContext db1 = new Asu.Web.Models.ContextDb.AsuAviaDbContext();

        [ValidateInput(false)]
        public ActionResult TreeListPartial()
        {



            var model = db1.Spr_nm_task;
            return PartialView("~/Views/Test3/_TreeListPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialAddNew(Asu.Web.Models.UsersTask.Spr_nm_task item)
        {
            var model = db1.Spr_nm_task;
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
            return PartialView("~/Views/Test3/_TreeListPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialUpdate(Asu.Web.Models.UsersTask.Spr_nm_task item)
        {
            var model = db1.Spr_nm_task;
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
            return PartialView("~/Views/Test3/_TreeListPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialDelete(System.Int32 Id)
        {
            var model = db1.Spr_nm_task;
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
            return PartialView("~/Views/Test3/_TreeListPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialMove(System.Int32 Id, System.Int32? Id_Roditel)
        {
            var model = db1.Spr_nm_task;
            try
            {
                var item = model.FirstOrDefault(it => it.Id == Id);
                if (item != null)
                    item.Id_Roditel = Id_Roditel;
                db1.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("~/Views/Test3/_TreeListPartial.cshtml", model.ToList());
        }
    }
}




