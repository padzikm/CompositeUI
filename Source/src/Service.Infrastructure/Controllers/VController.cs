using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Exceptions;
using HomeManager.Service.Infrastructure.Models;
using HomeManager.Service.Infrastructure.RequestHandlers;
using HomeManager.Service.Infrastructure.ViewModels;

namespace HomeManager.Service.Infrastructure.Controllers
{
    public class VController : Controller
    {
        protected readonly IEnumerable<IRequestHandler> _requestHandlers;

        public VController(IEnumerable<IRequestHandler> requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        public async Task HandleRequest()
        {
            var tasks = new List<Task>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.HandleRequest());

            await Task.WhenAll(tasks);
        }

        public async Task<ActionResult> HandleRequest(Func<ActionResult> onSuccess, Func<ActionResult> onInvalidModelException)
        {
            try
            {
                await HandleRequest();
                return onSuccess();
            }
            catch (InvalidModelException ex)
            {
                return onInvalidModelException();
            }
        }

        public async Task<ActionResult> HandleRequestInTransaction(Func<ActionResult> onSuccess, Func<ActionResult> onInvalidModelException)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    await HandleRequest();
                    ts.Complete();
                }
                return onSuccess();
            }
            catch (InvalidModelException ex)
            {
            }

            return onInvalidModelException();
        }

        public async Task<ActionResult> HandleRequestInTransaction(Func<ActionResult> onSuccess, Func<IDictionary<string, IViewModel>, ActionResult> onInvalidModelException, Type resourceClass)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    await HandleRequest();
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

        public async Task<IDictionary<string, IViewModel>> GenerateViewModels(Type resourceClass)
        {
            var uiKeys = GetUIKeys(resourceClass);
            var tasks = new List<Task<IEnumerable<IViewModel>>>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.GenerateViewModels(uiKeys));

            var list = await Task.WhenAll(tasks);
            var union = list.SelectMany(p => p.ToList()).ToList();
            var dict = union.ToDictionary(p => p.Id);
            UpdateServicePublicDataRequests(union);
            return dict;
        }

        public async Task<IDictionary<string, IViewModel>> GenerateViewModelsOnInvalidModelState(Type resourceClass)
        {
            var uiKeys = GetUIKeys(resourceClass);
            var tasks = new List<Task<IEnumerable<IViewModel>>>();
            foreach (var generator in _requestHandlers)
                tasks.Add(generator.GenerateViewModelsOnInvalidModelState(uiKeys));

            var list = await Task.WhenAll(tasks);
            var union = list.SelectMany(p => p.ToList()).ToList();
            var dict = union.ToDictionary(p => p.Id);
            UpdateServicePublicDataRequests(union);
            return dict;
        }

        public IEnumerable<string> GetUIKeys(Type resourceClass)
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
