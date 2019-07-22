using TheatreUZ.Models;

namespace TheatreUZ
{
    public class UserSaveCommand : ICommand<CommandResponse>
    {
        public User User { get; private set; }

        public UserSaveCommand(User item)
        {
            User = item;
        }
    }
}