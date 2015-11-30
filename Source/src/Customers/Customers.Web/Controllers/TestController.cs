using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using HomeManager.Customers.Contracts.Commands;
using HomeManager.Customers.Web.Models;
using HomeManager.Infrastructure;
using HomeManager.Service.Infrastructure.Attributes;
using HomeManager.Service.Infrastructure.Consts;
using HomeManager.Service.Infrastructure.Controllers;
using HomeManager.Service.Infrastructure.ViewModels;
using HomeManager.Web.Common.UIKeys;
using NServiceBus;

namespace HomeManager.Customers.Web.Controllers
{
    public class TestController : VMController
    {
        private readonly IBus _bus;
        private readonly ImportantClass _importantClass;

        public TestController(IBus bus, ImportantClass importantClass)
        {
            _bus = bus;
            _importantClass = importantClass;
        }

        [HttpGet]
        [InternalAction]
        public ActionResult AddCustomer()
        {
            var model = new TestModel();
            var vm = new ViewModel(UIKeysTest.AddCustomer.PersonalData, p => p.Html.Action("AddCustomer", new { model }));
            //ViewModel.ExecuteResult results in: p => p.Html.Action("_View", new { controller = "Test", area = "CustomersService", viewName = "AddCustomer", model = model, serviceKey = "CustomersService" })
            return ViewModel(vm);
        }

        [HttpPost]
        [InternalAction]
        public ActionResult AddCustomer(Guid testId, [Bind(Prefix = CustomersConsts.RouteServiceValue)] TestModel model)
        {
            var cmd = new CreateCustomer()
            {
                Id = testId,
                Name = model.Name,
                Surname = model.Surname
            };
            _bus.Send(cmd);
            return RequestHandled();
        }

        [InternalAction]
        public ActionResult Summary(Guid id)
        {
            var model = new TestUrlModel()
            {
                UrlInvocation = "Url.Action(\"CustomersDetails\")",
                GeneratedActionUrl = Url.Action("CustomerDetails"),
                ShouldBeActionUrl = "/" + CustomersConsts.ServiceName + "/Test/CustomersDetails",
                DependencyResult = _importantClass.GenerateText("Summary"),
            };
            var vm = new ViewModel(UIKeysTest.Summary.CustomersDiv, p => p.Html.Action("Summary", new { model }));
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
                ShouldBeActionUrl = "/" + CustomersConsts.ServiceName + "/Test/AddCustomer",
                DependencyResult = _importantClass.GenerateText("AddCustomer"),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}