using System;
using System.Linq;
using System.Web.Mvc;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(GetSpectacles(1));
        }

        [HttpPost]
        public ActionResult Index(int pageIndex)
        {
            return View(GetSpectacles(pageIndex));
        }

        private SpectaclePageModel GetSpectacles(int currentPage)
        {
            int maxRows = 2;

            var allSpectaclesHandler = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery());
            var allSpectacles = allSpectaclesHandler.Get();

            SpectaclePageModel pageModel = new SpectaclePageModel
            {
                Spectacles = allSpectacles.OrderBy(s => s.PlayDate).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList(),
                CurrentPageIndex = currentPage,
                PageCount = (int)Math.Ceiling(allSpectacles.Count() / (double)maxRows)
            };

            return pageModel;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}