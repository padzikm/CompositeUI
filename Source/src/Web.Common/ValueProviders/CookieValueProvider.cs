using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeManager.Web.Common.ValueProviders
{
    public class CookieValueProvider : IValueProvider
    {
        private readonly HttpCookieCollection _cookies;

        public CookieValueProvider(HttpCookieCollection cookies)
        {
            _cookies = cookies;
        }

        public bool ContainsPrefix(string prefix)
        {
            return _cookies.AllKeys.Any(p => p.StartsWith(prefix));
        }

        public ValueProviderResult GetValue(string key)
        {
            if (_cookies.AllKeys.Contains(key))
            {
                var cookie = _cookies[key];
                var value = cookie.Value;
                return new ValueProviderResult(value, value, CultureInfo.InvariantCulture);
            }
            return null;
        }
    }
}