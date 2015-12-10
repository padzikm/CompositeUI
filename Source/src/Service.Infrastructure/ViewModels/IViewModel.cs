using System.Collections.Generic;
using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure
{
    public interface IViewModel
    {
        string Id { get; }

        Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get; }

        MvcHtmlString Execute(WebViewPage viewPage);

        MvcHtmlString Execute(WebViewPage viewPage, string containerId);
    }
}