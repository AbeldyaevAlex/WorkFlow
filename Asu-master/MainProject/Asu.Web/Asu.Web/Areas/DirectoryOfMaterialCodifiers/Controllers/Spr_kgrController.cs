using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers
{
    public class Spr_kgrController : Controller
    {
        // GET: DirectoryOfMaterialCodifiers/Spr_kgr
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridView7Partial()
        {
            var model = new object[0];
            return PartialView("_GridView7Partial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView7PartialAddNew(Asu.Web.Models.Spr_kgr item)
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
            return PartialView("_GridView7Partial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView7PartialUpdate(Asu.Web.Models.Spr_kgr item)
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
            return PartialView("_GridView7Partial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView7PartialDelete(System.Int64 Id)
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
            return PartialView("_GridView7Partial", model);
        }
    }
}