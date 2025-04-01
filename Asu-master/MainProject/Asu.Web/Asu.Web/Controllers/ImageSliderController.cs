using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    [AllowAnonymous]
    public class ImageSliderController : Controller
    {
        public ActionResult FullScreen()
        {
            object folder = "~/Content/Slide_Images";
            return View(folder);
        }
    }
}