using System;
using System.Web.Mvc;

namespace Asu.Framework.UI.Paging
{
    public sealed class PostPager : Pager
    {
        private string jsPostFormFunctionName;

        public PostPager(IPageableModel model, ViewContext context, string jsPostFormFunctionName)
            : base(model, context)
        {
            this.jsPostFormFunctionName = jsPostFormFunctionName;
        }

        public PostPager JsPostFunctionName(string functionName)
        {
            this.jsPostFormFunctionName = functionName;
            return this;
        }

        protected override string CreatePageLink(int pageNumber, string text, string cssClass)
        {
            var liBuilder = new TagBuilder("li");
            if (!String.IsNullOrWhiteSpace(cssClass))
                liBuilder.AddCssClass(cssClass);

            var aBuilder = new TagBuilder("a");
            aBuilder.SetInnerText(text);
            aBuilder.MergeAttribute("href", string.Format("?PFC.PageNumber={0}", pageNumber));
            aBuilder.MergeAttribute("onclick", urlBuilder(pageNumber) + " return false;");

            liBuilder.InnerHtml += aBuilder;

            return liBuilder.ToString(TagRenderMode.Normal);
        }

        protected override string CreateDefaultUrl(int pageIndex)
        {
            var url = string.Empty;
            if (!string.IsNullOrEmpty(jsPostFormFunctionName))
            {
                url += string.Format("{0}({1});", jsPostFormFunctionName, pageIndex);
            }

            return url;
        }
    }
}