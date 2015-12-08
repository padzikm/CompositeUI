using System.Web.Mvc;

namespace CompositeUI.Web.Common.ValueProviders
{
    public class CookieValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            var cookies = controllerContext.RequestContext.HttpContext.Request.Cookies;
            return new CookieValueProvider(cookies);
        }
    }
}