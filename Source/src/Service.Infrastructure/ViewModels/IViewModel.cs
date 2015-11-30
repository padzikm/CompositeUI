using System.Web.Mvc;

namespace HomeManager.Service.Infrastructure.ViewModels
{
    public interface IViewModel
    {
        string Id { get; }

        MvcHtmlString Execute(WebViewPage viewPage);
    }
}