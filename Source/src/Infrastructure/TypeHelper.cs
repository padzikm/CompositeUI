using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Routing;

namespace HomeManager.Infrastructure
{
    public static class TypeHelper
    {
        public static RouteValueDictionary ObjectToDictionary(object value)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
            if (value != null)
            {
                foreach (PropertyHelper propertyHelper in PropertyHelper.GetProperties(value))
                    routeValueDictionary.Add(propertyHelper.Name, propertyHelper.GetValue(value));
            }
            return routeValueDictionary;
        }

        public static RouteValueDictionary ObjectToDictionaryUncached(object value)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
            if (value != null)
            {
                foreach (PropertyHelper propertyHelper in PropertyHelper.GetProperties(value))
                    routeValueDictionary.Add(propertyHelper.Name, propertyHelper.GetValue(value));
            }
            return routeValueDictionary;
        }

        public static void AddAnonymousObjectToDictionary(IDictionary<string, object> dictionary, object value)
        {
            foreach (KeyValuePair<string, object> keyValuePair in TypeHelper.ObjectToDictionary(value))
                dictionary.Add(keyValuePair);
        }

        public static bool IsAnonymousType(Type type)
        {
            if (type == (Type)null)
                throw new ArgumentNullException("type");
            if (!Attribute.IsDefined((MemberInfo)type, typeof(CompilerGeneratedAttribute), false) || !type.IsGenericType || !type.Name.Contains("AnonymousType") || !type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) && !type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                return false;
            int num = (int)type.Attributes;
            return 0 == 0;
        }
    }
}
