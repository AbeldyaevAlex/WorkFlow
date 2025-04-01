using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class CommonController : Controller
    {       
    }
    [ValidateInput(false)]
    public partial class EditorsController : Controller
    {
        //public override string Name { get { return "Editors"; } }

        //static EditorsController()
        //{
        //    EmailDataGeneration.Register();
        //    PersonalDataGeneration.Register();
        //}
        //public ActionResult Index()
        //{
        //    return CheckBoxList();
        //}
        public ActionResult ModelValidation()
        {
            return RedirectToAction("ModelValidation", "Common");
        }
    }       
}