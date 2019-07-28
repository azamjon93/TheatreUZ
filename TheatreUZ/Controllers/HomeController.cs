using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

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
            IEnumerable<Spectacle> allSpectacles = repo.GetAllSpectacles();
            List<Spectacle> spectacles = allSpectacles.OrderBy(s => s.PlayDate).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();

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
            Spectacle spectacle = repo.GetSpectacle(id);
            int x = repo.GetAllSales().Where(s => s.SpectacleID == id && s.State.Name == "Active").Sum(a => a.Amount);

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
            if (TAuth.IsLogged() && !TAuth.IsAdmin())
            {
                var userID = (string)Session["UserID"];

                if (userID == null || id == null)
                {
                    return RedirectToAction("Index");
                }

                Spectacle spectacle = repo.GetSpectacle(id);
                User user = repo.GetUser(Guid.Parse(userID));

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
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult ToBookConfirm(string saleStr)
        {
            bool success = false;
            string message = "There was an error";

            if (TAuth.IsLogged() && !TAuth.IsAdmin())
            {
                try
                {
                    Sale sale = JsonConvert.DeserializeObject<Sale>(saleStr);
                    sale.ID = Guid.Empty;

                    int ticketsCount = repo.GetSpectacle(sale.SpectacleID).TicketsCount;
                    int allSales = repo.GetAllSales().Where(s => s.SpectacleID == sale.SpectacleID && s.State.Name == "Active").Sum(a => a.Amount);

                    if (sale.Amount <= ticketsCount - allSales)
                    {
                        repo.SaveSale(sale);

                        success = true;
                        message = "OK";
                    }
                    else
                    {
                        message = string.Format("We have not {0} tickets. We have {1}", sale.Amount, ticketsCount - allSales);
                    }
                }
                catch
                {

                }
            }

            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
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