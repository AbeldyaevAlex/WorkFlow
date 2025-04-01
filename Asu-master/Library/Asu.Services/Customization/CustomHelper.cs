using System;
using System.Web;

namespace Asu.Services.Customization
{
    public class CustomHelper : ICustomHelper
    {
        #region Fields
        
        private readonly HttpContextBase httpContext;

        #endregion

        #region Constructors

        public CustomHelper(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        #endregion
        
        #region Methods

        public void AddToCookie(string cookieName, string cookieValue, DateTime expires)
        {
            this.httpContext.Response.Cookies.Add(new HttpCookie(cookieName) { Value = cookieValue, Expires = expires });
        }

        public void DeleteCookieValue(string cookieName)
        {
            if (string.IsNullOrEmpty(cookieName))
            {
                return;
            }

            var cookie = this.httpContext.Response.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.UtcNow.AddDays(-1);
            }
        }

        public string GetCookieValue(string cookieName)
        {
            var cookie = this.httpContext.Request.Cookies.Get(cookieName);

            return cookie?.Value;
        }

        #endregion
    }
}