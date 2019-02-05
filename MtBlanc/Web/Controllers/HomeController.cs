using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BreakAway.Domain.Core.Sites;
using ITCloud.Web.Routing;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Controllers
{
    public class HomeController : Controller
    {
        [UrlRoute(Path = "")]
        public ActionResult Index()
        {
            return View();
        }

        [UrlRoute(Path = "about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [UrlRoute(Path = "contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}