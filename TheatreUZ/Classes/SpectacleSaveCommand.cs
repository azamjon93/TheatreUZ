using TheatreUZ.Models;

namespace TheatreUZ
{
    public class SpectacleSaveCommand : ICommand<CommandResponse>
    {
        public Spectacle Spectacle { get; private set; }

        public SpectacleSaveCommand(Spectacle item)
        {
            Spectacle = item;
        }
    }
}