using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Controllers;
using CompositeUI.Service.Infrastructure.Models;
using CompositeUI.Service.Infrastructure.RequestHandlers;
using CompositeUI.Service.Infrastructure.ViewModels;
using CompositeUI.Web.Common.Extensions;
using CompositeUI.Web.Common.UIKeys;
using Newtonsoft.Json;

namespace CompositeUI.Web.Controllers
{
    public class ProductDraftsController : WebController
    {
        public ProductDraftsController(IEnumerable<IRequestHandler> requestHandlers)
            : base(requestHandlers)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var vms = await GenerateViewModels(typeof(UIKeysProductDrafts.Index));
            var tableOrders = vms.Values.OfType<ITableOrderViewModel>().ToList();
            foreach (var tableOrder in tableOrders)
            {
                Session[tableOrder.Id] = tableOrder.SortedIds;
                ViewData[tableOrder.Id] = tableOrder.SortedIds.Any() ? tableOrder.SortedIds.Serialize("productTableOrder") : "";
            }
            return View(vms);
        }

        [HttpGet]
        public async Task<ActionResult> IndexTable(string key, List<ServicePublicData> productTableOrder)
        {
            var parameters = new Dictionary<string, object>();
            if (productTableOrder == null)
            {
                productTableOrder = Session[key] as List<ServicePublicData>;
                parameters.Add("productTableOrder", productTableOrder);
                Session.Remove(key);
            }
            var vms = await GenerateTableViewModels(typeof(UIKeysProductDrafts.IndexTable), parameters);
            ViewBag.ProductIds = productTableOrder;
            return View(vms);
        }

        [HttpGet]
        public async Task<ActionResult> IndexDiff()
        {
            var vms = await GenerateViewModels(typeof(UIKeysProductDrafts.IndexDiff));
            var tableOrders = vms.Values.OfType<ITableOrderViewModel>().ToList();
            foreach (var tableOrder in tableOrders)
            {
                Session[tableOrder.Id] = tableOrder.SortedIds;
                ViewData[tableOrder.Id] = tableOrder.SortedIds.Any() ? tableOrder.SortedIds.Serialize("productTableOrder") : "";
                ViewData[tableOrder.Id + "json"] = JsonConvert.SerializeObject(tableOrder.SortedIds);
            }
            return View(vms);
        }

        [HttpGet]
        public async Task<ActionResult> GetTableData(string key, List<ServicePublicData> productTableOrder)
        {
            var parameters = new Dictionary<string, object>();
            if (productTableOrder == null)
            {
                productTableOrder = Session[key] as List<ServicePublicData>;
                parameters.Add("productTableOrder", productTableOrder);
                Session.Remove(key);
            }
            var vms = await GenerateJsonViewModels(parameters);
            return Json(vms, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var viewModels = await GenerateViewModels(typeof(UIKeysProductDrafts.Add));
            ViewBag.ProductId = Guid.NewGuid();
            return View(viewModels);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Guid productId)
        {
            return await HandleRequestInTransaction(() => RedirectToAction("Index"), () => RedirectToAction("Add"));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModels = await GenerateViewModels(typeof(UIKeysProductDrafts.Edit));
            ViewBag.ProductId = id;
            return View(viewModels);
        }

        [HttpPost]
        public async Task<ActionResult> Edit()
        {
            return await HandleRequest(() => RedirectToAction("Index"), () => RedirectToAction("Edit"));
        }

        [HttpGet]
        public async Task<ActionResult> Preview()
        {
            var vms = await GenerateViewModels(typeof(UIKeysProductDrafts.Preview));
            return View(vms);
        }

        [HttpPost]
        public async Task<ActionResult> Publish(Guid id)
        {
            return await HandleRequest(() => RedirectToAction("Index"), () => RedirectToAction("Index"));
        }
	}
}