using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Triad.CabinetOffice.Slipping.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ValidationErrorClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string errorClass)
        {
            MemberExpression body = expression.Body as MemberExpression;
            string propertyName = body.Member.Name;
            if (htmlHelper.ViewData.ModelState.IsValidField(propertyName))
            {
                // No validation errors so don't show error class
                return new MvcHtmlString(string.Empty);
            }
            else
            {
                return new MvcHtmlString(errorClass);
            }
        }
        public static MvcHtmlString ValidationErrorClassFor<TModel>(this HtmlHelper<TModel> htmlHelper, string propertyName, string errorClass)
        {
            if (htmlHelper.ViewData.ModelState.IsValidField(propertyName))
            {
                // No validation errors so don't show error class
                return new MvcHtmlString(string.Empty);
            }
            else
            {
                return new MvcHtmlString(errorClass);
            }
        }

        public static MvcHtmlString BackLink(this HtmlHelper helper, string defaultAction, object routeValues, string cssClass)
        {
            var returnUrl = HttpContext.Current.Request["returnUrl"];
            var backLink = "<a href=\"{0}\" class=\"{1}\">Back</a>";

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return new MvcHtmlString(string.Format(backLink, returnUrl, cssClass));
            }
            else if (HttpContext.Current.Request.HttpMethod == "POST" || (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.ToString().EndsWith("Create")))
            {
                return helper.ActionLink("Back", defaultAction, routeValues, new { @class = cssClass });
            }
            else if (HttpContext.Current.Request.UrlReferrer != null)
            {
                return new MvcHtmlString(string.Format(backLink, HttpContext.Current.Request.UrlReferrer.ToString(), cssClass));
            }
            return null;
        }
    }
}