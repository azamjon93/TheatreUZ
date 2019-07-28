using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

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
            if (TAuth.IsAdmin())
            {
                return View(repo.GetAllSpectacles());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetSpectacle(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetSpectacle(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddSpectacle()
        {
            if (TAuth.IsAdmin())
            {
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
                ViewBag.GenreID = new SelectList(repo.GetAllGenres(), "ID", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddSpectacle(Spectacle item)
        {
            if (TAuth.IsAdmin())
            {
                repo.SaveSpectacle(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditSpectacle(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                var spectacle = repo.GetSpectacle(id);

                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", spectacle.StateID);
                ViewBag.GenreID = new SelectList(repo.GetAllGenres(), "ID", "Name", spectacle.GenreID);

                return View(spectacle);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteSpectacle(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetSpectacle(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                repo.DeleteSpectacle(id);

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
