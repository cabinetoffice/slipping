using System;
using System.Collections.Generic;
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
            var backLink = "<a href=\"{0}\" class=\"{1}\">Back</a>";
            var referrer = HttpContext.Current.Request.UrlReferrer;

            if (referrer != null && referrer.ToString().EndsWith("CheckYourAnswers"))
            {
                return new MvcHtmlString(string.Format(backLink, referrer.ToString(), cssClass));
            }
            else
            {
                return helper.ActionLink("Back", defaultAction, routeValues, new { @class = cssClass });
            }
        }

        public static MvcHtmlString DropDownListWithErrorClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, string errorClass)
        {
            MemberExpression body = expression.Body as MemberExpression;
            string propertyName = body.Member.Name;
            if (htmlHelper.ViewData.ModelState.IsValidField(propertyName))
            {
                return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, htmlAttributes);
            }
            else
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (attributes.ContainsKey("class"))
                    attributes["class"] += " " + errorClass;
                else
                    attributes.Add("class", errorClass);
                return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, attributes);
            }
        }

        public static MvcHtmlString TextAreaWithErrorClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, string errorClass)
        {
            MemberExpression body = expression.Body as MemberExpression;
            string propertyName = body.Member.Name;
            if (htmlHelper.ViewData.ModelState.IsValidField(propertyName))
            {
                return TextAreaExtensions.TextAreaFor(htmlHelper, expression, htmlAttributes);
            }
            else
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (attributes.ContainsKey("class"))
                    attributes["class"] += " " + errorClass;
                else
                    attributes.Add("class", errorClass);
                return TextAreaExtensions.TextAreaFor(htmlHelper, expression, attributes);
            }
        }

        public static MvcHtmlString TextBoxWithErrorClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, string errorClass)
        {
            MemberExpression body = expression.Body as MemberExpression;
            string propertyName = body.Member.Name;
            if (htmlHelper.ViewData.ModelState.IsValidField(propertyName))
            {
                return InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes);
            }
            else
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (attributes.ContainsKey("class"))
                    attributes["class"] += " " + errorClass;
                else
                    attributes.Add("class", errorClass);
                return InputExtensions.TextBoxFor(htmlHelper, expression, attributes);
            }
        }

        public static MvcHtmlString TextBoxWithErrorClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, string validationProperty, string errorClass)
        {
            if (htmlHelper.ViewData.ModelState.IsValidField(validationProperty))
            {
                return InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes);
            }
            else
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (attributes.ContainsKey("class"))
                    attributes["class"] += " " + errorClass;
                else
                    attributes.Add("class", errorClass);
                return InputExtensions.TextBoxFor(htmlHelper, expression, attributes);
            }
        }
    }
}