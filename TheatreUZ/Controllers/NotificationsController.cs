using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

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
            return View(repo.GetAllNotifications());
        }

        public ActionResult GetNotification(Guid id)
        {
            return View(repo.GetNotification(id));
        }

        public ActionResult AddNotification()
        {
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
            ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddNotification(Notification item)
        {
            repo.SaveNotification(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditNotification(Guid id)
        {
            var notification = repo.GetNotification(id);
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", notification.StateID);
            ViewBag.UserID = new SelectList(repo.GetAllUsers(), "ID", "Name", notification.UserID);
            return View(notification);
        }

        public ActionResult DeleteNotification(Guid id)
        {
            return View(repo.GetNotification(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteNotification(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
