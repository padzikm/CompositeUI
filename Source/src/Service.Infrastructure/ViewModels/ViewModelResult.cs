using System.Collections.Generic;
using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public class ViewModelResult : ActionResult
    {
        public IEnumerable<IViewModel> ViewModels { get; private set; } 

        internal ViewModelResult()
        {
            ViewModels = new List<IViewModel>();
        }

        public ViewModelResult(IViewModel viewmodel)
        {
            ViewModels = new List<IViewModel>() {viewmodel};
        }

        public ViewModelResult(IEnumerable<IViewModel> viewModels)
        {
            ViewModels = new List<IViewModel>(viewModels);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            foreach (var viewModel in ViewModels)
            {
                var cast = viewModel as ViewModel;
                if (cast != null)
                    cast.UpdateViewModel(context);
            }

            var key = (string) context.RouteData.DataTokens[CompositeUI.Service.Infrastructure.Consts.Consts.RouteServiceKey];
            var list = context.HttpContext.Items[key] as List<IViewModel> ?? new List<IViewModel>();
            list.AddRange(ViewModels);
            context.HttpContext.Items[key] = list;
        }
    }
}