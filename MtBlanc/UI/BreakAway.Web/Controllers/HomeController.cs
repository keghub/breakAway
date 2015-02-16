using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCloud.Web.Routing;

namespace BreakAway.Web.Controllers
{
    public class HomeController : Controller
    {
        [UrlRoute(Path ="")]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [UrlRoute(Path = "about")]
        public ActionResult About()
        {
            return View();
        }
    }
}
