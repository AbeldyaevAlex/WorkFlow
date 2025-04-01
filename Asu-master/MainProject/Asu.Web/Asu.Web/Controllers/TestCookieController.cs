using Asu.Mapping.Malahit;
using Asu.Services.Vehicles;
using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class TestCookieController : Controller
    {
        private const string MALAHIT_ID_COOKIE_KEY = "WC.Malahit.Id.Cookie";

        private readonly HttpContextBase httpContext;
        private readonly IMalahitHelpers _malahitHelper;

        public TestCookieController(IMalahitHelpers malahitHelper, HttpContextBase httpContext)
        {
            _malahitHelper = malahitHelper;
            this.httpContext = httpContext;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetMSZ(WorkShopMemorandumBase item)
        {
            if (String.IsNullOrEmpty(item.start_date.ToString()) || String.IsNullOrWhiteSpace(item.start_date.ToString()))
            {
                item.start_date = DateTime.Parse("2015-01-01");
            }
            if (String.IsNullOrEmpty(item.end_date.ToString()) || String.IsNullOrWhiteSpace(item.end_date.ToString()) || item.end_date == DateTime.Parse("1/1/0001"))
            {
                item.end_date = DateTime.Now;
            }
            else
            {
                item.end_date = item.end_date.AddDays(1);
            }

            _malahitHelper.SetMeorandumIdToCookies(item.zakaz, item.start_date, item.end_date);

            var malahitCookie = GetMeorandumFromCookies();

            bool a = _malahitHelper.GetMeorandumFromCookies(item.zakaz, item.start_date, item.end_date);

            return View("~/Areas/Malahit/Views/MainMalahit/KendoGridMSZ.cshtml");
        }
        protected virtual HttpCookie GetMeorandumFromCookies()
        {
            if (httpContext == null || httpContext.Request == null)
                return null;

            return httpContext.Request.Cookies[MALAHIT_ID_COOKIE_KEY];
        }
    }
}