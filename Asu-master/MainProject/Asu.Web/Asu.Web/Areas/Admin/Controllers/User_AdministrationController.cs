using Asu.Web.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.Admin.Controllers
{
    public class User_AdministrationController : Controller
    {
        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();
        // GET: Admin/User_Administration
        public ActionResult Index()
        {
            var model = db.User;
            var model1 = GetUser();
            return View(model1);
        }
        public static IEnumerable<User> GetUser()
        {
            IEnumerable<User> user = null;
            using (ASU_AVIAEntities12 db = new ASU_AVIAEntities12())
            {
                user = (from us in db.User
                        select us).ToList();
                return user;
            }
        }

        [ValidateInput(false)]
        public ActionResult CardView1Partial()
        {
            var model = db.User;
            var model1 = GetUser();
            return PartialView("_CardView1Partial", model1);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CardView1PartialAddNew(Asu.Web.Models.User item)
        {
            var model = db.User;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_CardView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CardView1PartialUpdate(Asu.Web.Models.User item)
        {
            var model = db.User;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
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
            return PartialView("_CardView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CardView1PartialDelete(System.Int32 Id)
        {
            var model = db.User;
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
            return PartialView("_CardView1Partial", model.ToList());
        }
    }
}