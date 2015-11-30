using System;
using System.Web.Mvc;

namespace HomeManager.Service.Infrastructure.ViewModels
{
    public class ExceptionViewModel : IViewModel
    {
        public string Id { get; private set; }

        public Exception Exception {get; private set; }

        public ExceptionViewModel(Exception exception)
        {
            Exception = exception;
        }

        public MvcHtmlString Execute(WebViewPage viewPage)
        {
            throw new NotImplementedException();
        }
    }
}
