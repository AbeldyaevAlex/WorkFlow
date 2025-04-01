using System.Web.Mvc;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;





namespace Asu.Web.Controllers
{
    public class PhoneDirectoryController : Controller
    {
        public ActionResult GetPhoneDirectory()
        {
            //var process1 = new System.Diagnostics.Process();

            //process1.StartInfo.WorkingDirectory = Request.MapPath("../App_Data/Excel/Телефонный справочник.xlsx");
            //process1.StartInfo.FileName = Request.MapPath("../App_Data/Excel/Телефонный справочник.xlsx");


            //process1.StartInfo.LoadUserProfile = true;

            //process1.Start();

            
            //process1.Close();

            //ProcessStartInfo psi = new ProcessStartInfo();
            //psi.UseShellExecute = true;
            //psi.LoadUserProfile = true;
            //psi.WorkingDirectory = Server.MapPath("../");// This line solved my problem
            //psi.FileName = Server.MapPath("../App_Data/Excel/Телефонный справочник.xlsx");
            //psi.Arguments = "Myargument1 Myargument2";
            //Process.Start(psi);



            //Process p = new Process();
            //ProcessStartInfo ps = new ProcessStartInfo();
            //ps.FileName = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel"), "Телефонный справочник.xlsx");
            //p.StartInfo = ps;
            //p.Start();
            return View();
        }
        //public FileResult GetPhoneDirectoryResult()
        //{
        //    //Workbook workbook = new Workbook(Path.Combine(@"\\diskstation\Телефонный справочник\Телефонный справочник.xls"));

        //    //workbook.Save(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel"), "Телефонный справочник.xlsx"), SaveFormat.Xlsx);

        //    ////Process.Start(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel"), "Телефонный справочник.xlsx"));

        //    //string reportUrl = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel"), "Телефонный справочник.xlsx");


        //    //byte[] Files = System.IO.File.ReadAllBytes(reportUrl);

        //    //return File(Files, "application/vnd.ms-excel");
        //    ////return File(Files, "application/pdf");
        //}
    }
}
