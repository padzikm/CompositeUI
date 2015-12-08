using CompositeUI.Service.Infrastructure.Consts;

namespace CompositeUI.Service.Infrastructure.ViewEngines
{
    public class CustomersViewEngine : ViewEngine
    {
        public override string RouteServiceValue
        {
            get { return CustomersConsts.RouteServiceValue; }
        }

        public CustomersViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaMasterLocationFormats = new[]
            {
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            ViewLocationFormats = new[]
            {
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            MasterLocationFormats = new[]
            {
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + CustomersConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            FileExtensions = new[]
            {
                "cshtml",
                "vbhtml"
            };
        }
    }

    public class OrdersViewEngine : ViewEngine
    {
        public override string RouteServiceValue
        {
            get { return OrdersConsts.RouteServiceValue; }
        }

        public OrdersViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaMasterLocationFormats = new[]
            {
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            ViewLocationFormats = new[]
            {
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            MasterLocationFormats = new[]
            {
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + OrdersConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            FileExtensions = new[]
            {
                "cshtml",
                "vbhtml"
            };
        }
    }

    public class ProductsViewEngine : ViewEngine
    {
        public override string RouteServiceValue
        {
            get { return ProductsConsts.RouteServiceValue; }
        }

        public ProductsViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaMasterLocationFormats = new[]
            {
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            ViewLocationFormats = new[]
            {
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            MasterLocationFormats = new[]
            {
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/{1}/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/{1}/{0}.vbhtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/Shared/{0}.cshtml",
                "~/" + ProductsConsts.ServiceViewFolder +"/Views/Shared/{0}.vbhtml"
            };
            FileExtensions = new[]
            {
                "cshtml",
                "vbhtml"
            };
        }
    }
}