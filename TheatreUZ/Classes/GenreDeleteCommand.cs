using System;

namespace TheatreUZ
{
    public class GenreDeleteCommand : ICommand<CommandResponse>
    {
        public Guid GenreID { get; private set; }

        public GenreDeleteCommand(Guid id)
        {
            GenreID = id;
        }
    }
}