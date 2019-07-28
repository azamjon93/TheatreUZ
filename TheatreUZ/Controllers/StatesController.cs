using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

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
            if (TAuth.IsAdmin())
            {
                return View(repo.GetAllStates());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetState(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetState(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddState()
        {
            if (TAuth.IsAdmin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddState(State item)
        {
            if (TAuth.IsAdmin())
            {
                repo.SaveState(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditState(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetState(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteState(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetState(id));
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
                repo.DeleteState(id);

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
