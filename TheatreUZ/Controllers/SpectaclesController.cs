using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class SpectaclesController : Controller
    {
        IRepository repo;

        public SpectaclesController(IRepository r)
        {
            repo = r;
        }

        public string AllSpectacles()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAllSpectacles(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            return View(repo.GetAllSpectacles());
        }

        public ActionResult GetSpectacle(Guid id)
        {
            return View(repo.GetSpectacle(id));
        }

        public ActionResult AddSpectacle()
        {
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
            ViewBag.GenreID = new SelectList(repo.GetAllGenres(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddSpectacle(Spectacle item)
        {
            repo.SaveSpectacle(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditSpectacle(Guid id)
        {
            var spectacle = repo.GetSpectacle(id);
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", spectacle.StateID);
            ViewBag.GenreID = new SelectList(repo.GetAllRoles(), "ID", "Name", spectacle.GenreID);
            return View(spectacle);
        }

        public ActionResult DeleteSpectacle(Guid id)
        {
            return View(repo.GetSpectacle(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteSpectacle(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
