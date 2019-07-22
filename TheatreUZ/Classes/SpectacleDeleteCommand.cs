using System;

namespace TheatreUZ
{
    public class SpectacleDeleteCommand : ICommand<CommandResponse>
    {
        public Guid SpectacleID { get; private set; }

        public SpectacleDeleteCommand(Guid id)
        {
            SpectacleID = id;
        }
    }
}