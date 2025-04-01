using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Asu.Web.Areas.msi.Controllers.Vibor
{
    public class ListBoxHelperController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
    public static class ListBoxDemoHelper
    {
        static XmlDataSource data = new XmlDataSource();
        static string modelsXpath;
        public static ListEditSelectionMode SelectionMode { get; set; }
        public static bool EnableSelectAll { get; set; }
        public static XmlDataSource GetFeatures()
        {
            return GetData("//Features/*");
        }
        public static XmlDataSource GetModels()
        {
            return GetData(modelsXpath);
        }
        public static void LoadXmlDocument(string fileName)
        {
            data.DataFile = fileName;
        }
        public static void ResetFiltration()
        {
            modelsXpath = "//Model";
        }
        public static void FilterModels(string[] selectedFeatures)
        {
            ResetFiltration();
            StringBuilder sb = new StringBuilder(modelsXpath);
            for (int i = 0; i < selectedFeatures.Length; i++)
            {
                sb.Append(i == 0 ? "[" : " and ");
                sb.AppendFormat("@{0} = \"true\"", selectedFeatures[i]);
            }
            modelsXpath = sb.Append("]").ToString();
        }
        static XmlDataSource GetData(string xPath)
        {
            data.XPath = xPath;
            return data;
        }
    }
}