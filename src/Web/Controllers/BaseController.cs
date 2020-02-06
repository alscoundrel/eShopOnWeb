using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("home")]
    public class BaseController : Controller
    {
        public ActionResult pt()
        {
            var cultureInfo = new CultureInfo("pt-PT");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            return View();
        }

        public ActionResult en()
        {
            var cultureInfo = new CultureInfo("pt-PT");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            return View();
        }
    }
}
