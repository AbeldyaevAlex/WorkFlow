namespace Asu.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class DropDownListExtensions
    {
        public static MvcHtmlString CustomDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<CustomSelectListItem> items, object htmlAttributes)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            var select = new TagBuilder("select");
            select.Attributes.Add("name", fullBindingName);
            select.Attributes.Add("id", fieldId);
            select.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            foreach (var item in items)
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", item.Value);
                option.InnerHtml = item.Text;

                if (item.Selected)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.MergeAttributes(new RouteValueDictionary(item.HtmlAttributes));
                select.InnerHtml += MvcHtmlString.Create(option.ToString(TagRenderMode.Normal));
            }

            return MvcHtmlString.Create(select.ToString(TagRenderMode.Normal));
        }
    }

    public class CustomSelectListItem : SelectListItem
    {
        public object HtmlAttributes { get; set; }
    }
}
