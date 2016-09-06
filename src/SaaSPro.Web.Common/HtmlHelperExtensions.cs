using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SaaSPro.Web.Common
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, htmlAttributes);
        }

        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(object defaultValues, IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;

            if (defaultValuesList == null)
                return selectList;

            IEnumerable<string> values = from object value in defaultValuesList
                                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            selectList.ForEach(item =>
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            });

            return newSelectList;
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox"); // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                if (item.Selected)
                    cb.MergeAttribute("checked", "checked");

                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;

                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }

        /// <summary>
        /// Returns a description for this model property
        /// </summary>
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(expression, "expression");

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return new HtmlString(metadata.Description);
        }


        /// <summary>
        /// Generates a pager from a paged result set.
        /// </summary>
        public static IHtmlString PagerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            Func<int, string> pageUrlGenerator, object htmlAttributes = null)
            where TProperty : IPagedList
        {
            var stats = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model as IPagedList;

            if (stats == null || !stats.HasResults || stats.TotalPages <= 1)
                return MvcHtmlString.Empty;

            return htmlHelper.Pager(stats.Page, stats.TotalPages, pageUrlGenerator, htmlAttributes);
        }

        /// <summary>
        /// Generates a Twitter Bootstrap compatible page.
        /// </summary>
        public static IHtmlString Pager(this HtmlHelper htmlHelper, int currentPage, int totalPages, Func<int, string> pageUrlGenerator, object htmlAttributes = null)
        {
            var ul = new TagBuilder("ul");
            ul.MergeAttribute("class", "pagination");
            ul.MergeAttributes(new RouteValueDictionary(htmlAttributes), replaceExisting: true);
            int currentPageRowDecade = currentPage / 10;
            var startPage = currentPage % 10 > 0 ? currentPageRowDecade * 10 : (currentPageRowDecade - 1) * 10;
            var endPage = totalPages > startPage + 10 ? startPage + 11 : totalPages + 1;
            if (currentPage > 10)
            {
                var li = PageLiItem(currentPage, pageUrlGenerator, startPage, true);
                ul.InnerHtml += li.ToString();
            }
            for (int i = startPage + 1; i < endPage; i++)
            {
                var li = PageLiItem(currentPage, pageUrlGenerator, i);
                ul.InnerHtml += li.ToString();
            }
            if (totalPages > startPage + 10)
            {
                var li = PageLiItem(currentPage, pageUrlGenerator, startPage + 11, true);
                ul.InnerHtml += li.ToString();
            }
            return new HtmlString(ul.ToString());
        }

        private static TagBuilder PageLiItem(int currentPage, Func<int, string> pageUrlGenerator, int i, bool isCornerPage = false)
        {
            var li = new TagBuilder("li");
            if (i == currentPage)
                li.AddCssClass("active");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrlGenerator(i));
            a.SetInnerText(isCornerPage ? "..." : i.ToString());

            li.InnerHtml = a.ToString();
            return li;
        }

        public static bool Contains(this string input, string find, StringComparison comparisonType)
        {
            return !string.IsNullOrWhiteSpace(input) && input.IndexOf(find, comparisonType) > -1;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string prop, string order)
        {
            var type = typeof(T);
            var property = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                order.Equals("asc", StringComparison.InvariantCultureIgnoreCase) ? "OrderBy" : "OrderByDescending",
                new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}