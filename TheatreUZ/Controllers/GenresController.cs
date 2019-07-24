using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class GenresController : Controller
    {
        public string AllGenres()
        {
            var handler = GenreQueryHandlerFactory.Build(new AllGenresQuery());
            var genres = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(genres.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            var handler = GenreQueryHandlerFactory.Build(new AllGenresQuery());

            return View(handler.Get());
        }

        public ActionResult GetGenre(Guid id)
        {
            var handler = GenreQueryHandlerFactory.Build(new OneGenreQuery(id));

            return View(handler.Get());
        }

        public ActionResult AddGenre()
        {
            var handler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            ViewBag.StateID = new SelectList(handler.Get(), "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddGenre(Genre item)
        {
            var handler = GenreSaveCommandHandlerFactory.Build(new GenreSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditGenre(Guid id)
        {
            var genreQueryHandler = GenreQueryHandlerFactory.Build(new OneGenreQuery(id));
            var StatesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());

            var genre = genreQueryHandler.Get();
            var states = StatesQueryHandler.Get();

            ViewBag.StateID = new SelectList(states, "ID", "Name", genre.StateID);

            return View(genre);
        }

        public ActionResult DeleteGenre(Guid id)
        {
            var handler = GenreQueryHandlerFactory.Build(new OneGenreQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = GenreDeleteCommandHandlerFactory.Build(new GenreDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
