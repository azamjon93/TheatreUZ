using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

namespace TheatreUZ.Controllers
{
    public class GenresController : Controller
    {
        IRepository repo;

        public GenresController(IRepository r)
        {
            repo = r;
        }

        public string AllGenres()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAllGenres(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
                return View(repo.GetAllGenres());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetGenre(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetGenre(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddGenre()
        {
            if (TAuth.IsAdmin())
            {
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddGenre(Genre item)
        {
            if (TAuth.IsAdmin())
            {
                repo.SaveGenre(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditGenre(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                var genre = repo.GetGenre(id);

                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", genre.StateID);
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", genre.StateID);

                return View(genre);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteGenre(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetGenre(id));
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
                repo.DeleteGenre(id);

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
