using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class ImportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string myExcelData)
        {
            if (true)
            {
                string filePath = "D:\\ISP_TANTK_v3\\Asu.Web\\Areas\\Upload\\";
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

                filePath = filePath + fileName + ".xlsx";

                //myExcelData.SaveAs(filePath);
                XLWorkbook xlworkbook = new XLWorkbook(filePath);
                int row = 2;

                while (xlworkbook.Worksheets.Worksheet(1).Cell(row, 1).GetString() != "")
                {

                    //db.SaveChanges();
                    row++;
                }

            }
            else
            {

            }

            return View();
        }
    }
}