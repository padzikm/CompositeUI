using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Service.Infrastructure;
using CompositeUI.Web.Common.UIKeys.TestArea;

namespace CompositeUI.Products.Web.Areas.TestArea.Controllers
{
    public class TestController : ServiceController
    {
        [InternalAction]
        [GetAndReplayPost]
        public ActionResult AddCustomer()
        {
            var vm = new ViewModel(UIKeysAreaTest.AddCustomer.BeautifulLabel, p => p.Html.Action("AddCustomer"));
            return ViewModel(vm);
        }
	}
}