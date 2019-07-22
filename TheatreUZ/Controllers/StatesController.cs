using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class StatesController : Controller
    {
        public string AllStates()
        {
            var handler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var states = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(states.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            var handler = StateQueryHandlerFactory.Build(new AllStatesQuery());

            return View(handler.Get());
        }

        public ActionResult GetState(Guid id)
        {
            var handler = StateQueryHandlerFactory.Build(new OneStateQuery(id));

            return View(handler.Get());
        }

        public ActionResult AddState()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddState(State item)
        {
            var handler = StateSaveCommandHandlerFactory.Build(new StateSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditState(Guid id)
        {
            var handler = StateQueryHandlerFactory.Build(new OneStateQuery(id));

            return View(handler.Get());
        }

        public ActionResult DeleteState(Guid id)
        {
            var handler = StateQueryHandlerFactory.Build(new OneStateQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = StateDeleteCommandHandlerFactory.Build(new StateDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
