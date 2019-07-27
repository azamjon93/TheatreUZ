using System;
using System.Web.Mvc;
using Newtonsoft.Json;
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
                return JsonConvert.SerializeObject(repo.GetAllStates(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            return View(repo.GetAllStates());
        }

        public ActionResult GetState(Guid id)
        {
            return View(repo.GetState(id));
        }

        public ActionResult AddState()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddState(State item)
        {
            repo.SaveState(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditState(Guid id)
        {
            return View(repo.GetState(id));
        }

        public ActionResult DeleteState(Guid id)
        {
            return View(repo.GetState(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteState(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
