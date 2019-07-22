using TheatreUZ.Models;

namespace TheatreUZ
{
    public class RoleSaveCommand : ICommand<CommandResponse>
    {
        public Role Role { get; private set; }

        public RoleSaveCommand(Role item)
        {
            Role = item;
        }
    }
}