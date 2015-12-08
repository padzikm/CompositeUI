using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public class TableOrderViewModel : ITableOrderViewModel
    {
        public string Id { get; private set; }

        public List<ServicePublicData> SortedIds { get; private set; }

        public Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get; private set; }

        public TableOrderViewModel(string id, List<ServicePublicData> sortedIds)
        {
            Id = id;
            SortedIds = sortedIds;
            ServiceBreadcrumbsRequests = new Dictionary<string, List<ServicePublicData>>();
        }

        public MvcHtmlString Execute(WebViewPage viewPage)
        {
            throw new InvalidOperationException();
        }

        public MvcHtmlString Execute(WebViewPage viewPage, string containerId)
        {
            throw new InvalidOperationException();
        }
    }
}
