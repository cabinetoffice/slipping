using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

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
    }
}