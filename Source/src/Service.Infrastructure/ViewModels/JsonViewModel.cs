using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    internal class JsonViewModel : IViewModel
    {
        public string Id { get { throw new InvalidOperationException(); } }

        public Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get { throw new InvalidOperationException(); } }

        public string Name { get; private set; }

        public object Object { get; private set; }

        public JsonViewModel(string name, object obj)
        {
            Name = name;
            Object = obj;
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
