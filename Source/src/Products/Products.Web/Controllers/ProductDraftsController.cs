using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Products.Web.Models;
using CompositeUI.Service.Infrastructure.Attributes;
using CompositeUI.Service.Infrastructure.Consts;
using CompositeUI.Service.Infrastructure.Controllers;
using CompositeUI.Service.Infrastructure.Exceptions;
using CompositeUI.Service.Infrastructure.Models;
using CompositeUI.Service.Infrastructure.ViewModels;
using CompositeUI.Web.Common.UIKeys;

namespace CompositeUI.Products.Web.Controllers
{
    public class ProductDraftsController : ServiceController
    {
        private static Dictionary<string, Product> _productsDb = new Dictionary<string, Product>();

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Index()
        {
            var tableOrder = _productsDb.Where(p => !p.Value.IsPublished).OrderBy(p => p.Value.Title).Select(p => new ServicePublicData() { Id = p.Value.Id.ToString() }).ToList();
            var model = new TableOrderViewModel(UIKeysProductDrafts.Index.ProductTableOrder, tableOrder);
            return ViewModel(model);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> IndexTable(List<ServicePublicData> productTableOrder)
        {
            var ids = productTableOrder.Select(p => p.Id).ToList();
            var products = _productsDb.Where(p => ids.Contains(p.Value.Id.ToString())).ToList().Select(p => p.Value).ToList();
            var titleVM = new TableViewModel(UIKeysProductDrafts.IndexTable.ProductTitleColumn, (page, data) =>
            {
                var product = products.SingleOrDefault(p => p.Id.ToString() == data.Id);
                return product != null ? product.Title.ToString() : "";
            });
            var authorVM = new TableViewModel(UIKeysProductDrafts.IndexTable.ProductAuthorColumn, (page, data) =>
            {
                var product = products.SingleOrDefault(p => p.Id.ToString() == data.Id);
                return product != null ? product.Author.ToString() : "";
            });
            return ViewModels(titleVM, authorVM);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> IndexDiff()
        {
            var tableOrder = _productsDb.Where(p => !p.Value.IsPublished).OrderBy(p => p.Value.Title).Select(p => new ServicePublicData() { Id = p.Value.Id.ToString() }).ToList();
            var tableOrderVM = new TableOrderViewModel(UIKeysProductDrafts.Index.ProductTableOrder, tableOrder);
            var titleVM = new TableViewModel(UIKeysProductDrafts.IndexDiff.ProductTitleColumn, page => page.Html.Action("IndexDiff"));
            var authorVM = new TableViewModel(UIKeysProductDrafts.IndexDiff.ProductAuthorColumn, page => MvcHtmlString.Empty);
            return ViewModels(tableOrderVM, titleVM, authorVM);
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> GetTableData(List<ServicePublicData> productTableOrder)
        {
            var ids = productTableOrder.Select(p => p.Id).ToList();
            var products = _productsDb.Where(p => ids.Contains(p.Value.Id.ToString())).ToList().Select(p => new { id = p.Value.Id, title = p.Value.Title, author = p.Value.Author }).ToList();
            return JsonViewModel(products);
        }

        [HttpGet]
        [InternalAction]
        public ActionResult Add()
        {
            var model = new ViewModel(UIKeysProductDrafts.Add.ProductDataDiv, viewPage => viewPage.Html.Action("Add", new { model = new Product() }));
            return ViewModel(model);
        }

        [HttpPost]
        [InternalAction]
        public ActionResult Add(Guid productId, [Bind(Prefix = ProductsConsts.RouteServiceValue)] Product product, IEnumerable<string> productCategories)
        {
            if (!ModelState.IsValid || productCategories == null || !productCategories.Any())
                throw new InvalidModelException();

            product.Id = productId;
            product.CategoriesIds = productCategories.ToList();

            _productsDb.Add(productId.ToString(), product);

            return RequestHandled();
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Edit(Guid id)
        {
            var product = _productsDb.Single(p => p.Value.Id == id).Value;
            var list = product.CategoriesIds.Select(p => new ServicePublicData() { Id = p }).ToList();
            var breadcrumbs = new Dictionary<string, List<ServicePublicData>>() { { "productCategories", list } };
            var viewModel = new ViewModel(UIKeysProductDrafts.Edit.ProductDataDiv, viewPage => viewPage.Html.Action("Add", new { model = product }), breadcrumbs);
            return ViewModel(viewModel);
        }

        [HttpPost]
        [InternalAction]
        public ActionResult Edit(Guid productId, [Bind(Prefix = ProductsConsts.RouteServiceValue)] Product product, IEnumerable<string> productCategories)
        {
            if (!ModelState.IsValid || productCategories == null || !productCategories.Any())
                throw new InvalidModelException();

            product.Id = productId;
            product.CategoriesIds = productCategories.ToList();

            _productsDb[productId.ToString()] = product;

            return RequestHandled();
        }

        [HttpGet]
        [InternalAction]
        public async Task<ActionResult> Preview()
        {
            var viewModel = new ViewModel(UIKeysProductDrafts.Preview.ProductDataDiv, p => p.Html.Action("Preview", new { model = new Product() }));
            return ViewModel(viewModel);
        }


        [HttpPost]
        [InternalAction]
        public ActionResult Publish(Guid id)
        {
            var product = _productsDb.Single(p => p.Value.Id == id).Value;
            product.IsPublished = true;
            return RequestHandled();
        }
    }
}