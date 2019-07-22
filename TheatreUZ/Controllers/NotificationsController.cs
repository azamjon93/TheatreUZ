using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class NotificationsController : Controller
    {
        public string AllNotifications()
        {
            var handler = NotificationQueryHandlerFactory.Build(new AllNotificationsQuery());
            var notifications = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(notifications.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            var handler = NotificationQueryHandlerFactory.Build(new AllNotificationsQuery());

            return View(handler.Get());
        }

        public ActionResult GetNotification(Guid id)
        {
            var handler = NotificationQueryHandlerFactory.Build(new OneNotificationQuery(id));

            return View(handler.Get());
        }

        public ActionResult AddNotification()
        {
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var usersQueryHandler = UserQueryHandlerFactory.Build(new AllUsersQuery());

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name");
            ViewBag.UserID = new SelectList(usersQueryHandler.Get(), "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddNotification(Notification item)
        {
            var handler = NotificationSaveCommandHandlerFactory.Build(new NotificationSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditNotification(Guid id)
        {
            var notificationQueryHandler = NotificationQueryHandlerFactory.Build(new OneNotificationQuery(id));
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var usersQueryHandler = UserQueryHandlerFactory.Build(new AllUsersQuery());

            var notification = notificationQueryHandler.Get();

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name", notification.StateID);
            ViewBag.UserID = new SelectList(usersQueryHandler.Get(), "ID", "Name", notification.UserID);

            return View(notification);
        }

        public ActionResult DeleteNotification(Guid id)
        {
            var handler = NotificationQueryHandlerFactory.Build(new OneNotificationQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = NotificationDeleteCommandHandlerFactory.Build(new NotificationDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
