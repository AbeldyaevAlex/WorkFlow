using System;
using System.Web.Mvc;
using Asu.Framework.UI.Paging;

namespace Asu.Web.Extensions
{
    public static class CustomHtmlExtensions
    {
        public static PostPager PostPager(this HtmlHelper helper, IPageableModel pagination, string jsPostFormFunctionName)
        {
            return new PostPager(pagination, helper.ViewContext, jsPostFormFunctionName);
        }

        public static PostPager PostPager(this HtmlHelper helper, string viewDataKey, string jsPostFormFunctionName)
        {
            var dataSource = helper.ViewContext.ViewData.Eval(viewDataKey) as IPageableModel;

            if (dataSource == null)
            {
                throw new InvalidOperationException(string.Format("Item in ViewData with key '{0}' is not an IPagination.", viewDataKey));
            }

            return helper.PostPager(dataSource, jsPostFormFunctionName);
        }
    }
}