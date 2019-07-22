using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class NotificationQueryHandlerFactory
    {
        public static IQueryHandler<AllNotificationsQuery, IEnumerable<Notification>> Build(AllNotificationsQuery query)
        {
            return new AllNotificationsQueryHandler();
        }

        public static IQueryHandler<OneNotificationQuery, Notification> Build(OneNotificationQuery query)
        {
            return new OneNotificationQueryHandler(query);
        }
    }

    public class AllNotificationsQueryHandler : IQueryHandler<AllNotificationsQuery, IEnumerable<Notification>>
    {
        public IEnumerable<Notification> Get()
        {
            var db = new TheatreUZContext();
            return db.Notifications.OrderByDescending(w => w.RegDate);
        }
    }

    public class OneNotificationQueryHandler : IQueryHandler<OneNotificationQuery, Notification>
    {
        private readonly OneNotificationQuery query;

        public OneNotificationQueryHandler(OneNotificationQuery query)
        {
            this.query = query;
        }

        public Notification Get()
        {
            var db = new TheatreUZContext();
            return db.Notifications.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}