using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public class ViewModel : IViewModel
    {
        private readonly HtmlActionExpressionVisitor _viewModelExpressionVisitor;
        private readonly Expression<Func<WebViewPage, MvcHtmlString>> _action;
        private Func<WebViewPage, MvcHtmlString> _func;
        private string _controllerName;
        private string _areaName;
        private string _routeServiceValue;
        private ModelStateDictionary _modelState;
        private ViewDataDictionary _viewData;
        private string _containerId;

        public string Id { get; private set; }

        public bool ReturnAction { get; set; }

        public Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get; private set; }

        protected ViewModel(string id)
        {
            Id = id;
            ServiceBreadcrumbsRequests = new Dictionary<string, List<ServicePublicData>>();
            _viewModelExpressionVisitor = new HtmlActionExpressionVisitor(this);
        }

        public ViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action)
            : this(id)
        {
            _action = action;
        }

        public ViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests)
            : this(id, action)
        {
            ServiceBreadcrumbsRequests = serviceBreadcrumbsRequests;
        }

        public MvcHtmlString Execute(WebViewPage viewPage)
        {
            return Execute(viewPage, null);
        }

        public MvcHtmlString Execute(WebViewPage viewPage, string parentContainerId)
        {
            if (_func == null)
            {
                _containerId = parentContainerId;
                var exp = UpdateExpression(_action);
                _func = exp.Compile();
            }
            return _func(viewPage);
        }

        internal void UpdateViewModel(ControllerContext context)
        {
            var routeData = context.RouteData;
            _controllerName = (string)routeData.Values["controller"];
            _areaName = routeData.DataTokens.ContainsKey("area") ? (string)routeData.DataTokens["area"] : (string)null;
            _routeServiceValue = (string)routeData.DataTokens[Consts.Consts.RouteServiceKey];
            _modelState = context.Controller.ViewData.ModelState;
            _viewData = context.Controller.ViewData;
        }

        protected T UpdateExpression<T>(T expression) where T : Expression
        {
            return _viewModelExpressionVisitor.Visit(expression) as T;
        }

        private class HtmlActionExpressionVisitor : ExpressionVisitor
        {
            private static readonly MethodInfo RouteValueDictActionMethod;
            private readonly ViewModel _parent;

            static HtmlActionExpressionVisitor()
            {
                var typeList = new[]
                {
                    typeof (HtmlHelper),
                    typeof (string),
                    typeof (RouteValueDictionary),
                };
                RouteValueDictActionMethod = typeof(ChildActionExtensions).GetMethod("Action", typeList);
            }

            public HtmlActionExpressionVisitor(ViewModel parent)
            {
                _parent = parent;
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.Name == "Action")
                    return UpdateAction(node);
                return base.VisitMethodCall(node);
            }

            private MethodCallExpression UpdateAction(MethodCallExpression methodCall)
            {
                var args = methodCall.Arguments;
                if (args.Count < 2)
                    return methodCall;
                var actionNameEx = args[1] as ConstantExpression;
                if (actionNameEx == null)
                    return methodCall;
                var actionName = (string)actionNameEx.Value;
                if (actionName == null)
                    return methodCall;
                var controllerName = _parent._controllerName;
                if (args.Count > 3)
                {
                    var controllerNameEx = args[2] as ConstantExpression;
                    if (controllerNameEx == null)
                        return methodCall;
                    controllerName = (string)controllerNameEx.Value;
                    if (controllerName == null)
                        return methodCall;
                }
                var index = args.Count - 1;
                var arg = args[index] as ListInitExpression;
                var narg = args[index] as NewExpression;
                var newArg = (Expression)null;
                if (arg != null)
                    newArg = CreateRouteValueInitializers(arg, actionName, controllerName);
                else
                    newArg = CreateObjectInitializers(narg, actionName, controllerName);
                var newActionName = _parent.ReturnAction ? Expression.Constant(actionName) : Expression.Constant("_View");
                var newArgs = new List<Expression>()
            {
                args[0],
                newActionName,
                newArg,
            };
                return Expression.Call(methodCall.Object, RouteValueDictActionMethod, newArgs);
            }

            private Expression CreateRouteValueInitializers(ListInitExpression arg, string actionName, string controllerName)
            {
                var newInits = new List<ElementInit>(arg.Initializers);
                var addMethod = arg.NewExpression.Type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                var currentInitializersKeys = newInits.Select(p => p.Arguments[0]).OfType<ConstantExpression>().Select(p => (string)p.Value).ToList();
                var initializers = CreateInitializers(currentInitializersKeys, addMethod, actionName, controllerName);
                newInits.AddRange(initializers);
                return Expression.ListInit(arg.NewExpression, newInits);
            }

            private Expression CreateObjectInitializers(NewExpression arg, string actionName, string controllerName)
            {
                var ctor = typeof(RouteValueDictionary).GetConstructor(Type.EmptyTypes);
                var newExp = Expression.New(ctor);
                var addMethod = newExp.Type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                var newInits = new List<ElementInit>();
                var currentInitializersKeys = new List<string>();
                if (arg != null)
                    for (var i = 0; i < arg.Members.Count; ++i)
                    {
                        var member = arg.Members[i];
                        var name = Expression.Constant(member.Name);
                        var value = arg.Arguments[i];
                        if (value.Type.IsValueType)
                            value = Expression.Convert(value, typeof (object));
                        newInits.Add(Expression.ElementInit(addMethod, name, value));
                        currentInitializersKeys.Add(member.Name);
                    }
                var initializers = CreateInitializers(currentInitializersKeys, addMethod, actionName, controllerName);
                newInits.AddRange(initializers);
                return Expression.ListInit(newExp, newInits);
            }

            private List<ElementInit> CreateInitializers(List<string> currentInitializersKeys, MethodInfo addMethod, string actionName, string controllerName)
            {
                var elementInitList = new List<ElementInit>();
                var key = Expression.Constant(Consts.Consts.RouteServiceKey);
                var value = Expression.Constant(_parent._routeServiceValue);
                elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                if (!_parent.ReturnAction)
                {
                    key = Expression.Constant("viewName");
                    value = Expression.Constant(actionName);
                    elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                }
                if (controllerName != null && !currentInitializersKeys.Contains("controller"))
                {
                    key = Expression.Constant("controller");
                    value = Expression.Constant(controllerName);
                    elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                }
                if (_parent._areaName != null && !currentInitializersKeys.Contains("area"))
                {
                    key = Expression.Constant("area");
                    value = Expression.Constant(_parent._areaName);
                    elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                }
                if (_parent._modelState != null && !currentInitializersKeys.Contains("modelState"))
                {
                    key = Expression.Constant("modelState");
                    value = Expression.Constant(_parent._modelState);
                    elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                }
                if (_parent._viewData != null && !currentInitializersKeys.Contains("viewData"))
                {
                    key = Expression.Constant("viewData");
                    value = Expression.Constant(_parent._viewData);
                    elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                }
                if (_parent._containerId != null)
                {
                    key = Expression.Constant(Consts.Consts.ContainerId);
                    value = Expression.Constant(_parent._containerId);
                    elementInitList.Add(Expression.ElementInit(addMethod, key, value));
                }
                return elementInitList;
            }
        }
    }
}