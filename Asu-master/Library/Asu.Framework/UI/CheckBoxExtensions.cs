namespace Asu.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Security.Policy;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// WC. Custom CheckBoxList
    /// </summary>
    public static class CheckBoxExtensions
    {
        public static MvcHtmlString CheckBoxList<TModel>(this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, IEnumerable<CheckBoxListItem>>> sourceDataExpression, 
            object htmlAttributes = null, 
            object labelHtmlAttributes = null, 
            string disabledItemClass = null)
        {
            // Get currently select values from the ViewData model
            var list = sourceDataExpression.Compile().Invoke(htmlHelper.ViewData.Model);

            var ulTag = new TagBuilder("ul");
            ulTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            foreach (var item in list)
            {
                var labelTag = new TagBuilder("label");
                labelTag.MergeAttributes(new RouteValueDictionary(labelHtmlAttributes), true);
                if (item.IsDisabled)
                {
                    if (labelTag.Attributes.ContainsKey("class"))
                    {
                        labelTag.Attributes["class"] += disabledItemClass;
                    }
                    else
                    {
                        labelTag.Attributes.Add("class", disabledItemClass);
                    }
                }
                labelTag.InnerHtml = string.Format("<input type=\"checkbox\" style=\"display:none\" value=\"{0}\"{1}{2}/>{3}",
                    item.Value,
                    item.IsDisabled ? " disabled=\"disabled\"" : string.Empty,
                    item.IsSelected ? " checked=\"checked\"" : string.Empty,
                    item.Name);

                ulTag.InnerHtml += string.Format("<li>{0}</li>", labelTag);
            }

            return MvcHtmlString.Create(ulTag.ToString());
        }
    }
}
