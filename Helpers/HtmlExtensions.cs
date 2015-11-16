using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Torchlight.Mvc5.Common.Libs.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ImageActionLink(this HtmlHelper html, string imageSource, string url)
        {
            string imageActionLink = string.Format("<a href=\"{0}\"><img width=\"50\" height=\"50\" src=\"{1}\" /></a>", url, imageSource);

            return new MvcHtmlString(imageActionLink);
        }

        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkTest, string action, string controller, object routeValues, object htmlAttributes, string imageSrc)
        {
            var img = new TagBuilder("img");
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            img.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imageSrc));

            var anchor = new TagBuilder("a") {InnerHtml = img.ToString(TagRenderMode.SelfClosing)};

            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());
        }

        public static IHtmlString BootstrapLabelFor<TModel, TProp>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProp>> property)
        {
            return helper.LabelFor<TModel, TProp>(property, new
            {
                @class = "col-md-2 control-label"
            });
        }

        public static IHtmlString BootstrapLabel(this HtmlHelper helper, string propertyName)
        {
            return helper.Label(propertyName, new
            {
                @class = "col-md-2 control-label"
            });
        }

        public static IEnumerable<SelectListItem> SetSelected(this IEnumerable<SelectListItem> selectList,
            object selectedValue)
        {
            selectList = selectList ?? new List<SelectListItem>();

            if (selectedValue == null) return selectList;

            var value = selectedValue.ToString();

            return selectList
                .Select(m => new SelectListItem
                {
                    Selected = string.Equals(value, m.Text),
                    Text = m.Text,
                    Value = m.Value
                });
        }

        public static IEnumerable<SelectListItem> SetSelectedByValue(this IEnumerable<SelectListItem> selectList,
            object selectedValue)
        {
            selectList = selectList ?? new List<SelectListItem>();

            if (selectedValue == null) return selectList;

            var value = selectedValue.ToString();

            return selectList
                .Select(m => new SelectListItem
                {
                    Selected = string.Equals(value, m.Value),
                    Text = m.Text,
                    Value = m.Value
                });
        }

        public static IEnumerable<SelectListItem> GetAutomatedList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return ((IEnumerable<SelectListItem>) htmlHelper.ViewData["DDKey_" + metadata.PropertyName]);
        }

        public static bool IsDebugBuild()
        {
#if DEBUG
            return true;
#else
            return System.Web.HttpContext.Current.IsDebuggingEnabled;
#endif
        }

        public static MvcHtmlString IncludeService(this HtmlHelper htmlHelper, string service, UrlHelper url)
        {
            return new MvcHtmlString(string.Format("script type=\"text/javascript\" src=\"{0}/js{1}\"></script>",
                url.Content(service), (IsDebugBuild() ? "debug" : "")));
        }
    }
}
