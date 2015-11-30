using System.Web.Mvc;
using System.Web.Mvc.Html;
using HomeManager.Service.Infrastructure.Attributes;
using HomeManager.Service.Infrastructure.Controllers;
using HomeManager.Service.Infrastructure.ViewModels;
using HomeManager.Web.Common.UIKeys.TestArea;

namespace HomeManager.Products.Web.Areas.TestArea.Controllers
{
    public class TestController : VMController
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