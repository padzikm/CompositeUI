using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Exceptions;
using CompositeUI.Service.Infrastructure.Models;
using CompositeUI.Service.Infrastructure.RequestHandlers;
using CompositeUI.Service.Infrastructure.ViewModels;

namespace CompositeUI.Service.Infrastructure.Controllers
{
    public class WebController : Controller
    {
        protected readonly IEnumerable<IRequestHandler> _requestHandlers;

        public WebController(IEnumerable<IRequestHandler> requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        protected async Task HandleRequest()
        {
            await HandleRequest(new Dictionary<string, object>());
        }

        protected async Task HandleRequest(Dictionary<string, object> parameters)
        {
            var tasks = new List<Task>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.HandleRequest(parameters));

            await Task.WhenAll(tasks);
        }

        protected async Task<ActionResult> HandleRequest(Func<ActionResult> onSuccess, Func<ActionResult> onInvalidModelException)
        {
            return await HandleRequest(onSuccess, onInvalidModelException, new Dictionary<string, object>());
        }

        protected async Task<ActionResult> HandleRequest(Func<ActionResult> onSuccess, Func<ActionResult> onInvalidModelException, Dictionary<string, object> parameters)
        {
            try
            {
                await HandleRequest(parameters);
                return onSuccess();
            }
            catch (InvalidModelException ex)
            {
                return onInvalidModelException();
            }
        }

        protected async Task<ActionResult> HandleRequestInTransaction(Func<ActionResult> onSuccess, Func<ActionResult> onInvalidModelException)
        {
            return await HandleRequestInTransaction(onSuccess, onInvalidModelException, new Dictionary<string, object>());
        }

        protected async Task<ActionResult> HandleRequestInTransaction(Func<ActionResult> onSuccess, Func<ActionResult> onInvalidModelException, Dictionary<string, object> parameters)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    await HandleRequest(parameters);
                    ts.Complete();
                }
                return onSuccess();
            }
            catch (InvalidModelException ex)
            {
            }

            return onInvalidModelException();
        }

        protected async Task<ActionResult> HandleRequestInTransaction(Func<ActionResult> onSuccess, Func<IDictionary<string, IViewModel>, ActionResult> onInvalidModelException, Type resourceClass)
        {
            return await HandleRequestInTransaction(onSuccess, onInvalidModelException, resourceClass, new Dictionary<string, object>());
        }

        protected async Task<ActionResult> HandleRequestInTransaction(Func<ActionResult> onSuccess, Func<IDictionary<string, IViewModel>, ActionResult> onInvalidModelException, Type resourceClass, Dictionary<string, object> parameters)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    await HandleRequest(parameters);
                    ts.Complete();
                }
                return onSuccess();
            }
            catch (InvalidModelException ex)
            {
            }

            var viewModels = await GenerateViewModelsOnInvalidModelState(resourceClass);
            return onInvalidModelException(viewModels);
        }

        protected async Task<IDictionary<string, IViewModel>> GenerateViewModels(Type resourceClass)
        {
            return await GenerateViewModels(resourceClass, new Dictionary<string, object>());
        }

        protected async Task<IDictionary<string, IViewModel>> GenerateViewModels(Type resourceClass, Dictionary<string, object> parameters)
        {
            var uiKeys = GetUIKeys(resourceClass);
            var tasks = new List<Task<IEnumerable<IViewModel>>>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.GenerateViewModels(uiKeys, parameters));

            var list = await Task.WhenAll(tasks);
            var union = list.SelectMany(p => p.ToList()).ToList();
            var dict = union.ToDictionary(p => p.Id);
            UpdateServicePublicDataRequests(union);
            return dict;
        }

        protected async Task<IDictionary<string, ITableViewModel>> GenerateTableViewModels(Type resourceClass)
        {
            return await GenerateTableViewModels(resourceClass, new Dictionary<string, object>());
        }

        protected async Task<IDictionary<string, ITableViewModel>> GenerateTableViewModels(Type resourceClass, Dictionary<string, object> parameters)
        {
            var viewModels = await GenerateViewModels(resourceClass, parameters);
            var dict = viewModels.ToDictionary(p => p.Key, p => p.Value as ITableViewModel);
            if(dict.Any(p => p.Value == null))
                throw new InvalidOperationException("Request requires table response");
            return dict;
        }

        protected async Task<object> GenerateJsonViewModels()
        {
            return await GenerateJsonViewModels(new Dictionary<string, object>());
        }

        protected async Task<object> GenerateJsonViewModels(Dictionary<string,object> parameters)
        {
            var uiKeys = Enumerable.Empty<string>();
            var tasks = new List<Task<IEnumerable<IViewModel>>>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.GenerateViewModels(uiKeys, parameters));

            var list = await Task.WhenAll(tasks);
            var viewModels = list.SelectMany(p => p.ToList()).ToList();
            var jsonViewModels = viewModels.Cast<JsonViewModel>().ToList();
            if (jsonViewModels.Any(p => p == null) || jsonViewModels.Count != viewModels.Count)
                throw new InvalidOperationException("Request requires json response");
            var jsons = jsonViewModels.Select(p => new JsonData()
            {
                Name = p.Name,
                Object = p.Object,
            }).ToList();
            return jsons;
        }

        protected async Task<IDictionary<string, IViewModel>> GenerateViewModelsOnInvalidModelState(Type resourceClass)
        {
            return await GenerateViewModelsOnInvalidModelState(resourceClass, new Dictionary<string, object>());
        }

        protected async Task<IDictionary<string, IViewModel>> GenerateViewModelsOnInvalidModelState(Type resourceClass, Dictionary<string,object> parameters)
        {
            var uiKeys = GetUIKeys(resourceClass);
            var tasks = new List<Task<IEnumerable<IViewModel>>>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.GenerateViewModelsOnInvalidModelState(uiKeys, parameters));

            var list = await Task.WhenAll(tasks);
            var union = list.SelectMany(p => p.ToList()).ToList();
            var dict = union.ToDictionary(p => p.Id);
            UpdateServicePublicDataRequests(union);
            return dict;
        }

        protected IEnumerable<string> GetUIKeys(Type resourceClass)
        {
            var fields = resourceClass.GetFields(BindingFlags.Public | BindingFlags.Static);
            var list = fields.Select(p => (string)p.GetValue(null)).ToList();
            return list;
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RemoveServicePublicDataRequests();
            base.OnResultExecuted(filterContext);
        }

        private void UpdateServicePublicDataRequests(IEnumerable<IViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                var requests = viewModel.ServiceBreadcrumbsRequests;
                foreach (var pair in requests)
                {
                    if (!TempData.ContainsKey(pair.Key))
                        TempData[pair.Key] = new List<ServicePublicData>(pair.Value);
                    else
                    {
                        var list = TempData[pair.Key] as List<ServicePublicData>;
                        list.AddRange(pair.Value);
                    }
                }
            }
        }

        private void RemoveServicePublicDataRequests()
        {
            var keysToRemove = new List<string>();
            foreach (var pair in TempData)
            {
                var list = pair.Value as List<ServicePublicData>;
                if (list != null)
                    keysToRemove.Add(pair.Key);
            }
            foreach (var key in keysToRemove)
                TempData.Remove(key);
        }
    }
}
