using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class SpectaclesController : Controller
    {
        public string AllSpectacles()
        {
            var handler = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery());
            var spectacles = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(spectacles.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            var handler = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery());

            return View(handler.Get());
        }

        public ActionResult GetSpectacle(Guid id)
        {
            var handler = SpectacleQueryHandlerFactory.Build(new OneSpectacleQuery(id));

            return View(handler.Get());
        }

        public ActionResult AddSpectacle()
        {
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var genresQueryHandler = GenreQueryHandlerFactory.Build(new AllGenresQuery());

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name");
            ViewBag.GenreID = new SelectList(genresQueryHandler.Get(), "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddSpectacle(Spectacle item)
        {
            var handler = SpectacleSaveCommandHandlerFactory.Build(new SpectacleSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditSpectacle(Guid id)
        {
            var spectacleQueryHandler = SpectacleQueryHandlerFactory.Build(new OneSpectacleQuery(id));
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var genresQueryHandler = GenreQueryHandlerFactory.Build(new AllGenresQuery());

            var spectacle = spectacleQueryHandler.Get();

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name", spectacle.StateID);
            ViewBag.GenreID = new SelectList(genresQueryHandler.Get(), "ID", "Name", spectacle.GenreID);

            return View(spectacle);
        }

        public ActionResult DeleteSpectacle(Guid id)
        {
            var handler = SpectacleQueryHandlerFactory.Build(new OneSpectacleQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = SpectacleDeleteCommandHandlerFactory.Build(new SpectacleDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
