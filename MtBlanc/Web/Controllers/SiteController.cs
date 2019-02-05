using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BreakAway.Domain.Core.Sites;
using BreakAway.Models.Sites;
using ITCloud.Web.Routing;

namespace BreakAway.Controllers
{
    public class SiteController : Controller
    {
        private readonly ISiteRepository _siteRepository;

        public SiteController(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository ?? throw new ArgumentNullException(nameof(siteRepository));
        }

        [UrlRoute(Path = "site/list")]
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Sites = _siteRepository.Items.Select(p => new SiteItem
            {
                Id = p.Id,
                Domain = p.Domain,
                IsActive = p.IsActive,
                Type = p.Type.Name
            }).ToArray();

            return View(viewModel);
        }
    }
}