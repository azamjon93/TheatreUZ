using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Ninject;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class StatesController : Controller
    {
        IRepository repo;
        public StatesController(IRepository r)
        {
            repo = r;
        }

        public string AllStates()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAll(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        public ActionResult GetState(Guid id)
        {
            return View((State)repo.GetOne(id));
        }

        public ActionResult AddState()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddState(State item)
        {
            repo.Save(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditState(Guid id)
        {
            return View((State)repo.GetOne(id));
        }

        public ActionResult DeleteState(Guid id)
        {
            var handler = StateQueryHandlerFactory.Build(new OneStateQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
