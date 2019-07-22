using System;

namespace TheatreUZ
{
    public class RoleDeleteCommand : ICommand<CommandResponse>
    {
        public Guid RoleID { get; private set; }

        public RoleDeleteCommand(Guid id)
        {
            RoleID = id;
        }
    }
}