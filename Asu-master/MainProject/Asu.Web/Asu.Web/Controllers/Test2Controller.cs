using DevExpress.Web.Mvc;
using DevExpress.Export.Xl;
using Asu.Web.Data;
using Asu.Web.Models;
using Asu.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ClosedXML.Excel;
using System.Data.SqlClient;
using DataTable = System.Data.DataTable;
using System.Threading.Tasks;
using System.Data.Entity;
using MSI = Asu.Web.Models.Msi;
using DevExpress.Web;
using System.Diagnostics;
using DevExpress.XtraEditors.Controls;
//using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;

namespace Asu.Web.Controllers
{

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
    public static class Role
    {
        public const string SA = "SuperAdministrator";
        public const string Administrator = "Administrator";
        public const string Assistant = "Assistant";


        public const string AdministratorOrUser = Administrator + "," + Assistant;

    }
    public class FL
    {
        public decimal teg { get; set; }
    }
    public class Event
    {
        public string first { get; set; }
        public string second { get; set; }
        public string itog { get; set; }
    }
    public class Test2Controller : Controller
    {
        public ActionResult TestExcel()
        {
            return View();
        }
        public FileResult SaveEndOfBase()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Лист1");

            //создадим заголовки у столбцов
            worksheet.Cell("A" + 1).Value = "Имя";
            worksheet.Cell("B" + 1).Value = "Фамиля";
            worksheet.Cell("C" + 1).Value = "Возраст";

            // 

            worksheet.Cell("A" + 2).Value = "Иван";
            worksheet.Cell("B" + 2).Value = "Иванов";
            worksheet.Cell("C" + 2).Value = 18;
            //пример изменения стиля ячейки
            worksheet.Cell("B" + 2).Style.Fill.BackgroundColor = XLColor.Red;

            // пример создания сетки в диапазоне
            var rngTable = worksheet.Range("A1:G" + 10);
            rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            worksheet.Columns().AdjustToContents(); //ширина столбца по содержимому

            // вернем пользователю файл без сохранения его на сервере
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Base.xlsx");
            }
        }
        public  void TestDb()
        {
            
                var cex = new MSI.Spr_cex()
                {
                    cex = "test"
                };
               
            
        }
        [AuthorizeRoles(Role.AdministratorOrUser, Role.Administrator)]

       
        public ActionResult TestCustomHelper()
        {
            return View();
        }
        const GridHeaderFilterMode DefaultHeaderFilterMode = GridHeaderFilterMode.DateRangePicker;
        public ActionResult DateRangeHeaderFilter()
        {
            return View("DateRangeHeaderFilter", DefaultHeaderFilterMode);
        }
        public ActionResult DateRangeHeaderFilterPartial(GridHeaderFilterMode headerFilterMode = DefaultHeaderFilterMode)
        {
            ViewBag.HeaderFilterMode = headerFilterMode;
            return PartialView("DateRangeHeaderFilterPartial", db.Spr_skm);
        }
        public ActionResult GetViewModel()
        {
            var obozn = db.Spr_obozn.ToList();
            var mater = db.Spr_mater.ToList();
            var model = new TestViewModel { Spr_Obozns = obozn, Spr_Maters = mater };
            return View(model);
        }
        public ActionResult Recurs()
        {
            int[] array = new int[4] { 10, 30, 50, 70};
            ReversArray(array, array.Length - 1);
            return View();
        }
        static void ReversArray(int[] array, int i)
        {
            if (i < 0)
            {
                return;
            }
           int prom_it = array[i];                    
           ReversArray(array, i - 1);            
        }


        public ActionResult GetMainAssemblies()
        {
            SqlCommand cmd = new SqlCommand();
            string query = "GetMainAssemblies";
            cmd.Parameters.AddWithValue("@ser_ss", 314);
            cmd.Parameters.AddWithValue("@ser_spo", 314);
            cmd.Parameters.AddWithValue("@izdelie", 4);
            cmd.CommandText = query;
            cmd.CommandType = CommandType.StoredProcedure;
            //List<SelectListItem> statesList = GetItems(cmd);

            return View();
        }

        public ActionResult FileExcelPhone()
        {
            var i = Process.Start(Path.Combine(@"\\forward\Телефонный справочник\Телефонный справочник.xls"));
            return View();
        }

        //private List<SelectListItem> GetItems(SqlCommand cmd)
        //{
        //    List<MainAssembliesModel> listMainAssemblies = new List<MainAssembliesModel>();
        //    string connetionString = ConfigurationManager.ConnectionStrings["AsuAviaContext"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(connetionString))
        //    {
        //        cmd.Connection = conn;
        //        conn.Open();
        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while (sdr.Read())
        //            {
        //                listItems.Add(new SelectListItem { Text = sdr[1].ToString(), Value = sdr[0].ToString() });
        //            }
        //        }
        //        conn.Close();
        //    }
        //    return listMainAssemblies;
        //}




        public ActionResult GetSplitter()
        {
            return View();
        }
        public ActionResult FloatingActionButtonForGridView()
        {
            return View();
        }


        public const string EditResultKey = "EditResult";
        public const string EditErrorKey = "EditError";
        public ActionResult Export()
        {
            return Content("");
        }
        [HttpPost]
        public ActionResult Export(FormCollection formcollection)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            try
            {
                SqlConnection con = new SqlConnection(@"data source = i7-860; initial catalog = ASU_AVIA; user id = k6; password = jnltk35");
                cmd = new SqlCommand("GetPKP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = cmd;
                sqlDataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= REPORT_EXCEL.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public ActionResult ExportToXL()
        {

           
            IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);
            //using (FileStream stream = new FileStream($"\\\\{Ip}\\D$\\Load\\Document_2.xlsx", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (MemoryStream stream = new MemoryStream())
            {
                using (IXlDocument document = exporter.CreateDocument(stream))
                {
                    using (IXlSheet sheet = document.CreateSheet())
                    {
                        sheet.Name = "Report";
                        using (IXlColumn column = sheet.CreateColumn())
                        {
                            column.WidthInPixels = 100;
                        }
                        using (IXlColumn column = sheet.CreateColumn())
                        {
                            column.WidthInPixels = 200;
                        }
                        using (IXlColumn column = sheet.CreateColumn())
                        {
                            column.WidthInPixels = 100;
                            column.Formatting = new XlCellFormatting();
                            column.Formatting.NumberFormat = @"_([$$-409]* #,##0.00_);_([$$-409]*\(#,##0.00\);_([$$-409]* ""-""??_(@_)";
                        }
                        XlCellFormatting cellFormating = new XlCellFormatting();
                        cellFormating.Font = new XlFont();
                        cellFormating.Font.Name = "Century Gothic";
                        cellFormating.Font.SchemeStyle = XlFontSchemeStyles.None;

                        XlCellFormatting header = new XlCellFormatting();
                        header.CopyFrom(cellFormating);
                        header.Font.Bold = true;
                        header.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0);
                        header.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0));
                        header.Border = XlBorder.AllBorders(XlColor.Auto);

                        using (IXlRow row = sheet.CreateRow())
                        {
                            using (IXlCell cell = row.CreateCell())
                            {
                                cell.Value = "Product";
                                cell.ApplyFormatting(header);
                            }
                            using (IXlCell cell = row.CreateCell())
                            {
                                cell.Value = "Region";
                                cell.ApplyFormatting(header);
                            }
                            using (IXlCell cell = row.CreateCell())
                            {
                                cell.Value = "Sales";
                                cell.ApplyFormatting(header);
                            }
                        }
                        List<string> List = new List<string>();
                        List.Add("One");
                        List.Add("Two");
                        List.Add("Three");
                        foreach (var item in List)
                        {
                            using (IXlRow row = sheet.CreateRow())
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = item;
                                }
                            }
                        }
                        sheet.AutoFilterRange = sheet.DataRange;
                    }                  
                }               
            }
            System.Diagnostics.Process.Start("Document.xlsx");
            return View();
        }
        //public static void SendFile()
        //{
        //    var remoteIpAddress = IPAddress.Parse("10.48.7.164".ToString());
        //    var endPoint = new IPEndPoint(remoteIpAddress, 8888);
        //    fs = new FileStream("Document.xlsx", FileMode.Open, FileAccess.Read);
        //    Thread.Sleep(2000);
        //    SendFiles();
        //}
        private static void SendFiles()
        {
            //byte[] bytes = new byte[fs.Length];
            //fs.Read(bytes, 0, bytes.Length);

        }
        [HttpGet]
        public ActionResult Trep()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Trep(FL item)
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetEvent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetEvent(string first, string second, string itog)
        {
            return View();
        }
        private ASU_AVIAEntities12 db;
        //private static FileStream fs;

        public Test2Controller()
        {
            db = new ASU_AVIAEntities12();
        }
        public ActionResult Index()
        {
            ViewBag.Cex = new SelectList(db.Spr_cex, "Id", "cex");
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult TabPage1()
        {
            var model = DateTime.Now;
            return PartialView("_PartialView1", model);
        }
        public ActionResult TabPage2()
        {
            var model = DateTime.Now;
            return PartialView("_PartialView2", model);
        }
        public ActionResult TabPage3()
        {
            var model = DateTime.Now;
            return PartialView("_PartialView3", model);
        }
        public ActionResult PageControl()
        {
            var model = DateTime.Now;
            return PartialView();
        }
        public ActionResult GridViewPartial()
        {
            var model = db.Spr_tto.ToList();
            return PartialView("PartialView2", model);
        }

        public ActionResult GetUsers()
        {
            return View();
        }

        public ActionResult ChosenDropDown()
        {
            test obj = new test();
            obj.GetTestList = db.Spr_tem.Select(s => new test { Id = (int)s.Id, Name = s.nm_tem_p }).ToList();
            return View(obj);
        }
        [HttpPost]
        public ActionResult ChosenDropDown(test obj)
        {
            return RedirectToAction("ChosenDropDown");
        }
        public ActionResult MultiSelectWithDropDownList()
        {
            //IEnumerable<Test2ViewModel> list_Tem = (from obj in db.Spr_tem
            //                                        select new Test2ViewModel()
            //                                        {
            //                                            Id = (int)obj.Id,
            //                                            Name = obj.nm_tem_p
            //                                        }).ToList();
            return View();

            //return View(list_Tem);
        }
        //    public ActionResult Index()
        //    {
        //        string f = "Абельдяев";

        //        XtraReport1 cs = new XtraReport1();

        //        var list = db.User.Where(x => x.Last_Name == f).ToList();
        //        var s = cs.ShowPreviewMarginLines = true;
        //        cs.Report.DataSource = list;
        //        cs.ExportToXls("C:\\Excel\\test2.xls");
        //        return View();
        //    }
        //    public ActionResult Slide_Image()
        //    {
        //        return View();
        //    }
        //    public ActionResult Information()
        //    {
        //        DataTable table = new OleDbEnumerator().GetElements();
        //        string inf = "";
        //        foreach (DataRow row in table.Rows)
        //        {
        //            inf += row["SOURCES_NAME"] + " ";
        //        }
        //        return View(inf.ToList());
        //    }
        //    public ActionResult Index_2()
        //    {
        //        return View();
        //    }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialGetUsersAddNew(UserViewModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialGetUsers", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialGetUsersUpdate(UserViewModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialGetUsers", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialGetUsersDelete(int Id)
        {
            var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialGetUsers", model);
        }

        public ActionResult GetObozn()
        
        {
            return View();
        }

        ASU_AVIAEntities12 db1 = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        //public async Task<ActionResult> GetOboznGridViewPartial()
        //{
        //    var model = await db1.Spr_obozn.ToListAsync();
        //    return PartialView("_GetOboznGridViewPartial", model);
        //}


        public ActionResult GetOboznGridViewPartial()
        {
            var model = db1.Spr_obozn;
            return PartialView("_GetOboznGridViewPartial", model);
        }
        public ActionResult CustomGridViewEditingPartial(int key)
        {
            var model = db1.Spr_obozn;
            ViewData["key"] = key;
            return PartialView("_GetOboznGridViewPartial", model);
        }
        public static  List<Spr_obozn> GetRowValuesByKeyValue(int key)
        {
            ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
            var array_obozn =  db.Spr_obozn.Where(x => x.Id == key).ToList();
            return array_obozn;
        }        
        [HttpPost, ValidateInput(false)]
        public ActionResult GetOboznGridViewPartialAddNew(Spr_obozn item)
        {
            var model = db1.Spr_obozn;
            if (ModelState.IsValid)
            {
                try
                {
                    var any_Obozn = db.Spr_obozn.Where(x => x.obozn.Trim() == item.obozn.Trim() && x.var == item.var).ToList();
                    if (any_Obozn.Count > 0)
                    {
                        ViewData["EditError"] = $"Обозначение - {item.obozn} {item.var} уже существует.";
                    }
                    else
                    {
                        model.Add(item);
                        db1.SaveChanges();
                        ViewData[EditResultKey] = string.Format("Добавлена запись с обозначением: '{0}{1}'", item.obozn, item.var);
                    }                   
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GetOboznGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GetOboznGridViewPartialUpdate(Spr_obozn item)
        {
            var model = db1.Spr_obozn;
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
            return PartialView("_GetOboznGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GetOboznGridViewPartialDelete(System.Int64 Id)
        {
            var model = db1.Spr_obozn;
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
            return PartialView("_GetOboznGridViewPartial", model);
        }

        ASU_AVIAEntities12 db2 = new ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult FloatingActionButtonForGridViewPagePartial()
        {
            var model = db2.Spr_obozn;
            return PartialView("~/Views/Test2/_FloatingActionButtonForGridViewPagePartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FloatingActionButtonForGridViewPagePartialAddNew(Asu.Web.Models.User item)
        {
            var model = db2.User;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Views/Test2/_FloatingActionButtonForGridViewPagePartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FloatingActionButtonForGridViewPagePartialUpdate(Asu.Web.Models.User item)
        {
            var model = db2.User;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db2.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Views/Test2/_FloatingActionButtonForGridViewPagePartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FloatingActionButtonForGridViewPagePartialDelete(System.Int32 Id)
        {
            var model = db2.User;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Views/Test2/_FloatingActionButtonForGridViewPagePartial.cshtml", model.ToList());
        }
    }
    public class XLExport
    {

    }
}