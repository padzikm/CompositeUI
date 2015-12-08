using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Infrastructure;
using CompositeUI.Orders.Web.Models;
using CompositeUI.Service.Infrastructure.Attributes;
using CompositeUI.Service.Infrastructure.Consts;
using CompositeUI.Service.Infrastructure.Controllers;
using CompositeUI.Service.Infrastructure.ViewModels;
using CompositeUI.Web.Common.UIKeys;

namespace CompositeUI.Orders.Web.Controllers
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
            var vm = new ViewModel(UIKeysTest.AddCustomer.AgeData, p => p.Html.Action("AddCustomer", new { model }));
            return ViewModel(vm);
        }

        [HttpPost]
        [InternalAction]
        public ActionResult AddCustomer(Guid testId, [Bind(Prefix = OrdersConsts.RouteServiceValue)] TestModel model)
        {
            if (!ModelState.IsValid)
                throw new InvalidOperationException();
            return RequestHandled();
        }

        [InternalAction]
        public ActionResult Summary(Guid id)
        {
            var model = new TestUrlModel()
            {
                UrlInvocation = "Url.Action(\"CustomersDetails\")",
                GeneratedActionUrl = Url.Action("CustomerDetails"),
                ShouldBeActionUrl = "/" + OrdersConsts.ServiceName + "/Test/CustomersDetails",
                DependencyResult = _importantClass.GenerateText("Summary"),
            };
            var vm = new ViewModel(UIKeysTest.Summary.OrdersDiv, p => p.Html.Action("Summary", new { model }));
            return ViewModel(vm);
        }

        [ServiceAction]
        public ActionResult CustomerDetails()
        {
            var model = new TestUrlModel()
            {
                UrlInvocation = "Url.Action(\"AddCustomer\", new { area = \"\", _external_ = true })",
                GeneratedActionUrl = Url.Action("AddCustomer", new { area = "", _external_ = true }),
                ShouldBeActionUrl = "/Test/AddCustomer",
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
                ShouldBeActionUrl = "/" + OrdersConsts.ServiceName + "/Test/AddCustomer",
                DependencyResult = _importantClass.GenerateText("AddCustomer"),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}