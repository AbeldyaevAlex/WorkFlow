using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using EFDBFirst;

namespace Asu.Web.Controllers
{
    public class DirectiveWorksController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}