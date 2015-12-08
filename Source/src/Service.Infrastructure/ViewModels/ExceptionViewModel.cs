using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Models;

namespace CompositeUI.Service.Infrastructure.ViewModels
{
    public class ExceptionViewModel : IViewModel
    {
        public string Id { get; private set; }

        public Dictionary<string, List<ServicePublicData>> ServiceBreadcrumbsRequests { get { throw new InvalidOperationException(); } }

        public Exception Exception {get; private set; }

        public ExceptionViewModel(Exception exception)
        {
            Exception = exception;
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
