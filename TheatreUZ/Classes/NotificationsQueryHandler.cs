using System;
using System.Collections.Generic;
using TheatreUZ.Models;

namespace TheatreUZ
{
    public class AllNotificationsQuery : IQuery<IEnumerable<Notification>>
    {

    }

    public class OneNotificationQuery : IQuery<Notification>
    {
        public Guid ID { get; set; }

        public OneNotificationQuery(Guid id)
        {
            ID = id;
        }
    }
}