using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HomeManager.Web.Common.Extensions
{
    public static class WebExtensions
    {
        public static void RemoveFromVisited(this TemplateInfo templateInfo, ViewDataDictionary viewData)
        {
            var visitedObjects = GetVisitedObjects(templateInfo);
            object visitedObjectsKey = viewData.ModelMetadata.Model ?? GetRealModelType(viewData.ModelMetadata);
            visitedObjects.Remove(visitedObjectsKey);
        }

        private static HashSet<object> GetVisitedObjects(TemplateInfo templateInfo)
        {
            var propertyInfo = typeof(TemplateInfo).GetProperty("VisitedObjects", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var obj = propertyInfo.GetValue(templateInfo);
            var hash = obj as HashSet<object>;
            return hash;
        }

        private static Type GetRealModelType(ModelMetadata metadata)
        {
            var propertyInfo = typeof(ModelMetadata).GetProperty("RealModelType", BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance);
            var obj = propertyInfo.GetValue(metadata);
            var type = obj as Type;
            return type;
        }

        public static MvcHtmlString Edit<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.EditorFor(expression, "FormRow");
        }

        public static MvcHtmlString EditPublic<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.EditorFor(expression, "FormRow", new { _includePrefix = false });
        }

        public static MvcHtmlString Display<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DisplayFor(expression, "FormRow");
        }
    }
}