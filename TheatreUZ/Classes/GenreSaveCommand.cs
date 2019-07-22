using TheatreUZ.Models;

namespace TheatreUZ
{
    public class GenreSaveCommand : ICommand<CommandResponse>
    {
        public Genre Genre { get; private set; }

        public GenreSaveCommand(Genre item)
        {
            Genre = item;
        }
    }
}