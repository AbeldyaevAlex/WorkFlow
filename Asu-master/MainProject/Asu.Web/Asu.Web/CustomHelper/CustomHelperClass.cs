using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.CustomHelper
{
    public static class CustomHelperClass
    {
        public static MvcHtmlString CreateLabel(this HtmlHelper obj, string Content)
        {
            string labelColor = "<label style = 'background: white; color: black'>" + Content +  "</label>";
            return new MvcHtmlString(labelColor);
        }
    }
}