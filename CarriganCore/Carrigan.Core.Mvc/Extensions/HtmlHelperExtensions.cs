using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carrigan.Core.Mvc.Extensions;

public static class HtmlHelperExtensions
{
    public static IHtmlContent NumericTextBoxFor<TModel, TProperty>(
        this IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        int? min = null, int? max = null, object? htmlAttributes = null)
    {
        IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) ?? new Dictionary<string, object>();
        attributes["type"] = "number";
        if(min != null)
            attributes["min"] = min;
        if(max != null)
            attributes["max"] = max;

        return htmlHelper.TextBoxFor(expression, attributes);
    }
}
