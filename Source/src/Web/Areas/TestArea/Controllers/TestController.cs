using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Controllers;
using HomeManager.Service.Infrastructure.RequestHandlers;
using HomeManager.Web.Common.UIKeys;
using HomeManager.Web.Common.UIKeys.TestArea;

namespace HomeManager.Web.Areas.TestArea.Controllers
{
    public class TestController : VController
    {
        public TestController(IEnumerable<IRequestHandler> requestHandlers) : base(requestHandlers)
        {
        }

        [HttpGet]
        public async Task<ActionResult> AddCustomer()
        {
            var vms = await GenerateViewModels(typeof(UIKeysAreaTest.AddCustomer));
            ViewBag.Id = Guid.NewGuid();
            return View(vms);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Guid testId)
        {
            var view = await HandleRequestInTransaction(() => RedirectToAction("Summary", new { id = testId }), (vms) => View(vms), typeof(UIKeysTest.AddCustomer));
            ViewBag.Id = testId;
            return view;
        }

        public async Task<ActionResult> Summary(Guid id)
        {
            var vms = await GenerateViewModels(typeof(UIKeysAreaTest.Summary));
            return View(vms);
        }
	}
}