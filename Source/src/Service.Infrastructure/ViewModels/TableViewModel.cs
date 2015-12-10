using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure
{
    public class TableViewModel : ViewModel, ITableViewModel
    {
        private readonly Expression<Func<WebViewPage, ServicePublicData, MvcHtmlString>> _tableAction;
        private Func<WebViewPage, ServicePublicData, MvcHtmlString> _tableFunc;
        private string p;
        private Func<WebViewPage, ServicePublicData, MvcHtmlString> func;

        public TableViewModel(string id, Expression<Func<WebViewPage, ServicePublicData, MvcHtmlString>> tableAction)
            : this(id, page => MvcHtmlString.Empty, tableAction)
        {
        }

        public TableViewModel(string id, Func<WebViewPage, ServicePublicData, string> tableFunc)
            : this(id, page => MvcHtmlString.Empty, tableFunc)
        {
        }

        public TableViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action) : base(id, action)
        {
        }

        public TableViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Expression<Func<WebViewPage, ServicePublicData, MvcHtmlString>> tableAction)
            : this(id, action, new Dictionary<string, List<ServicePublicData>>(), tableAction)
        {
        }

        public TableViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Func<WebViewPage, ServicePublicData, string> tableFunc)
            : this(id, action, new Dictionary<string, List<ServicePublicData>>(), tableFunc)
        {
        }

        public TableViewModel(string id, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests, Expression<Func<WebViewPage, ServicePublicData, MvcHtmlString>> tableAction)
            : this(id, page => MvcHtmlString.Empty, serviceBreadcrumbsRequests, tableAction)
        {
        }

        public TableViewModel(string id, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests, Func<WebViewPage, ServicePublicData, string> tableFunc)
            : this(id, page => MvcHtmlString.Empty, serviceBreadcrumbsRequests, tableFunc)
        {
        }

        public TableViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests)
            : base(id, action, serviceBreadcrumbsRequests)
        {
        }

        public TableViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests, Expression<Func<WebViewPage, ServicePublicData, MvcHtmlString>> tableAction)
            : base(id, action, serviceBreadcrumbsRequests)
        {
            _tableAction = tableAction;
        }

        public TableViewModel(string id, Expression<Func<WebViewPage, MvcHtmlString>> action, Dictionary<string, List<ServicePublicData>> serviceBreadcrumbsRequests, Func<WebViewPage, ServicePublicData, string> tableFunc)
            : base(id, action, serviceBreadcrumbsRequests)
        {
            _tableFunc = (page, data) =>
            {
                var str = tableFunc(page, data);
                return str != null ? MvcHtmlString.Create(str) : MvcHtmlString.Empty;
            };
        }

        public MvcHtmlString Execute(WebViewPage viewPage, ServicePublicData id)
        {
            if (_tableFunc == null)
            {
                var exp = UpdateExpression(_tableAction);
                _tableFunc = exp.Compile();
            }
            return _tableFunc(viewPage, id);
        }
    }
}
