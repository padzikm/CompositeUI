using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CompositeUI.Orders.Web.Models;
using CompositeUI.Service.Infrastructure;
using CompositeUI.Web.Common.UIKeys;

namespace CompositeUI.Orders.Web.Controllers
{
    public class ProductDraftsController : ServiceController
    {
        private static Dictionary<string, string> _productCategoriesDict = new Dictionary<string, string>() { { "1", "Thriller" }, { "3", "Action" }, { "2", "Romantic" } };

        [InternalAction]
        [GetAndReplayPost]
        public ActionResult Add()
        {
            var model = new ProductModel() { ProductCategories = _productCategoriesDict };
            var vm = new ViewModel(UIKeysProductDrafts.Add.ProductCategorySelect, p => p.Html.Action("Add", new { model }));
            return ViewModel(vm);
        }

        [InternalAction]
        [GetAndReplayPost]
        public ActionResult Edit()
        {
            var model = new ProductModel() { ProductCategories = _productCategoriesDict };
            var vm = new ViewModel(UIKeysProductDrafts.Edit.ProductCategorySelect, p => p.Html.Action("Edit", new { model }));
            return ViewModel(vm);
        }

        [InternalAction]
        [HttpGet]
        public ActionResult Preview()
        {
            var model = new ProductModel() {ProductCategories = new Dictionary<string, string>()};
            var vm = new ViewModel(UIKeysProductDrafts.Preview.ProductCategorySelect, p => p.Html.Action("Preview", new { model }));
            return ViewModel(vm);
        }

        [HttpGet]
        [ServiceAction]
        public ActionResult GetCategories(IEnumerable<string> categoryIds)
        {
            var list = _productCategoriesDict.Where(p => categoryIds.Contains(p.Key)).Select(p => p.Value).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}