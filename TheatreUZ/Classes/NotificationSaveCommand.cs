using TheatreUZ.Models;

namespace TheatreUZ
{
    public class NotificationSaveCommand : ICommand<CommandResponse>
    {
        public Notification Notification { get; private set; }

        public NotificationSaveCommand(Notification item)
        {
            Notification = item;
        }
    }
}