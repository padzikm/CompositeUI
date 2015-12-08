using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public interface ITableViewModel : IViewModel
    {
        MvcHtmlString Execute(WebViewPage viewPage, ServicePublicData id);
    }
}
