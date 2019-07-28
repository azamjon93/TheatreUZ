using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

namespace TheatreUZ.Controllers
{
    public class SalesController : Controller
    {
        IRepository repo;

        public SalesController(IRepository r)
        {
            repo = r;
        }

        public string AllSales()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAllSales(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetAllSales());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetSale(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetSale(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddSale()
        {
            if (TAuth.IsAdmin())
            {
                ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name");
                ViewBag.SpectacleID = new SelectList(repo.GetAllSpectacles(), "ID", "Name");
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddSale(Sale item)
        {
            if (TAuth.IsAdmin())
            {
                repo.SaveSale(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditSale(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                var sale = repo.GetSale(id);

                ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name", sale.UserID);
                ViewBag.SpectacleID = new SelectList(repo.GetAllSpectacles(), "ID", "Name", sale.SpectacleID);
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", sale.StateID);

                return View(sale);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteSale(Guid id)
        {
            return View(repo.GetSale(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                repo.DeleteSale(id);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
