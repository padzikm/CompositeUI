using System.Collections.Generic;
using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Models;

namespace HomeManager.Service.Infrastructure.ViewModels
{
    public interface IViewModel
    {
        string Id { get; }

        Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get; }

        MvcHtmlString Execute(WebViewPage viewPage);
    }
}