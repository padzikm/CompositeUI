using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using HomeManager.Infrastructure;
using HomeManager.Service.Infrastructure.Models;

namespace HomeManager.Service.Infrastructure.ViewModels
{
    public class ViewModel : IViewModel
    {
        private Expression<Func<WebViewPage, MvcHtmlString>> _action;
        private Func<WebViewPage, MvcHtmlString> _func;
        private static readonly MethodInfo RouteValueDictActionMethod;

        static ViewModel()
        {
            var typeList = new[]
                {
                    typeof (HtmlHelper),
                    typeof (string),
                    typeof (RouteValueDictionary),
                };
            RouteValueDictActionMethod = typeof(ChildActionExtensions).GetMethod("Action", typeList);
        }

        public string Id { get; private set; }

        public Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get; private set; }

        public ViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action)
        {
            Id = id;
            _action = action;
            ServiceBreadcrumbsRequests = new Dictionary<string, List<ServicePublicData>>();
        }

        public ViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests) : this(id, action)
        {
            ServiceBreadcrumbsRequests = serviceBreadcrumbsRequests;
        }

        public MvcHtmlString Execute(WebViewPage viewPage)
        {
            return _func(viewPage);
        }

        internal void UpdateModel(ControllerContext context)
        {
            var routeData = context.RouteData;
            var controllerName = (string)routeData.Values["controller"];
            var areaName = routeData.DataTokens.ContainsKey("area") ? (string)routeData.DataTokens["area"] : (string)null;
            var routeServiceValue = (string)routeData.DataTokens[Consts.Consts.RouteServiceKey];
            var modelState = context.Controller.ViewData.ModelState;
            if (routeServiceValue != null)
                UpdateModel(controllerName, areaName, routeServiceValue, modelState);
            _func = _action.Compile();
        }

        private void UpdateModel(string controllerName, string areaName, string routeServiceValue, ModelStateDictionary modelState)
        {
            var exp = _action as LambdaExpression;
            if (exp == null)
                return;
            var call = exp.Body as MethodCallExpression;
            if (call == null)
                return;
            if (call.Method.Name != "Action")
                return;
            var args = call.Arguments;
            if (args.Count < 2)
                return;
            var actionNameEx = args[1] as ConstantExpression;
            if (actionNameEx == null)
                return;
            var actionName = (string)actionNameEx.Value;
            if (actionName == null)
                return;
            if (args.Count > 3)
            {
                var controllerNameEx = args[2] as ConstantExpression;
                if (controllerNameEx == null)
                    return;
                controllerName = (string)controllerNameEx.Value;
                if (controllerName == null)
                    return;
            }
            var index = args.Count - 1;
            var arg = args[index] as ListInitExpression;
            var narg = args[index] as NewExpression;
            var newArg = (Expression)null;
            if (arg != null)
                newArg = RouteValueExp(arg, actionName, controllerName, areaName, routeServiceValue, modelState);
            else
                newArg = ObjectExp(narg, actionName, controllerName, areaName, routeServiceValue, modelState);
            if (newArg == null)
                return;
            var newActionName = Expression.Constant("_View");
            var newArgs = new List<Expression>()
            {
                args[0],
                newActionName,
                newArg,
            };
            var newCall = Expression.Call(call.Object, RouteValueDictActionMethod, newArgs);
            var newAction = Expression.Lambda<Func<WebViewPage, MvcHtmlString>>(newCall, _action.Parameters);
            _action = newAction;
        }

        private Expression RouteValueExp(ListInitExpression arg, string actionName, string controllerName, string areaName, string routeServiceValue, ModelStateDictionary modelState)
        {
            var newInits = new List<ElementInit>(arg.Initializers);
            var addMethod = arg.NewExpression.Type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
            var key = Expression.Constant(Consts.Consts.RouteServiceKey);
            var value = Expression.Constant(routeServiceValue);
            var newInit = Expression.ElementInit(addMethod, key, value);
            newInits.Add(newInit);
            key = Expression.Constant("viewName");
            value = Expression.Constant(actionName);
            newInit = Expression.ElementInit(addMethod, key, value);
            newInits.Add(newInit);
            if (controllerName != null)
            {
                key = Expression.Constant("controller");
                value = Expression.Constant(controllerName);
                newInit = Expression.ElementInit(addMethod, key, value);
                newInits.Add(newInit);
            }
            if (areaName != null)
            {
                key = Expression.Constant("area");
                value = Expression.Constant(areaName);
                newInit = Expression.ElementInit(addMethod, key, value);
                newInits.Add(newInit);
            }
            if (modelState != null)
            {
                key = Expression.Constant("modelState");
                value = Expression.Constant(modelState);
                newInit = Expression.ElementInit(addMethod, key, value);
                newInits.Add(newInit);
            }
            var newArg = Expression.ListInit(arg.NewExpression, newInits);
            return newArg;
        }

        private Expression ObjectExp(NewExpression arg, string actionName, string controllerName, string areaName, string routeServiceValue, ModelStateDictionary modelState)
        {
            var ctor = typeof(RouteValueDictionary).GetConstructor(Type.EmptyTypes);
            var n = Expression.New(ctor);
            var addMethod = n.Type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
            var newInits = new List<ElementInit>();

            if (arg != null)
                for (var i = 0; i < arg.Members.Count; ++i)
                {
                    var m = arg.Members[i];
                    var k = Expression.Constant(m.Name);
                    var v = arg.Arguments[i];
                    var ni = Expression.ElementInit(addMethod, k, v);
                    newInits.Add(ni);
                }

            var key = Expression.Constant(Consts.Consts.RouteServiceKey);
            var value = Expression.Constant(routeServiceValue);
            var newInit = Expression.ElementInit(addMethod, key, value);
            newInits.Add(newInit);
            key = Expression.Constant("viewName");
            value = Expression.Constant(actionName);
            newInit = Expression.ElementInit(addMethod, key, value);
            newInits.Add(newInit);
            if (controllerName != null && arg != null && arg.Members.All(p => p.Name != "controller"))
            {
                key = Expression.Constant("controller");
                value = Expression.Constant(controllerName);
                newInit = Expression.ElementInit(addMethod, key, value);
                newInits.Add(newInit);
            }
            if (areaName != null && arg != null && arg.Members.All(p => p.Name != "area"))
            {
                key = Expression.Constant("area");
                value = Expression.Constant(areaName);
                newInit = Expression.ElementInit(addMethod, key, value);
                newInits.Add(newInit);
            }
            if (modelState != null && arg != null && arg.Members.All(p => p.Name != "modelState"))
            {
                key = Expression.Constant("modelState");
                value = Expression.Constant(modelState);
                newInit = Expression.ElementInit(addMethod, key, value);
                newInits.Add(newInit);
            }

            return Expression.ListInit(n, newInits);
        }
    }
}