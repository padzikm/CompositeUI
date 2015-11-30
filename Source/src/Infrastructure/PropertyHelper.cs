using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomeManager.Infrastructure
{
    internal class PropertyHelper
    {
        private static ConcurrentDictionary<Type, PropertyHelper[]> _reflectionCache =
            new ConcurrentDictionary<Type, PropertyHelper[]>();

        private static readonly MethodInfo _callPropertyGetterOpenGenericMethod =
            typeof (PropertyHelper).GetMethod("CallPropertyGetter",
                BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly MethodInfo _callPropertyGetterByReferenceOpenGenericMethod =
            typeof (PropertyHelper).GetMethod("CallPropertyGetterByReference",
                BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly MethodInfo _callPropertySetterOpenGenericMethod =
            typeof (PropertyHelper).GetMethod("CallPropertySetter",
                BindingFlags.Static | BindingFlags.NonPublic);

        private Func<object, object> _valueGetter;

        public virtual string Name { get; protected set; }

        public PropertyHelper(PropertyInfo property)
        {
            this.Name = property.Name;
            this._valueGetter = PropertyHelper.MakeFastPropertyGetter(property);
        }

        public static Action<TDeclaringType, object> MakeFastPropertySetter<TDeclaringType>(PropertyInfo propertyInfo)
            where TDeclaringType : class
        {
            MethodInfo setMethod = propertyInfo.GetSetMethod();
            Type reflectedType = propertyInfo.ReflectedType;
            Type parameterType = setMethod.GetParameters()[0].ParameterType;
            return
                (Action<TDeclaringType, object>)
                    Delegate.CreateDelegate(typeof (Action<TDeclaringType, object>),
                        (object)
                            setMethod.CreateDelegate(typeof (Action<,>).MakeGenericType(reflectedType, parameterType)),
                        PropertyHelper._callPropertySetterOpenGenericMethod.MakeGenericMethod(
                            reflectedType, parameterType));
        }

        public object GetValue(object instance)
        {
            return this._valueGetter(instance);
        }

        public static PropertyHelper[] GetProperties(object instance)
        {
            return PropertyHelper.GetProperties(instance,
                new Func<PropertyInfo, PropertyHelper>(
                    PropertyHelper.CreateInstance),
                PropertyHelper._reflectionCache);
        }

        public static Func<object, object> MakeFastPropertyGetter(PropertyInfo propertyInfo)
        {
            MethodInfo getMethod = propertyInfo.GetGetMethod();
            Type reflectedType = getMethod.ReflectedType;
            Type returnType = getMethod.ReturnType;
            Delegate @delegate;
            if (reflectedType.IsValueType)
                @delegate = Delegate.CreateDelegate(typeof (Func<object, object>),
                    (object)
                        getMethod.CreateDelegate(
                            typeof (PropertyHelper.ByRefFunc<,>).MakeGenericType(reflectedType,
                                returnType)),
                    PropertyHelper._callPropertyGetterByReferenceOpenGenericMethod.MakeGenericMethod
                        (reflectedType, returnType));
            else
                @delegate = Delegate.CreateDelegate(typeof (Func<object, object>),
                    (object) getMethod.CreateDelegate(typeof (Func<,>).MakeGenericType(reflectedType, returnType)),
                    PropertyHelper._callPropertyGetterOpenGenericMethod.MakeGenericMethod(
                        reflectedType, returnType));
            return (Func<object, object>) @delegate;
        }

        private static PropertyHelper CreateInstance(PropertyInfo property)
        {
            return new PropertyHelper(property);
        }

        private static object CallPropertyGetter<TDeclaringType, TValue>(Func<TDeclaringType, TValue> getter,
            object @this)
        {
            return (object) getter((TDeclaringType) @this);
        }

        private static object CallPropertyGetterByReference<TDeclaringType, TValue>(
            PropertyHelper.ByRefFunc<TDeclaringType, TValue> getter, object @this)
        {
            TDeclaringType declaringType = (TDeclaringType) @this;
            return (object) getter(ref declaringType);
        }

        private static void CallPropertySetter<TDeclaringType, TValue>(Action<TDeclaringType, TValue> setter,
            object @this, object value)
        {
            setter((TDeclaringType) @this, (TValue) value);
        }

        protected static PropertyHelper[] GetProperties(object instance,
            Func<PropertyInfo, PropertyHelper> createPropertyHelper,
            ConcurrentDictionary<Type, PropertyHelper[]> cache)
        {
            Type type = instance.GetType();
            PropertyHelper[] propertyHelperArray;
            if (!cache.TryGetValue(type, out propertyHelperArray))
            {
                IEnumerable<PropertyInfo> enumerable =
                    Enumerable.Where<PropertyInfo>(
                        (IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Public),
                        (Func<PropertyInfo, bool>) (prop =>
                        {
                            if (prop.GetIndexParameters().Length == 0)
                                return prop.GetMethod != (MethodInfo) null;
                            return false;
                        }));
                List<PropertyHelper> list = new List<PropertyHelper>();
                foreach (PropertyInfo propertyInfo in enumerable)
                {
                    PropertyHelper propertyHelper = createPropertyHelper(propertyInfo);
                    list.Add(propertyHelper);
                }
                propertyHelperArray = list.ToArray();
                cache.TryAdd(type, propertyHelperArray);
            }
            return propertyHelperArray;
        }

        private delegate TValue ByRefFunc<TDeclaringType, TValue>(ref TDeclaringType arg);
    }
}
