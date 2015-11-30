using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Controllers;
using HomeManager.Service.Infrastructure.RequestHandlers;
using HomeManager.Web.Common.UIKeys;

namespace HomeManager.Web.Controllers
{
    public class TestController : VController
    {
        public TestController(IEnumerable<IRequestHandler> requestHandlers) : base(requestHandlers)
        {
        }

        [HttpGet]
        public async Task<ActionResult> AddCustomer()
        {
            var vms = await GenerateViewModels(typeof(UIKeysTest.AddCustomer));
            ViewBag.Id = Guid.NewGuid();
            return View(vms);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Guid testId)
        {
            return await HandleRequestInTransaction(() => RedirectToAction("Summary", new { id = testId }), () => RedirectToAction("AddCustomer"));
        }

        public async Task<ActionResult> Summary(Guid id)
        {
            var vms = await GenerateViewModels(typeof(UIKeysTest.Summary));
            return View(vms);
        }
    }
}