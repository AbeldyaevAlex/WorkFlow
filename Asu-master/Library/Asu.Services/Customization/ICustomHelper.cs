using System;

namespace Asu.Services.Customization
{
    public interface ICustomHelper
    {
        void AddToCookie(string cookieName, string cookieValue, DateTime expires);
        string GetCookieValue(string cookieName);
        void DeleteCookieValue(string cookieName);
    }
}
