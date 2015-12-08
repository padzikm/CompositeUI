using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CompositeUI.Service.Infrastructure.ViewModels;

namespace CompositeUI.Service.Infrastructure.RequestHandlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        protected abstract RouteCollection Routes { get; }

        public async Task HandleRequest()
        {
            await HandleRequest(Enumerable.Empty<string>(), new Dictionary<string, object>());
        }

        public async Task HandleRequest(Dictionary<string,object> parameters)
        {
            await HandleRequest(Enumerable.Empty<string>(), parameters);
        }

        public async Task HandleRequest(IEnumerable<string> uiKeys)
        {
            await HandleRequest(uiKeys, new Dictionary<string, object>());
        }

        public async Task HandleRequest(IEnumerable<string> uiKeys, Dictionary<string, object> parameters)
        {
            var values = new RouteValueDictionary(parameters) { { CompositeUI.Service.Infrastructure.Consts.Consts.UIKeysParamName, uiKeys } };
            await GenerateResponse(values);
        }

        public async Task<IEnumerable<IViewModel>> GenerateViewModels(IEnumerable<string> uiKeys)
        {
            return await GenerateViewModels(uiKeys, new Dictionary<string, object>());
        }

        public async Task<IEnumerable<IViewModel>> GenerateViewModels(IEnumerable<string> uiKeys, Dictionary<string,object> parameters)
        {
            var values = new RouteValueDictionary(parameters) { { CompositeUI.Service.Infrastructure.Consts.Consts.UIKeysParamName, uiKeys } };
            var viewModels = await GenerateResponse(values);
            return viewModels;
        }

        public async Task<IEnumerable<IViewModel>> GenerateViewModelsOnInvalidModelState(IEnumerable<string> uiKeys)
        {
            return await GenerateViewModelsOnInvalidModelState(uiKeys, new Dictionary<string, object>());
        }

        public async Task<IEnumerable<IViewModel>> GenerateViewModelsOnInvalidModelState(IEnumerable<string> uiKeys, Dictionary<string, object> parameters)
        {
            var values = new RouteValueDictionary(parameters) { { CompositeUI.Service.Infrastructure.Consts.Consts.UIKeysParamName, uiKeys }, { CompositeUI.Service.Infrastructure.Consts.Consts.InvalidModelStateReplayParamName, CompositeUI.Service.Infrastructure.Consts.Consts.InvalidModelStateReplayParamValue } };
            var viewModels = await GenerateResponse(values);
            return viewModels;
        }

        private async Task<IEnumerable<IViewModel>> GenerateResponse(RouteValueDictionary values)
        {
            if (Routes == null || !Routes.Any())
                throw new NotImplementedException("Routes");
            var viewModelsResultKey = (string)null;
            var list = new List<IViewModel>();
            var context = HttpContext.Current;
            var wrap = new HttpContextWrapper(context);
            var handler = GetHandler(wrap, values, out viewModelsResultKey);
            if (handler != null)
            {
                try
                {
                    HttpContext.Current.Items[viewModelsResultKey] = new List<IViewModel>();
                    await Task.Factory.FromAsync(handler.BeginProcessRequest, handler.EndProcessRequest, context, null);
                    list = (List<IViewModel>)HttpContext.Current.Items[viewModelsResultKey] ?? new List<IViewModel>();
                }
                catch
                {
                }
                finally
                {
                    HttpContext.Current.Items.Remove(viewModelsResultKey);
                }
                var ex = list.FirstOrDefault(p => p is ExceptionViewModel);
                if (ex != null)
                {
                    var cast = ex as ExceptionViewModel;
                    throw cast.Exception;
                }
            }
            return list;
        }

        private IHttpAsyncHandler GetHandler(HttpContextBase context, RouteValueDictionary values, out string resultKey)
        {
            // Match the incoming URL against the route table
            RouteData routeData = Routes.GetRouteData(context);
            
            // Do nothing if no route found 
            if (routeData == null)
            {
                resultKey = null;
                return null;
            }
            resultKey = (string)routeData.DataTokens[CompositeUI.Service.Infrastructure.Consts.Consts.RouteServiceKey];

            if (values != null)
                foreach (var value in values)
                    routeData.Values.Add(value.Key, value.Value);

            // If a route was found, get an IHttpHandler from the route's RouteHandler 
            IRouteHandler routeHandler = routeData.RouteHandler;
            //if (routeHandler == null) {
            //    throw new InvalidOperationException(
            //        String.Format( 
            //            CultureInfo.CurrentUICulture,
            //            System.Web.SR.GetString(System.Web.SR.UrlRoutingModule_NoRouteHandler))); 
            //} 
            if (routeHandler == null)
                return null;

            // This is a special IRouteHandler that tells the routing module to stop processing 
            // routes and to let the fallback handler handle the request.
            if (routeHandler is StopRoutingHandler)
            {
                return null;
            }

            RequestContext requestContext = new RequestContext(context, routeData);

            // Dev10 766875	Adding RouteData to HttpContext
            context.Request.RequestContext = requestContext;

            //IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
            var httpHandler = new MvcHandler(requestContext);
            return httpHandler;
            //if (httpHandler == null) {
            //    throw new InvalidOperationException( 
            //        String.Format(
            //            CultureInfo.CurrentUICulture, 
            //            System.Web.SR.GetString(System.Web.SR.UrlRoutingModule_NoHttpHandler), 
            //            routeHandler.GetType()));
            //} 

            //if (httpHandler is UrlAuthFailureHandler) {
            //    if (FormsAuthenticationModule.FormsAuthRequired) {
            //        UrlAuthorizationModule.ReportUrlAuthorizationFailure(HttpContext.Current, this); 
            //        return;
            //    } 
            //    else { 
            //        throw new HttpException(401, System.Web.SR.GetString(System.Web.SR.Assess_Denied_Description3));
            //    } 
            //}

            //// Remap IIS7 to our handler
            //HttpContext.Current.RemapHandler(httpHandler); 
        }
    }
}