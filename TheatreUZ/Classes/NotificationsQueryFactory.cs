using System.Collections.Generic;
using System.Linq;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public static class NotificationQueryHandlerFactory
    {
        public static IQueryHandler<AllNotificationsQuery, IEnumerable<Notification>> Build(AllNotificationsQuery query, TheatreUZContext dbContext)
        {
            return new AllNotificationsQueryHandler(dbContext);
        }

        public static IQueryHandler<OneNotificationQuery, Notification> Build(OneNotificationQuery query, TheatreUZContext dbContext)
        {
            return new OneNotificationQueryHandler(query, dbContext);
        }
    }

    public class AllNotificationsQueryHandler : IQueryHandler<AllNotificationsQuery, IEnumerable<Notification>>
    {
        TheatreUZContext db;

        public AllNotificationsQueryHandler(TheatreUZContext dbContext)
        {
            db = dbContext;
        }

        public IEnumerable<Notification> Get()
        {
            return db.Notifications.OrderByDescending(w => w.RegDate);
        }
    }

    public class OneNotificationQueryHandler : IQueryHandler<OneNotificationQuery, Notification>
    {
        private readonly OneNotificationQuery query;
        TheatreUZContext db;

        public OneNotificationQueryHandler(OneNotificationQuery query, TheatreUZContext dbContext)
        {
            this.query = query;
            db = dbContext;
        }

        public Notification Get()
        {
            return db.Notifications.FirstOrDefault(s => s.ID == query.ID);
        }
    }
}