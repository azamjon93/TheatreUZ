using System;

namespace TheatreUZ
{
    public class NotificationDeleteCommand : ICommand<CommandResponse>
    {
        public Guid NotificationID { get; private set; }

        public NotificationDeleteCommand(Guid id)
        {
            NotificationID = id;
        }
    }
}