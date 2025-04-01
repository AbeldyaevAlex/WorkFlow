using Asu.Web.CustomAttribute;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeRolesAttribute());
        }
    }
}