using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using HomeManager.Infrastructure;
using HomeManager.Service.Infrastructure.ViewModels;

namespace HomeManager.Service.Infrastructure.Controllers
{
    public class VMController : Controller
    {
        public ActionResult RequestHandled()
        {
            return new ViewModelResult();
        }

        public ViewModelResult ViewModel(IViewModel viewModel)
        {
            return new ViewModelResult(viewModel);
        }

        public ViewModelResult ViewModels(IEnumerable<IViewModel> viewModels)
        {
            return new ViewModelResult(viewModels);
        }

        [ChildActionOnly]
        public ActionResult _View(string viewName, object model, object modelState, object viewData)
        {
            var modelStateCasted = modelState as ModelStateDictionary;
            if(modelStateCasted != null)
                ModelState.Merge(modelStateCasted);
            if (viewData != null)
            {
                var dict = (viewData as RouteValueDictionary) ?? TypeHelper.ObjectToDictionary(viewData);
                foreach(var pair in dict)
                    ViewData.Add(pair.Key, pair.Value);
            }
            return View(viewName, model);
        }
    }
}