using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Service.Infrastructure;
using CompositeUI.Web.Common.UIKeys;

namespace CompositeUI.Orders.Web.Controllers
{
    public class PubSubCacheController : ServiceController
    {
        private static readonly List<Guid> _productsDb = new List<Guid>();

        static PubSubCacheController()
        {
            for(var i = 0; i < 5; ++i)
                _productsDb.Add(Guid.NewGuid());
        }
        
        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Communication(IEnumerable<string> uiKeys)
        {
            var count = uiKeys.Count();
            var list = _productsDb.Take(count).ToList();
            ViewData["time"] = DateTime.UtcNow.ToLongTimeString();
            var vm = new ViewModel(UIKeysPubSubCache.Communication.OrdersLabel, page => page.Html.Action("Communication", new { model = list }));
            return ViewModel(vm);
        }

        [OutputCache(Duration = 15)]
        [HttpGet]
        [ServiceAction]
        public async Task<ActionResult> GetTotal(IEnumerable<Guid> productIds)
        {
            var json = new { time = DateTime.UtcNow.ToLongTimeString(), total = productIds.Count() };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Networking(IEnumerable<string> uiKeys)
        {
            var count = uiKeys.Count();
            var vm = new ViewModel(UIKeysPubSubCache.Networking.OrdersLabel, page => page.Html.Action("Networking", new { count })) { ReturnAction = true };
            return ViewModel(vm);
        }

        [OutputCache(Duration = 15)]
        [HttpGet]
        [ServiceAction]
        [ChildActionOnly]
        public async Task<ActionResult> Networking(int count, string containerId)
        {
            ViewData["time"] = DateTime.UtcNow.ToLongTimeString();
            ViewData[Consts.ContainerId] = containerId;
            var list = _productsDb.Take(count).ToList();
            return View(list);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> NetworkingData([Bind(Prefix = OrdersConsts.RouteServiceValue)] IEnumerable<Guid> productIds)
        {
            var json = new { time = DateTime.UtcNow.ToLongTimeString(), total = productIds.Count() };
            return JsonViewModel(json);
        }
	}
}