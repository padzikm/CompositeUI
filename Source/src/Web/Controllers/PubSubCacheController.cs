using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using CompositeUI.Service.Infrastructure;
using CompositeUI.Web.Common.UIKeys;

namespace CompositeUI.Web.Controllers
{
    public class PubSubCacheController : WebController
    {
        public PubSubCacheController(IEnumerable<IRequestHandler> requestHandlers) : base(requestHandlers)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Communication()
        {
            var vms = await GenerateViewModels(typeof(UIKeysPubSubCache.Communication));
            return View(vms);
        }

        [HttpGet]
        public async Task<ActionResult> Networking()
        {
            var vms = await GenerateViewModels(typeof(UIKeysPubSubCache.Networking));
            return View(vms);
        }

        [HttpGet]
        public async Task<ActionResult> NetworkingData()
        {
            var vms = await GenerateJsonViewModels();
            return Json(vms, JsonRequestBehavior.AllowGet);
        }
    }
}