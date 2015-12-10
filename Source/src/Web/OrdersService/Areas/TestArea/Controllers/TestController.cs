using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Infrastructure;
using CompositeUI.Orders.Web.Models;
using CompositeUI.Service.Infrastructure;
using CompositeUI.Web.Common.UIKeys.TestArea;

namespace CompositeUI.Orders.Web.Areas.TestArea.Controllers
{
    public class TestController : ServiceController
    {
        private readonly ImportantClass _importantClass;

        public TestController(ImportantClass importantClass)
        {
            _importantClass = importantClass;
        }

        [HttpGet]
        [InternalAction]
        public ActionResult AddCustomer()
        {
            var model = new TestModel();
            var vm = new ViewModel(UIKeysAreaTest.AddCustomer.AgeData, p => p.Html.Action("AddCustomer", new { model }));
            return ViewModel(vm);
        }

        [HttpPost]
        [InternalAction]
        public ActionResult AddCustomer(Guid testId, [Bind(Prefix = OrdersConsts.RouteServiceValue)] TestModel model, bool isModelValid = true)
        {
            if(!isModelValid)
                return ViewModel(new ViewModel(UIKeysAreaTest.AddCustomer.AgeData, p => p.Html.Action("AddCustomer", new { model })));
            if (!ModelState.IsValid)
                throw new InvalidModelException();
            return RequestHandled();
        }

        [InternalAction]
        public ActionResult Summary(Guid id)
        {
            var model = new TestUrlModel()
            {
                UrlInvocation = "Url.Action(\"CustomersDetails\")",
                GeneratedActionUrl = Url.Action("CustomerDetails"),
                ShouldBeActionUrl = "/" + OrdersConsts.ServiceName + "/TestArea/Test/CustomersDetails",
                DependencyResult = _importantClass.GenerateText("Summary"),
            };
            var vm = new ViewModel(UIKeysAreaTest.Summary.OrdersDiv, p => p.Html.Action("Summary", new { model }));
            return ViewModel(vm);
        }

        [ServiceAction]
        public ActionResult CustomerDetails()
        {
            var model = new TestUrlModel()
            {
                UrlInvocation = "Url.Action(\"AddCustomer\", new { _external_ = true })",
                GeneratedActionUrl = Url.Action("AddCustomer", new { _external_ = true }),
                ShouldBeActionUrl = "/TestArea/Test/AddCustomer",
                DependencyResult = _importantClass.GenerateText("CustomerDetails"),
            };
            return View(model);
        }

        [ServiceAction]
        public ActionResult AddCustomer(object o)
        {
            var model = new TestUrlModel()
            {
                UrlInvocation = "Url.Action(\"AddCustomer\")",
                GeneratedActionUrl = Url.Action("AddCustomer"),
                ShouldBeActionUrl = "/" + OrdersConsts.ServiceName + "/TestArea/Test/AddCustomer",
                DependencyResult = _importantClass.GenerateText("AddCustomer"),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}