using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Asu.Mapping.Skm
{
    public partial class SkmHelper : ISkmHelper
    {
        private const string FILTER_NAIM_ID_COOKIE_KEY = "WC.FilterNaim.Id.Cookie";
        private const string FILTER_MARKA_ID_COOKIE_KEY = "WC.FilterMarka.Id.Cookie";
        private const string FILTER_GOST_ID_COOKIE_KEY = "WC.FilterGost.Id.Cookie";
        private const string OGT_ID_COOKIE_KEY = "WC.Ogt.Id.Cookie";
        private const string NAIM_SKM_COOKIE_KEY = "WC.Naim.Skm.Cookie";
        private const string MARKA_ID_COOKIE_KEY_AFTER_CHANGE = "WC.Marka.Id.After.Change.Cookie";
        private const string GOST_ID_COOKIE_KEY_AFTER_CHANGE = "WC.Gost.Id.After.Change.Cookie";
        private readonly HttpContextBase httpContext;

        public SkmHelper(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        

        public void SetGostSkmIdToCookies(string listGost)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var GostSkmIdCookie = this.httpContext.Request.Cookies.Get(FILTER_GOST_ID_COOKIE_KEY);
            if (GostSkmIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(FILTER_GOST_ID_COOKIE_KEY);
            }

            GostSkmIdCookie = new HttpCookie(FILTER_GOST_ID_COOKIE_KEY);
            GostSkmIdCookie.Value = string.Format(listGost);
            GostSkmIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(GostSkmIdCookie);
        }

        public void SetMarkaSkmIdToCookies(string listMarkaSkm)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var MarkaIdCookie = this.httpContext.Request.Cookies.Get(FILTER_MARKA_ID_COOKIE_KEY);
            if (MarkaIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(FILTER_MARKA_ID_COOKIE_KEY);
            }

            MarkaIdCookie = new HttpCookie(FILTER_MARKA_ID_COOKIE_KEY);
            MarkaIdCookie.Value = string.Format(listMarkaSkm);
            MarkaIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(MarkaIdCookie);
        }
        public void SetMarkaSkmIdToCookiesAfterChange(string MarkaSkm)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var MarkaCookie = this.httpContext.Request.Cookies.Get(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            if (MarkaCookie != null)
            {
                httpContext.Response.Cookies.Remove(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            }

            MarkaCookie = new HttpCookie(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            MarkaCookie.Value = string.Format(MarkaSkm);
            MarkaCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(MarkaCookie);
        }
        public void SetGostSkmIdToCookiesAfterChange(string Gost)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var GostCookie = this.httpContext.Request.Cookies.Get(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            if (GostCookie != null)
            {
                httpContext.Response.Cookies.Remove(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            }

            GostCookie = new HttpCookie(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            GostCookie.Value = string.Format(Gost);
            GostCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(GostCookie);
        }
        public void SetNaimSkmToCookies(string NmSkm)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var NmSkmCookie = this.httpContext.Request.Cookies.Get(NAIM_SKM_COOKIE_KEY);
            if (NmSkmCookie != null)
            {
                httpContext.Response.Cookies.Remove(NAIM_SKM_COOKIE_KEY);
            }

            NmSkmCookie = new HttpCookie(NAIM_SKM_COOKIE_KEY);
            NmSkmCookie.Value = string.Format(NmSkm);
            NmSkmCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(NmSkmCookie);
        }

        public void SetNmSkmIdToCookies(string listNmSkm)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var NmSkmIdCookie = this.httpContext.Request.Cookies.Get(FILTER_NAIM_ID_COOKIE_KEY);
            if (NmSkmIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(FILTER_NAIM_ID_COOKIE_KEY);
            }

            NmSkmIdCookie = new HttpCookie(FILTER_NAIM_ID_COOKIE_KEY);
            NmSkmIdCookie.Value = string.Format(listNmSkm);
            NmSkmIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(NmSkmIdCookie);
        }

        public void SetOgtIdToCookies(int OgtId)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var OgtIdCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);
            if (OgtIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(OGT_ID_COOKIE_KEY);
            }

            OgtIdCookie = new HttpCookie(OGT_ID_COOKIE_KEY);
            OgtIdCookie.Value = OgtId.ToString();
            OgtIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(OgtIdCookie);
        }

        public void ClearSkmCookies()
        {
            var NmSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_NAIM_ID_COOKIE_KEY);
            if (NmSkmCookie != null)
            {
                NmSkmCookie.Expires = new DateTime(1970, 1, 1);
                NmSkmCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(NmSkmCookie);
            }

            var MarkaSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_MARKA_ID_COOKIE_KEY);
            if (MarkaSkmCookie != null)
            {
                MarkaSkmCookie.Expires = new DateTime(1970, 1, 1);
                MarkaSkmCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(MarkaSkmCookie);
            }

            var GostSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_GOST_ID_COOKIE_KEY);
            if (GostSkmCookie != null)
            {
                GostSkmCookie.Expires = new DateTime(1970, 1, 1);
                GostSkmCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(GostSkmCookie);
            }

            var OgtSkmCookie = this.httpContext.Request.Cookies.Get(OGT_ID_COOKIE_KEY);
            if (OgtSkmCookie != null)
            {
                OgtSkmCookie.Expires = new DateTime(1970, 1, 1);
                OgtSkmCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(OgtSkmCookie);
            }

            var NaimSkmCookie = this.httpContext.Request.Cookies.Get(NAIM_SKM_COOKIE_KEY);
            if (NaimSkmCookie != null)
            {
                NaimSkmCookie.Expires = new DateTime(1970, 1, 1);
                NaimSkmCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(NaimSkmCookie);
            }
            var MarkaSkmCookieAfterChange = this.httpContext.Request.Cookies.Get(MARKA_ID_COOKIE_KEY_AFTER_CHANGE);
            if (MarkaSkmCookieAfterChange != null)
            {
                MarkaSkmCookieAfterChange.Expires = new DateTime(1970, 1, 1);
                MarkaSkmCookieAfterChange.Value = string.Empty;
                httpContext.Response.Cookies.Add(MarkaSkmCookieAfterChange);
            }
            var GostSkmCookieAfterChange = this.httpContext.Request.Cookies.Get(GOST_ID_COOKIE_KEY_AFTER_CHANGE);
            if (GostSkmCookieAfterChange != null)
            {
                GostSkmCookieAfterChange.Expires = new DateTime(1970, 1, 1);
                GostSkmCookieAfterChange.Value = string.Empty;
                httpContext.Response.Cookies.Add(GostSkmCookieAfterChange);
            }           
        }

        public void RemoveNmSkmToCookies()
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }
            var NmSkmCookie = this.httpContext.Request.Cookies.Get(FILTER_NAIM_ID_COOKIE_KEY);
            if (NmSkmCookie != null)
            {
                httpContext.Response.Cookies.Remove(FILTER_NAIM_ID_COOKIE_KEY);
                NmSkmCookie = new HttpCookie(FILTER_NAIM_ID_COOKIE_KEY);
                NmSkmCookie.Value = "/";
                NmSkmCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
                this.httpContext.Response.Cookies.Add(NmSkmCookie);
            }
        }
    }
}
