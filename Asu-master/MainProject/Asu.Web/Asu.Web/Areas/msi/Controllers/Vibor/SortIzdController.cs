using Asu.Web.Areas.msi.Controllers.Vibor;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class SortIzdController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
    public partial class EditorsController : Controller
    {
        [HttpGet]
        public ActionResult ListBox()
        {
            ListBoxDemoHelper.LoadXmlDocument(Server.MapPath("//App_Data//PhoneModel.xml"));
            ListBoxDemoHelper.ResetFiltration();
            ListBoxDemoHelper.SelectionMode = ListEditSelectionMode.CheckColumn;
            ListBoxDemoHelper.EnableSelectAll = true;
            return View("ListBox", ListBoxDemoHelper.GetFeatures());
        }
        [HttpPost]
        public ActionResult ListBox(ListEditSelectionMode selectionMode, bool enableSelectAll = false)
        {
            ListBoxDemoHelper.ResetFiltration();
            ListBoxDemoHelper.SelectionMode = selectionMode;
            ListBoxDemoHelper.EnableSelectAll = enableSelectAll;
            return View("ListBox", ListBoxDemoHelper.GetFeatures());
        } 
        public ActionResult ListBoxPartial(string selectedFeatures)
        {
            if (!string.IsNullOrEmpty(selectedFeatures))
            {
                ListBoxDemoHelper.FilterModels(selectedFeatures.Split(','));
            }
            else
            {
                ListBoxDemoHelper.ResetFiltration();
            }
            return PartialView("ListBoxPartial", ListBoxDemoHelper.GetModels());
        }
    }
}