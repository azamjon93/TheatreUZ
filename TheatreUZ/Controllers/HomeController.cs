using System;
using System.Linq;
using System.Web.Mvc;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;

        public HomeController(IRepository r)
        {
            repo = r;
        }

        public ActionResult Index(int id = 1)
        {
            return View(GetSpectacles(id));
        }

        private SpectaclePageModel GetSpectacles(int currentPage)
        {
            int maxRows = 9;
            var allSpectacles = repo.GetAllSpectacles();
            var spectacles = allSpectacles.OrderBy(s => s.PlayDate).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();
            SpectaclePageModel pageModel = null;
            if (spectacles.Count == 0)
            {
                pageModel = new SpectaclePageModel()
                {
                    Spectacles = new System.Collections.Generic.List<Spectacle>(),
                    PageInfo = new PageInfo
                    {
                        PageNumber = currentPage,
                        PageSize = 0,
                        TotalItems = allSpectacles.Count()
                    }
                };
            }
            else
            {
                pageModel = new SpectaclePageModel
                {
                    Spectacles = spectacles,
                    PageInfo = new PageInfo
                    {
                        PageNumber = currentPage,
                        PageSize = spectacles.Count,
                        TotalItems = allSpectacles.Count()
                    }
                };
            }
            return pageModel;
        }

        public ActionResult SpectacleReadMore(Guid id)
        {
            var spectacle = repo.GetSpectacle(id);
            var x = repo.GetAllSales().Where(s => s.SpectacleID == id).Sum(a => a.Amount);
            SpectacleReadModel srm = new SpectacleReadModel
            {
                Spectacle = spectacle,
                Image = "~/Content/Images/scene2.jpg",
                Remain = spectacle.TicketsCount - x
            };
            return View(srm);
        }

        public ActionResult ToBook(Guid id)
        {
            var userID = (string)Session["UserID"];
            if (userID == null || id == null)
            {
                return RedirectToAction("Index");
            }
            var spectacle = repo.GetSpectacle(id);
            var user = repo.GetUser(Guid.Parse(userID));
            Sale sale = new Sale
            {
                ID = Guid.Empty,
                SpectacleID = spectacle.ID,
                Spectacle = spectacle,
                UserID = user.ID,
                User = user,
                Amount = 2
            };
            return View(sale);
        }

        [HttpPost]
        public ActionResult ToBook(Sale sale)
        {
            sale.ID = Guid.Empty;
            var ticketsCount = repo.GetSpectacle(sale.SpectacleID).TicketsCount;
            var allSales = repo.GetAllSales().Where(s => s.SpectacleID == sale.SpectacleID && s.State.Name == "Active").Sum(a => a.Amount);
            if (sale.Amount <= ticketsCount - allSales)
            {
                repo.SaveSale(sale);
            }
            return RedirectToAction("Index");
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