using DevExpress.Web.Mvc;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections;
using Asu.Mapping.Skm;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using Asu.Web.Models;
using Asu.Core.Domain.Customers;
using Asu.Framework.UI.Captcha;
using Asu.Web.Models.Customer;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Asu.Services.SprPkp;
using ClosedXML.Excel;
using System.IO;
using DevExpress.CodeParser;

namespace Asu.Web.Controllers
{
    public class FileUpload
    {
        public List<HttpPostedFileBase> Files { get; set; }
        public string Name { get; set; }
    }
    public class Util
    {
        public static async Task<List<Models.Msi.Spr_cex>> GetStudents()
        {
            List<Models.Msi.Spr_cex> students = new List<Models.Msi.Spr_cex>();
            Models.Msi.Spr_cex s;
            DataTable dt = new DataTable();



            string connetionString = ConfigurationManager.ConnectionStrings["AsuAviaContext"].ConnectionString;
            SqlConnection connection = new SqlConnection(connetionString);

            SqlDataAdapter da = new SqlDataAdapter("select * from Spr_cex", connection);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                s = new Models.Msi.Spr_cex();
                s.Id = Convert.ToInt32(row["Id"]);
                s.cex = row["cex"] as string;
                students.Add(s);
            }
            return await Task.FromResult(students);
        }
    }

    public class Test4Controller : Controller
    {
        private readonly IDirectoryOfMaterialCodifiersService _directoryOfMaterialCodifiersService;
        private readonly IRepository<Spr_pkp> _pkpRepository;
        private readonly CustomerSettings _customerSettings;
        private readonly ISprPkpService _sprPkpService;

        public Test4Controller(IDirectoryOfMaterialCodifiersService directoryOfMaterialCodifiersService, IRepository<Spr_pkp> pkpRepository, CustomerSettings customerSettings, ISprPkpService sprPkpService)
        {
            _directoryOfMaterialCodifiersService = directoryOfMaterialCodifiersService;
            _pkpRepository = pkpRepository;
            _customerSettings = customerSettings;
            _sprPkpService = sprPkpService;
        }
        public ActionResult Export()
        {
            var Listpkp = _sprPkpService.GetAllPkp();
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.CalculateMode = XLCalculateMode.Auto;
                var worksheet = workbook.Worksheets.Add("Справчник ПКП");
                //worksheet.Cell("A1").Value = "Id";
                //worksheet.Cell("B1").Value = "Pkp";
                //worksheet.Cell("C1").Value = "NmPkp";
                

                worksheet.Row(1).Height = 30;
                worksheet.SheetView.FreezeRows(2);

                worksheet.SetTabSelected(true);
                worksheet.SetTabColor(XLColor.Almond);

                worksheet.Cells("A1").Value = "Справчник ПКП";
                worksheet.Cells("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cells("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Range("A1:Q1").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Range("A1:Q1").Style.Font.Bold = true;
                worksheet.Range("A1:Q1").Merge();

                worksheet.Cell("A3").InsertData(Listpkp);
                worksheet.Column(1).Width = 25;
                worksheet.Column(2).Width = 50;
                worksheet.Column(2).Style.Alignment.WrapText = true;

                //worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Range("A1:Q1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range("A1:Q1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Range("A2:Q2").AsRange().SetAutoFilter(true);

                worksheet.Range($"A2:Q{Listpkp.Count + 2}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range($"A2:Q{Listpkp.Count + 2}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range($"A2:Q{Listpkp.Count + 2}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range($"A2:Q{Listpkp.Count + 2}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range($"A2:Q{Listpkp.Count + 2}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range($"A2:Q{Listpkp.Count + 2}").Style.Border.TopBorder = XLBorderStyleValues.Thin;

                //for (int i = 0; i < Listpkp.Count; i++)
                //{
                //    worksheet.Cell(i + 2, 1).Value = Listpkp[i].Id;
                //    worksheet.Cell(i + 2, 2).Value = Listpkp[i].Pkp;
                //    worksheet.Cell(i + 2, 3).Value = Listpkp[i].NmPkp;
                //}
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Справчник ПКП на {DateTime.Now.ToString("dd.MM.yyyy")} (ОИСП).xlsx"
                    };
                }
            }
        }
        public async Task<ActionResult> FileUpload()

        {
            Queue qt = new Queue();


            List<Models.Msi.Spr_cex> students = await Util.GetStudents();
            return View(students);
        }
        public ActionResult fileSave(FileUpload fileupload)
        {
            try
            {
                if (fileupload.Files.Count() > 0)
                {
                    foreach (var file in fileupload.Files)
                    {
                        string filename = file.FileName;
                        var ext = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
                        if (ext.ToLower() == "jpeg" || ext.ToLower() == "jpg" || ext.ToLower() == "png")
                        {
                            string path = Server.MapPath("~/Content/" + filename);
                            file.SaveAs(path);
                            // Todo: for database 
                            string uploadedBy = fileupload.Name;
                            string FilePath = path;
                            //Save fields to database
                            //
                        }
                    }
                    return Json("data saved");
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("something went wrong");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View(_directoryOfMaterialCodifiersService.GetAllKm(null).AsQueryable());
        }
        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = DateTime.Now.AddDays(i),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            return Json(result.ToDataSourceResult(request));
        }
        [HttpGet]
        public ActionResult Index3()
        {
            //var table = _pkpRepository.Table.ToList();
            //return View(table);
            var model = new LoginModel();
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.CheckoutAsGuest = false;
            return View();
        }
        [HttpPost]
        public ActionResult Index3(WorkShopMemorandumBase selectedIDsHF)
        {
            //Get all selected keys from hidden input
            var table = _pkpRepository.Table.Where(x => x.Id == 2).ToList();
            //string _selectedIDs = selectedIDsHF;
            return View(table);
        }
        public ActionResult GridViewEditingPartial()
        {
            ////Get all selected keys from e.customArgs on GridView callback
            string _selectedIDs = Request.Params["selectedIDs"];
            ViewData["_selectedIDs"] = _selectedIDs;
            var table = _pkpRepository.Table.ToList();
            return PartialView(table);
        }
        public ActionResult UploadControlCallbackAction()
        {
            UploadControlExtension.GetUploadedFiles("uc", UploadControlDemosHelper.ValidationSettings, UploadControlDemosHelper.uc_FileUploadComplete);
            return null;
        }
        public ActionResult TestTabPanel()
        {
            return View();
        }
    }
    public class UploadControlDemosHelper
    {
        public const string UploadDirectory = "~/Content/UploadControl/UploadFolder/";

        public static readonly UploadControlValidationSettings ValidationSettings = new UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", },
            MaxFileSize = 20971520,
        };

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + e.UploadedFile.FileName);
                //e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath);
                }
            }
        }
    }
}
