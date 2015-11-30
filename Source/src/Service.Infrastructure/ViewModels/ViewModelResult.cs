using System.Collections.Generic;
using System.Web.Mvc;

namespace HomeManager.Service.Infrastructure.ViewModels
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
                    cast.UpdateModel(context);
            }

            var key = context.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey) ? (string)context.RouteData.DataTokens[Consts.Consts.RouteServiceKey] : "_mainweb_";
            var list = context.HttpContext.Items[key] as List<IViewModel> ?? new List<IViewModel>();
            list.AddRange(ViewModels);
            context.HttpContext.Items[key] = list;
        }
    }
}