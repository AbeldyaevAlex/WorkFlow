using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.Admin.Controllers
{

    public class ImageLoadController : Controller
    {
        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
        //public ActionResult Index()
        //{
        //    return View(db.Image);
        //}
        public ActionResult Create()
        {
            //ViewBag.link_tasks = new SelectList(db.Tasks, "Id", "link_nm_task");
            //ViewBag.link_tasks = new SelectList(db.Spr_nm_task, "Id", "Naim_task");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,screen,name,link_tasks,UrlAction,Alt,Controller_Name,Action_Name,IsGroup,RouteUrl,Title")] Spr_nm_task img, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                using (var binayReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binayReader.ReadBytes(uploadImage.ContentLength);
                }
                img.screen = imageData;
                db.Spr_nm_task.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", "Admin_default");
            }
            return View();
        }
    }
}