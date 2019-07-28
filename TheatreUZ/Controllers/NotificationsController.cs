using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

namespace TheatreUZ.Controllers
{
    public class NotificationsController : Controller
    {
        IRepository repo;

        public NotificationsController(IRepository r)
        {
            repo = r;
        }

        public string AllNotifications()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAllNotifications(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
                return View(repo.GetAllNotifications());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetNotification(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetNotification(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddNotification()
        {
            if (TAuth.IsAdmin())
            {
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
                ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddNotification(Notification item)
        {
            if (TAuth.IsAdmin())
            {
                repo.SaveNotification(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditNotification(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                var notification = repo.GetNotification(id);

                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", notification.StateID);
                ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name", notification.UserID);

                return View(notification);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteNotification(Guid id)
        {
            return View(repo.GetNotification(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                repo.DeleteNotification(id);
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
