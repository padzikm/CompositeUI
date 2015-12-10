using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure
{
    public interface ITableViewModel : IViewModel
    {
        MvcHtmlString Execute(WebViewPage viewPage, ServicePublicData id);
    }
}
