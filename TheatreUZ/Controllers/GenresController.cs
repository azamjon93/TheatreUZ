using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

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

        [Authorize]
        public ActionResult Index()
        {
            return View(repo.GetAllGenres());
        }

        public ActionResult GetGenre(Guid id)
        {
            return View(repo.GetGenre(id));
        }

        public ActionResult AddGenre()
        {
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddGenre(Genre item)
        {
            repo.SaveGenre(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditGenre(Guid id)
        {
            var genre = repo.GetGenre(id);
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", genre.StateID);
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", genre.StateID);
            return View(genre);
        }

        public ActionResult DeleteGenre(Guid id)
        {
            return View(repo.GetGenre(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteGenre(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
