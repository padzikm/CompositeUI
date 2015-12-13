using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Products.Web.Models;
using CompositeUI.Service.Infrastructure;
using CompositeUI.Web.Common.UIKeys;

namespace CompositeUI.Products.Web.Controllers
{
    public class PubSubCacheController : ServiceController
    {
        private static readonly List<Product> _productsDb = new List<Product>()
        {
            new Product(){Id = Guid.NewGuid(), Title = "Jan Brzechwa"},
            new Product(){Id = Guid.NewGuid(), Title = "Henryk Sienkiewicz"},
            new Product(){Id = Guid.NewGuid(), Title = "Adam Mickiewicz"},
            new Product(){Id = Guid.NewGuid(), Title = "Stanislaw Lem"},
            new Product(){Id = Guid.NewGuid(), Title = "Aleksander Fredro"},
        };

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Communication()
        {
            ViewData["time"] = DateTime.UtcNow.ToLongTimeString();
            var productIds = _productsDb.Select(p => p.Id).ToList();
            var vm = new ViewModel(UIKeysPubSubCache.Communication.ProductsDiv, page => page.Html.Action("Communication", new { model = productIds }));
            return ViewModel(vm);
        }

        [OutputCache(Duration = 10)]
        [HttpGet]
        [ServiceAction]
        public async Task<ActionResult> GetData(IEnumerable<Guid> productIds)
        {
            var result = _productsDb.Where(p => productIds.Contains(p.Id)).Select(p => new { id = p.Id, title = p.Title }).ToList();
            var json = new { time = DateTime.UtcNow.ToLongTimeString(), products = result };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Networking()
        {
            var vm = new ViewModel(UIKeysPubSubCache.Networking.ProductsDiv, page => page.Html.Action("Networking")) { InvokeAction = true };
            return ViewModel(vm);
        }

        [OutputCache(Duration = 10)]
        [HttpGet]
        [ServiceAction]
        [ChildActionOnly]
        public async Task<ActionResult> Networking(string containerId)
        {
            ViewData["time"] = DateTime.UtcNow.ToLongTimeString();
            ViewData[Consts.ContainerId] = containerId;
            var productIds = _productsDb.Select(p => p.Id).ToList();
            return View(productIds);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> NetworkingData([Bind(Prefix = ProductsConsts.RouteServiceValue)] IEnumerable<Guid> productIds)
        {
            var result = _productsDb.Where(p => productIds.Contains(p.Id)).Select(p => new { id = p.Id, title = p.Title }).ToList();
            var json = new { time = DateTime.UtcNow.ToLongTimeString(), products = result };
            return JsonViewModel(json);
        }
    }
}