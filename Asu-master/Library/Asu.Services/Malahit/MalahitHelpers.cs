using Asu.Core.Domain.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Asu.Mapping.Malahit
{
    public partial class MalahitHelpers : IMalahitHelpers
    {
        private const string MALAHIT_ID_COOKIE_KEY = "WC.Malahit.Id.Cookie";
        private const string MALAHIT_ID_COOKIE_PATTERN = "{0}|{1}|{2}";

        private readonly HttpContextBase httpContext;

        public MalahitHelpers(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }
        public void SetMeorandumIdToCookies(string zakaz, DateTime startDate, DateTime endDate)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var malahitIdCookie = this.httpContext.Request.Cookies.Get(MALAHIT_ID_COOKIE_KEY);
            if (malahitIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(MALAHIT_ID_COOKIE_KEY);
            }

            malahitIdCookie = new HttpCookie(MALAHIT_ID_COOKIE_KEY);
            malahitIdCookie.Value = string.Format(MALAHIT_ID_COOKIE_PATTERN, zakaz, startDate, endDate);
            malahitIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(malahitIdCookie);
        }

        public bool GetMeorandumFromCookies(string zakaz, DateTime startDate, DateTime endDate)
        {

            var malahitCookie = this.httpContext.Request.Cookies.Get(MALAHIT_ID_COOKIE_KEY);
            if (malahitCookie == null || string.IsNullOrEmpty(malahitCookie.Value))
            {
                return false;
            }

            var vehicleParts = malahitCookie.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (vehicleParts.Length != 3)
            {
                return false;
            }
            return true;
        }
    }
}
