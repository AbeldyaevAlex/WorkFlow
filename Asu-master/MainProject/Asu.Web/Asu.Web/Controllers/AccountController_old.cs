using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Asu.Web.Model;
using System.Threading.Tasks;
using System.Web.Security;
using Asu.Web.Models;
using Microsoft.Owin;

namespace Asu.Web.Controllers_old
{
    //public class AccountController : BaseController
    //{
    //    ASU_AVIAEntities12 bb = new ASU_AVIAEntities12();
    //    [AllowAnonymous]
    //    public ActionResult SignIn(string returnUrl)
    //    {
    //        ViewBag.ReturnUrl = returnUrl;
    //        return View(new User());
    //    }
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult SignIn(User model, string returnUrl)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return View(model);
    //        }
    //        var result = bb.User.FirstOrDefault(a => a.Login == model.Login && a.Password == model.Password);
            
    //        if (result != null)
    //        {
    //            ViewBag.mode = model;
    //            User user = new User();
    //            user.Id = result.Id;
    //            Session["UserId"] = user.Id;
    //            Session["FullMane"] = result.First_Name + " " + result.Last_Name;
    //            Session["globalId"] = (long?)0;

    //            Session["User"] = result;

    //            FormsAuthentication.SetAuthCookie(model.Login, true);
               
    //            return RedirectToAction("../Home/Index"); //RedirectToAction("SignIn", "Account", "Default")
    //        }
    //        else
    //        {               
    //            ViewBag.GeneralError = "Ошибка! Проверьте логин или пароль ...";
    //        }
    //        return View(model);
    //    }






    //    //[HttpPost]
    //    //[ValidateAntiForgeryToken]
    //    public ActionResult SignOut()
    //    {
    //        {
    //            Session["LoginID"] = 0;
    //            FormsAuthentication.SignOut();
    //            return RedirectToAction("SignIn", "Account", "Default");
    //        }
    //    }
    //      public ActionResult UserMenuItemPartial()
    //    {
    //        return PartialView("UserMenuItemPartial", AuthHelper.GetLoggedInUserInfo());
    //    }
    //}
}