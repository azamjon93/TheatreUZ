using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

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
            return View(repo.GetAllSales());
        }

        public ActionResult GetSale(Guid id)
        {
            return View(repo.GetSale(id));
        }

        public ActionResult AddSale()
        {
            ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name");
            ViewBag.SpectacleID = new SelectList(repo.GetAllSpectacles(), "ID", "Name");
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddSale(Sale item)
        {
            repo.SaveSale(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditSale(Guid id)
        {
            var sale = repo.GetSale(id);
            ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name", sale.UserID);
            ViewBag.SpectacleID = new SelectList(repo.GetAllSpectacles(), "ID", "Name", sale.SpectacleID);
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", sale.StateID);
            return View(sale);
        }

        public ActionResult DeleteSale(Guid id)
        {
            return View(repo.GetSale(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteSale(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
