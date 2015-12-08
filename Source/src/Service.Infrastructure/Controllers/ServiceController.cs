using System.Collections.Generic;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.ViewModels;

namespace CompositeUI.Service.Infrastructure.Controllers
{
    public class ServiceController : Controller
    {
        [ChildActionOnly]
        public ActionResult _View(string viewName, object model, object modelState, object viewData, string containerId)
        {
            var modelStateCasted = modelState as ModelStateDictionary;
            if (modelStateCasted != null)
                ModelState.Merge(modelStateCasted);
            var viewDataCasted = viewData as ViewDataDictionary;
            if (viewDataCasted != null)
            {
                foreach (var pair in viewDataCasted)
                    ViewData.Add(pair.Key, pair.Value);
            }
            ViewData.Add(Consts.Consts.ContainerId, containerId);
            return View(viewName, model);
        }

        protected ActionResult RequestHandled()
        {
            return new ViewModelResult();
        }

        protected ViewModelResult ViewModel(IViewModel viewModel)
        {
            return new ViewModelResult(viewModel);
        }

        protected ViewModelResult ViewModels(IEnumerable<IViewModel> viewModels)
        {
            return new ViewModelResult(viewModels);
        }

        protected ViewModelResult ViewModels(params IViewModel[] viewModels)
        {
            return new ViewModelResult(viewModels);
        }

        protected ViewModelResult JsonViewModel(object obj)
        {
            var serviceValue = (string)RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            return JsonViewModel(serviceValue, obj);
        }

        protected ViewModelResult JsonViewModel(string name, object obj)
        {
            var jsonvm = new JsonViewModel(name, obj);
            return new ViewModelResult(jsonvm);
        }
    }
}