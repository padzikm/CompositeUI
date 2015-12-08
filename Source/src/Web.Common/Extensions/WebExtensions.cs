using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CompositeUI.Web.Common.Extensions
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

        public static MvcHtmlString ActionPost<TModel>(this HtmlHelper<TModel> html, string url, string text)
        {
            var str = "<form method=\"POST\" action=\"" + url + "\"><button>" + text + "</button></form>";
            return MvcHtmlString.Create(str);
        }

        public static MvcHtmlString Edit<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.EditorFor(expression, "FormRow");
        }

        public static MvcHtmlString EditPublic<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.EditorFor(expression, "FormRow", new { _excludePrefix = false });
        }

        public static MvcHtmlString Display<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DisplayFor(expression, "FormRow");
        }

        public static string Serialize<T>(this IEnumerable<T> collection, string name)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if(properties.Any())
                throw new ArgumentException("Collection can contain only value types");
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");
            var sb = new StringBuilder();
            foreach(var elem in collection)
                sb.AppendFormat("&{0}={1}", name, elem);
            return sb.ToString(1, sb.Length - 1);
        }

        public static string Serialize<T>(this IList<T> collection)
        {
            return Serialize(collection, "");
        }

        public static string Serialize<T>(this IList<T> collection, string name)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if(!properties.Any() && string.IsNullOrEmpty(name))
                throw new ArgumentException("Value types must have names");
            var sb = new StringBuilder();
            for (var i = 0; i < collection.Count; ++i)
            {
                var obj = collection[i];
                foreach (var propertyInfo in properties)
                {
                    var value = propertyInfo.GetValue(obj);
                    if (value != null)
                    {
                        if (!string.IsNullOrEmpty(name))
                            sb.AppendFormat("&{0}[{1}].{2}={3}", name, i, propertyInfo.Name, value);
                        else
                            sb.AppendFormat("&[{0}].{1}={2}", i, propertyInfo.Name, value);
                    }
                }
                if(!properties.Any())
                    sb.AppendFormat("&{0}[{1}]={2}", name, i, obj);
            }
            return sb.ToString(1, sb.Length - 1);
        }
    }
}