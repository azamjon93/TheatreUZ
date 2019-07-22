using System;

namespace TheatreUZ
{
    public class UserDeleteCommand : ICommand<CommandResponse>
    {
        public Guid UserID { get; private set; }

        public UserDeleteCommand(Guid id)
        {
            UserID = id;
        }
    }
}