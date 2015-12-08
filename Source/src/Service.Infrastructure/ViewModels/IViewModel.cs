using System.Collections.Generic;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public interface IViewModel
    {
        string Id { get; }

        Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get; }

        MvcHtmlString Execute(WebViewPage viewPage);

        MvcHtmlString Execute(WebViewPage viewPage, string containerId);
    }
}